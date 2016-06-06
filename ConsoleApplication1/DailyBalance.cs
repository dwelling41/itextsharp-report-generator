using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    public class DailyBalance
    {
        public DailyBalance(DateTime Date, decimal ClosingBalance)
        {
            this.Date = Date;
            this.ClosingBalance = ClosingBalance;
        }

        public DateTime Date { get; set; }
        public decimal ClosingBalance { get; set; }
        public decimal CalculatedInterest { get; set; }
        public decimal AccruedInterest { get; set; }
        public decimal InterestRate { get; set; }
    }
}
