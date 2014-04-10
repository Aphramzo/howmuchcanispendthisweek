using System;
using System.Collections.Generic;
using System.Web.Mvc;
using HowMuchCanISpend.Models;
using HowMuchCanISpend.Util;
using HowMuchCanISpend.Helpers;
namespace HowMuchCanISpend.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            InsertAutoTransfers();
            var categories = CategoryHelper.GetActiveCategories();
            var viewModel = new HomeViewModel();
            viewModel.CategoryDisplays = new List<CategoryDisplay>();
            foreach(var category in categories){
                viewModel.CategoryDisplays.Add(new CategoryDisplay{
                    Category = category,
                    Moneys = CategoryHelper.AvailableMoneyForCategory(category.CategoryName)
                });
            }
            ViewBag.Message = "Derp Derp Money";

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddTransaction(string amount, int category, string merchant)
        {
            var goodAmount = Convert.ToDecimal(amount);
            SqlHelper.ExecuteNonReader(String.Format("[sp_AddTransaction] {0}, {1}, '{2}','{3}'", category, goodAmount, merchant, "added Through web before interface allowed memos"));
            return RedirectToAction("index");
        }

        

        private void InsertAutoTransfers()
        {
            var connection = SqlHelper.GetConnection();
            connection.Open();
            var reader = SqlHelper.GetReader(connection,"select max(transactiondate) from transactions where memo = 'autotransfer'");
            // Data is accessible through the DataReader object here.
            var lastDate = DateTime.Now;
            while (reader.Read())
            {
                lastDate = (DateTime)reader[0];
            }
            reader.Close();
            connection.Close();
            for (var i = 1; i <= (DateTime.Now - lastDate).TotalDays;i++)
            {
                SqlHelper.ExecuteNonReader(String.Format("[sp_DailyTransfer] '{0}'", lastDate.AddDays(i)));
            }
        }
    }
}
