using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcApplication1.Models;
using MvcApplication1.Util;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            InsertAutoTransfers();
            var categories = GetActiveCategories();
            var viewModel = new HomeViewModel();
            viewModel.CategoryDisplays = new List<CategoryDisplay>();
            foreach(var category in categories){
                viewModel.CategoryDisplays.Add(new CategoryDisplay{
                    Category = category,
                    Moneys = AvailableMoneyForCategory(category.CategoryName)
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

        private IList<Category> GetActiveCategories()
        {
            var connection = SqlHelper.GetConnection();
            connection.Open();
            var reader = SqlHelper.GetReader(connection, "sp_GetCategoriesForDisplay");
            // Data is accessible through the DataReader object here.
            var categories = new List<Category>();
            while (reader.Read())
            {
                categories.Add(new Category{
                    CategoryName = reader[1].ToString(),
                    Id = Convert.ToInt64(reader[0]),
                    WeeklyAmount = Convert.ToDecimal(reader[2])
                });
            }
            reader.Close();
            connection.Close();
            return categories;
        }

        private decimal AvailableMoneyForCategory(string category)
        {
            var connection = SqlHelper.GetConnection();
            connection.Open();
            var reader = SqlHelper.GetReader(connection,"sp_AvailableMoneyByCategory '" + category + "'");
            reader.Read();
            var amount = Convert.ToDecimal(reader[0]);
            reader.Close();
            connection.Close();
            return amount;
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
                lastDate = ReadSingleRow(reader);
            }
            reader.Close();
            connection.Close();
            for (var i = 1; i <= (DateTime.Now - lastDate).TotalDays;i++)
            {
                SqlHelper.ExecuteNonReader(String.Format("[sp_DailyTransfer] '{0}'", lastDate.AddDays(i)));
            }
        }

        private DateTime ReadSingleRow(IDataRecord reader)
        {
            return (DateTime) reader[0];
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
