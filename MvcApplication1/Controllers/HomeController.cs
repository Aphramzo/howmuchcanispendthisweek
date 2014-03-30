﻿using System;
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
            var viewModel = new HomeViewModel
            {
                GroceryMoney = AvailableGroceryMoney(),
                EatingOutMoney = AvailableEatingOutMoney(),
                SpendingMoneys = AvailableSpendingMoney()
            };
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult AddTransaction(string amount, int category, string merchant)
        {
            var goodAmount = Convert.ToDecimal(amount);
            //oh dear god, what kind of human being would put a minus like this in a string for this.
            //a drunk lazy kind. Sorry future Tyler. I want to kick my ass only slightly less than I want to 
            //go get another beer. You get it.
            SqlHelper.ExecuteNonReader(String.Format("[sp_AddTransaction] {0}, -{1}, '{2}','{3}'", category, goodAmount, merchant, "added Through web before interface allowed memos"));
            return RedirectToAction("index");
        }

        private decimal AvailableGroceryMoney()
        {
            return AvailableMoneyForCategory("Groceries");
        }

        private decimal AvailableEatingOutMoney()
        {
            return AvailableMoneyForCategory("Eating Out");
        }

        private decimal AvailableSpendingMoney()
        {
            return AvailableMoneyForCategory("Spending Moneys");
        }

        private decimal AvailableMoneyForCategory(string category)
        {
            var connection = SqlHelper.GetConnection();
            connection.Open();
            var reader = SqlHelper.GetReader(connection,"AvailableMoneyByCategory '" + category + "'");
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
