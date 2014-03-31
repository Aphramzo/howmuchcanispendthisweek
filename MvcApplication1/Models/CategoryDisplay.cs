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
                var percent = Convert.ToInt16(Moneys/Category.WeeklyAmount*100);
                return percent < 0 ? 0 : percent;
            }
        }
    }
}