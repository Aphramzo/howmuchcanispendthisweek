using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HowMuchCanISpend.Models
{
	public class CategoryTransactionsDisplay
	{
		public Category Category { get; set; }
		public IList<Transaction> Transactions { get; set; } 
	}
}