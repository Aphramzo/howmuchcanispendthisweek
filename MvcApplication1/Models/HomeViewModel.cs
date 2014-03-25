using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class HomeViewModel
    {
        public string GroceryMoney { get; set; }
        public string EatingOutMoney { get; set; }
        public string SpendingMoneys { get; set; }

        public int GroceryPercent
        {
            get { return Convert.ToInt16(Convert.ToDecimal(GroceryMoney) / 87 * 100); }
        }

        public int EatingOutPercent
        {
            get { return Convert.ToInt16(Convert.ToDecimal(EatingOutMoney) / 81*100); }
        }

        public int SpendingPercent
        {
            get { return Convert.ToInt16(Convert.ToDecimal(SpendingMoneys) / 161*100); }
        }
    }
}