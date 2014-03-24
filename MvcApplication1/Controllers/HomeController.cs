using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            InsertAutoTransfers();
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        private void InsertAutoTransfers()
        {
            SqlConnection sqlConnection1 = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["SQLSERVER_CONNECTION_STRING"].ConnectionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader reader;

            cmd.CommandText = "select max(transactiondate) from transactions where memo = 'autotransfer'";
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;

            sqlConnection1.Open();

            reader = cmd.ExecuteReader();
            // Data is accessible through the DataReader object here.
            var lastDate = DateTime.Now;
            while (reader.Read())
            {
                lastDate = ReadSingleRow(reader);
            }
            reader.Close();
            for (var i = 1; i <= (DateTime.Now - lastDate).TotalDays;i++)
            {
                cmd.CommandText = String.Format("[sp_DailyTransfer] '{0}'", lastDate.AddDays(i));
                cmd.CommandType = CommandType.Text;
                cmd.Connection = sqlConnection1;

                cmd.ExecuteNonQuery();
            }
            sqlConnection1.Close();
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
