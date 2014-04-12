using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HowMuchCanISpend.Models
{
    public class Category : BaseModel
    {
        public string CategoryName { get; set; }

        public decimal WeeklyAmount { get; set; }

		public decimal DailyAmount { get; set; }
    }
}