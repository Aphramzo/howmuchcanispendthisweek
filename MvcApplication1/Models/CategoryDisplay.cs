using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class CategoryDisplay
    {
        public Category Category { get; set; }

        public decimal Moneys { get; set; }

        public int Percent
        {
            get
            {
                 return Convert.ToInt16(Moneys / Category.WeeklyAmount *100);
            }
        }
    }
}