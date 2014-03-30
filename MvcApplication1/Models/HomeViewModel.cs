using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class HomeViewModel
    {
        public decimal GroceryMoney { get; set; }
        public decimal EatingOutMoney { get; set; }
        public decimal SpendingMoneys { get; set; }

        public int GroceryPercent
        {
            get { return Convert.ToInt16(GroceryMoney / 87 * 100); }
        }

        public int EatingOutPercent
        {
            get { return Convert.ToInt16(EatingOutMoney / 81*100); }
        }

        public int SpendingPercent
        {
            get { return Convert.ToInt16(SpendingMoneys / 161*100); }
        }
    }
}