using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO: Setup output path to the PDF i.e.: "C:\test.pdf"
            string pathToFile = "";

            // Create the generator and run the report. The code below will generate the report with 31 rows and contain the headers that were shown on your initial copy.
            var generator = new ReportGenerator();
            generator.GenerateReport(
                pathToFile,
                "Principal Bank",
                "Big Bank Ltd",
                DateTime.Now.AddDays(-3),
                DateTime.Now,
                0, 
                new List<DailyBalance>()
                {
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10001, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10002, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10003, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10004, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10005, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10006, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10007, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10008, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10009, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10000, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10001, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10002, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10003, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10004, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10005, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10006, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10007, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10008, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10009, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10000, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10001, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10002, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10003, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10004, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10005, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10006, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10007, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10008, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10009, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10000, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                    new DailyBalance(DateTime.Now, 100) { AccruedInterest = 10001, CalculatedInterest = 20000, ClosingBalance = 30000, Date = DateTime.Now, InterestRate = 50000 },
                },
                "Simple",
                "FedFunds",
                "Act/360"
                );
        }
    }
}
