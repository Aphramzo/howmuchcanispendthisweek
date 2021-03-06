﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HowMuchCanISpend.Models;
using HowMuchCanISpend.Util;

namespace HowMuchCanISpend.Helpers
{
    public static class CategoryHelper
    {
        public static IList<Category> GetActiveCategories()
        {
            var connection = SqlHelper.GetConnection();
            connection.Open();
            var reader = SqlHelper.GetReader(connection, "sp_GetCategoriesForDisplay", null);
            // Data is accessible through the DataReader object here.
            var categories = new List<Category>();
            while (reader.Read())
            {
                categories.Add(new Category
                {
                    CategoryName = reader[1].ToString(),
                    Id = Convert.ToInt64(reader[0]),
                    WeeklyAmount = Convert.ToDecimal(reader[2]),
					DailyAmount = Convert.ToDecimal(reader[2])/7
                });
            }
            reader.Close();
            connection.Close();
            return categories;
        }

        public static decimal AvailableMoneyForCategory(string category)
        {
            var connection = SqlHelper.GetConnection();
            connection.Open();
            var reader = SqlHelper.GetReader(connection, "sp_AvailableMoneyByCategory", new List<SqlParameter>
            {
	            new SqlParameter("@categoryName",category)
            });
            reader.Read();
            var amount = Convert.ToDecimal(reader[0]);
            reader.Close();
            connection.Close();
            return amount;
        }
    }
}