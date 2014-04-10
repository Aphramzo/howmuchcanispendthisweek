using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HowMuchCanISpend.Models
{
	public class TransactionViewModel
	{
		public IList<CategoryTransactionsDisplay> CategoryTransactionDisplays { get; set; } 
	}
}