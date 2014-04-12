using System.Collections.Generic;
using System.Web.Mvc;
using HowMuchCanISpend.Helpers;
using HowMuchCanISpend.Models;

namespace HowMuchCanISpend.Controllers
{
    public class TransactionController : Controller
    {
        //
        // GET: /Transaction/

        public ActionResult Index()
        {
	        var viewModel = new TransactionViewModel();
			viewModel.CategoryTransactionDisplays = new List<CategoryTransactionsDisplay>();
			var categories = CategoryHelper.GetActiveCategories();
	        foreach (var category in categories)
	        {
		        viewModel.CategoryTransactionDisplays.Add(new CategoryTransactionsDisplay
		        {
			        Category = category,
					Transactions = TransactionHelper.GetLatestTransactions(category)
		        });
	        }
            return View(viewModel);
        }

	    [HttpPost]
	    public void Delete(long id)
	    {
		    TransactionHelper.Delete(id);
	    }
    }
}
