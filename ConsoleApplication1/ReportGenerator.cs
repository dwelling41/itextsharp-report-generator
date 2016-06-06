using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class ReportGenerator
    {
        #region Fields & Properties
        // Section Header Fonts
        private Font SECTION_HEADER_FONT = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 16, BaseColor.BLACK);

        // Body Fonts
        private Font BODY_BOLD_FONT = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.BLACK);
        private Font BODY_FONT = FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.BLACK);
        private Font BODY_UNDERLINE_FONT = FontFactory.GetFont(FontFactory.HELVETICA, BaseFont.IDENTITY_H, 8, Font.UNDERLINE, BaseColor.BLACK);

        // Table Fonts
        private Font TABLE_BOLD_FONT = FontFactory.GetFont(FontFactory.HELVETICA_BOLD, 8, BaseColor.BLACK);
        private Font TABLE_FONT = FontFactory.GetFont(FontFactory.HELVETICA, 8, BaseColor.BLACK);
        #endregion

        #region Public Methods
        public ReportGenerator()
        {
        }

        /// <summary>
        /// Generates a PDF for the report and places the document at pdfOutputPath
        /// </summary>
        /// <param name="pdfOutputPath">Where the PDF will be output to</param>
        /// <param name="bankName"></param>
        /// <param name="toName"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="openingBalance"></param>
        /// <param name="balanceItems"></param>
        /// <param name="calculationType"></param>
        /// <param name="indexName"></param>
        /// <param name="dayCountConvention"></param>
        /// <returns></returns>
        public void GenerateReport(string pdfOutputPath, string bankName, string toName, DateTime startDate, DateTime endDate,
            decimal openingBalance, List<DailyBalance> balanceItems, string calculationType, string indexName,
            string dayCountConvention)
        {
            // Open up the document
            Document pdfDoc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(pdfDoc, new FileStream(pdfOutputPath, FileMode.OpenOrCreate));
            pdfDoc.Open();


            // Draw the header components
            DrawHeader(pdfDoc, bankName, toName);
            DrawInterestStatementHeader(pdfDoc, bankName, toName, startDate, endDate, openingBalance, calculationType, indexName, dayCountConvention);

            // Draw the table components in ascending order
            var sortedBalanceItems = (balanceItems ?? new List<DailyBalance>()).OrderBy(x => x.Date).ToList();
            DrawBalanceItemsTable(pdfDoc, sortedBalanceItems);

            // TODO: Get the dueTo and netInterest amounts. Assumed to be final entry of accrued interest for now
            var dueTo = sortedBalanceItems.Last().AccruedInterest;
            var netInterest = sortedBalanceItems.Last().AccruedInterest;
            DrawBalanceItemsTableFooter(pdfDoc, dueTo, netInterest, toName);

            // Close the document
            pdfDoc.Close();
        }
        #endregion

        #region Private Methods
        private void DrawHeader(Document pdfDoc, string bankName, string toName)
        {
            // Draw the bank name at the top
            var bankNameParagraph = new Paragraph(string.Format("{0}\n\n", bankName), SECTION_HEADER_FONT);
            pdfDoc.Add(bankNameParagraph);

            // Create a table for the 'To', 'Contacts', and 'Calculated At' so the items in all columns can be left aligned with each other.
            // The first column will always be bold and the second column will not be.
            PdfPTable table = new PdfPTable(new float[] { .3f, .7f });
            table.HorizontalAlignment = 0;

            // Row 1, 'To:' {toName}
            AddCellToTable(table, "To:", BODY_BOLD_FONT);
            AddCellToTable(table, bankName, BODY_FONT);

            // Row 2, 'Contact(s):' {EMPTY FOR NOW?}
            // TODO: Do you want this value always empty? if not, pass in a value to the method
            AddCellToTable(table, "Contact(s):", BODY_BOLD_FONT);
            AddCellToTable(table, "", BODY_FONT);

            // Row 3, 'Calculated at:' {Nows Date}
            AddCellToTable(table, "Calculated at:", BODY_BOLD_FONT);
            AddCellToTable(table, DateTime.Now.ToString("MM/dd/yyyy H:mm "), BODY_FONT);

            // Add the table to the current document
            pdfDoc.Add(table);
        }

        private void DrawInterestStatementHeader(Document pdfDoc, string bankName, string toName, DateTime startDate, DateTime endDate, decimal openingBalance, string calculationType, string indexName, string dayCountConvention)
        {
            // Draw 'Interest Statement' header
            var interestHeaderParagraph = new Paragraph(string.Format("\n{0}\n\n", "Interest Statement"), SECTION_HEADER_FONT);
            pdfDoc.Add(interestHeaderParagraph);

            #region Calculation Details
            // Create a table for the calculation details so all of the text can be aligned correctly
            PdfPTable table = new PdfPTable(new float[] { .4f, .6f });
            table.HorizontalAlignment = 0;

            // Put the 'Calculation Details...' text and have it span all rows
            AddCellToTable(table, string.Format("Calculation Details between {0} and {1}", bankName, toName), BODY_BOLD_FONT, 0, 2);

            // Row 1: Distribution method
            AddCellToTable(table, "Distribution Method:", BODY_FONT);
            AddCellToTable(table, "Distribute", BODY_FONT);

            // Row 2: Calculation Type
            AddCellToTable(table, "Calculation Type:", BODY_FONT);
            AddCellToTable(table, calculationType, BODY_FONT);

            // Row 3: Index
            AddCellToTable(table, "Index:", BODY_FONT);
            AddCellToTable(table, indexName, BODY_FONT);

            // Row 4: Day Count Convention
            AddCellToTable(table, "Day Count Convention:", BODY_FONT);
            AddCellToTable(table, dayCountConvention, BODY_FONT);

            // Row 5: Disclaimer
            AddCellToTable(table, "Values displayed from sender's perspective", BODY_FONT, 0, 2);

            pdfDoc.Add(table);
            #endregion

            #region Cash Held
            // Add some spacing before this section
            pdfDoc.Add(new Paragraph("\n", BODY_FONT));

            // Center align the USD Cash Held and Previous period closing notes
            var cashHeldParagraph = new Paragraph(string.Format("USD Cash Held for Variation Collateral between {0} to {1}", 
                startDate.ToString("dd MMM yyyy"), endDate.ToString("dd MMM yyyy")), BODY_FONT);
            cashHeldParagraph.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(cashHeldParagraph);

            var previousHoldingParagraph = new Paragraph(string.Format("Previous Period Closing Held Balance: {0}",
                openingBalance.ToString("N0")), BODY_FONT);
            previousHoldingParagraph.Alignment = Element.ALIGN_CENTER;
            pdfDoc.Add(previousHoldingParagraph);
            #endregion
        }
                    
        private void DrawBalanceItemsTable(Document pdfDoc, List<DailyBalance> balanceItems)
        {
            // Add some spacing before the table
            pdfDoc.Add(new Paragraph("\n", BODY_FONT));

            // Setup the table
            PdfPTable balanceItemsTable = new PdfPTable(8);
            balanceItemsTable.HorizontalAlignment = Element.ALIGN_LEFT;
            balanceItemsTable.HeaderRows = 1;
            balanceItemsTable.WidthPercentage = 100f;

            // Add the table header
            AddCellToTable(balanceItemsTable, "Date", TABLE_BOLD_FONT, 0, 1, Element.ALIGN_CENTER);
            AddCellToTable(balanceItemsTable, "Movements", TABLE_BOLD_FONT, 0, 1, Element.ALIGN_RIGHT);
            AddCellToTable(balanceItemsTable, "End of Day Balance", TABLE_BOLD_FONT, 0, 1, Element.ALIGN_RIGHT);
            AddCellToTable(balanceItemsTable, "Effective Net Balance", TABLE_BOLD_FONT, 0, 1, Element.ALIGN_RIGHT);
            AddCellToTable(balanceItemsTable, "Index Rate", TABLE_BOLD_FONT, 0, 1, Element.ALIGN_RIGHT);
            AddCellToTable(balanceItemsTable, "Spread", TABLE_BOLD_FONT, 0, 1, Element.ALIGN_RIGHT);
            AddCellToTable(balanceItemsTable, "Daily Interest", TABLE_BOLD_FONT, 0, 1, Element.ALIGN_RIGHT);
            AddCellToTable(balanceItemsTable, "Accrued Interest", TABLE_BOLD_FONT, 0, 1, Element.ALIGN_RIGHT);


            // Add each item
            foreach (var curBalanceItem in balanceItems)
            {
                AddCellToTable(balanceItemsTable, curBalanceItem.Date.ToString("dd/MM/yyyy ddd"), TABLE_FONT, 0, 1, Element.ALIGN_LEFT);
                AddCellToTable(balanceItemsTable, 0.ToString("N0"), TABLE_FONT, 0, 1, Element.ALIGN_RIGHT);
                AddCellToTable(balanceItemsTable, 0.ToString("N0"), TABLE_FONT, 0, 1, Element.ALIGN_RIGHT);
                AddCellToTable(balanceItemsTable, 0.ToString("N0"), TABLE_FONT, 0, 1, Element.ALIGN_RIGHT);
                AddCellToTable(balanceItemsTable, 0.ToString("N0"), TABLE_FONT, 0, 1, Element.ALIGN_RIGHT);
                AddCellToTable(balanceItemsTable, 0.ToString("N0"), TABLE_FONT, 0, 1, Element.ALIGN_RIGHT);
                AddCellToTable(balanceItemsTable, 0.ToString("N0"), TABLE_FONT, 0, 1, Element.ALIGN_RIGHT);
                AddCellToTable(balanceItemsTable, curBalanceItem.AccruedInterest.ToString("N0"), TABLE_FONT, 0, 1, Element.ALIGN_RIGHT);


            }

            pdfDoc.Add(balanceItemsTable);
        }

        private void DrawBalanceItemsTableFooter(Document pdfDoc, decimal dueTo, decimal netInterest, string toName)
        {
            // Add some spacing before the footer
            pdfDoc.Add(new Paragraph("\n\n", BODY_FONT));

            // Put all of the footer items into a table so values align correctly. Right align the table
            PdfPTable balanceItemsFooterTable = new PdfPTable(new float[] { .35f, .5f, .15f });
            balanceItemsFooterTable.HorizontalAlignment = Element.ALIGN_RIGHT;

            // Add the due amount
            AddCellToTable(balanceItemsFooterTable, "", BODY_BOLD_FONT);
            AddCellToTable(balanceItemsFooterTable, string.Format("Due to {0}", toName), BODY_BOLD_FONT);
            AddCellToTable(balanceItemsFooterTable, dueTo.ToString("N0"), BODY_UNDERLINE_FONT, 0, 1, Element.ALIGN_RIGHT);

            // Add a space
            AddCellToTable(balanceItemsFooterTable, " ", BODY_BOLD_FONT, 0, 3);

            // Add the net interest
            AddCellToTable(balanceItemsFooterTable, "", BODY_BOLD_FONT);
            AddCellToTable(balanceItemsFooterTable, "Net Interest Amount", BODY_BOLD_FONT);
            AddCellToTable(balanceItemsFooterTable, "", BODY_BOLD_FONT);

            AddCellToTable(balanceItemsFooterTable, "", BODY_BOLD_FONT);
            AddCellToTable(balanceItemsFooterTable, string.Format("Due to {0}", toName), BODY_BOLD_FONT);
            AddCellToTable(balanceItemsFooterTable, netInterest.ToString("N0"), BODY_UNDERLINE_FONT, 0, 1, Element.ALIGN_RIGHT);

            pdfDoc.Add(balanceItemsFooterTable);
        }
        #endregion

        #region PDF Helper Methods
        private void AddCellToTable(PdfPTable tableToAddTo, string textToPrint, Font font, int border = 0, int colSpan = 1, int horizontalAlignment = Element.ALIGN_LEFT)
        {
            PdfPCell cellToAdd = new PdfPCell(new Phrase(textToPrint, font));
            cellToAdd.Border = border;
            cellToAdd.Colspan = colSpan;
            cellToAdd.HorizontalAlignment = horizontalAlignment;
            tableToAddTo.AddCell(cellToAdd);
        }
        #endregion
    }
}
