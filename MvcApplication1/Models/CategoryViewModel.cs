using System.Collections.Generic;
using System.Linq;

namespace HowMuchCanISpend.Models
{
	public class CategoryViewModel
	{
		public IList<Category> Categories { get; set; }

		public decimal WeeklyAllowence
		{
			get { return Categories.Sum(x => x.WeeklyAmount); }
		}

		public decimal DailyAllowence
		{
			get { return Categories.Sum(x => x.DailyAmount); }
		}
	}
}