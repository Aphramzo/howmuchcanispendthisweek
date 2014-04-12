using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
                    Moneys = CategoryHelper.AvailableMoneyForCategory(category.CategoryName),
					Transactions = TransactionHelper.GetLatestTransactions(category)
                });
            }
            ViewBag.Message = "Derp Derp Money";

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddTransaction(string amount, int category, string merchant)
        {
            var goodAmount = Convert.ToDecimal(amount);
	        SqlHelper.ExecuteNonReader("sp_AddTransaction", new List<SqlParameter>
	        {
		        new SqlParameter("@categoryId",category),
		        new SqlParameter("@amount",goodAmount),
		        new SqlParameter("@merchant",merchant),
		        new SqlParameter("@memo","added Through web before interface allowed memos")
	        });
            return RedirectToAction("index");
        }

        

        private void InsertAutoTransfers()
        {
            var connection = SqlHelper.GetConnection();
            connection.Open();
			var reader = SqlHelper.GetReader(connection, "sp_GetLastAutotransferDate", null);
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
                SqlHelper.ExecuteNonReader("sp_DailyTransfer", new List<SqlParameter>
                {
	                new SqlParameter("@transactionDate",lastDate.AddDays(i))
                });
            }
        }
    }
}
