using System.Web.Mvc;
using HowMuchCanISpend.Helpers;
using HowMuchCanISpend.Models;

namespace HowMuchCanISpend.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Index()
        {
	        var viewModel = new CategoryViewModel();
	        viewModel.Categories = CategoryHelper.GetActiveCategories();

            return View(viewModel);
        }

    }
}
