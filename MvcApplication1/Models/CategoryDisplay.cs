using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HowMuchCanISpend.Models
{
    public class CategoryDisplay
    {
        public Category Category { get; set; }

        public decimal Moneys { get; set; }

        public int Percent
        {
            get
            {
                return  Math.Abs(Convert.ToInt16(Moneys/Category.WeeklyAmount*100));
            }
        }

		public IList<Transaction> Transactions { get; set; } 
    }
}