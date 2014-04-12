using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using HowMuchCanISpend.Models;
using HowMuchCanISpend.Util;

namespace HowMuchCanISpend.Helpers
{
	public static class TransactionHelper
	{
		public static IList<Transaction> GetLatestTransactions(Category category)
		{
			var transactions = new List<Transaction>();
			var connection = SqlHelper.GetConnection();
			connection.Open();
			var reader = SqlHelper.GetReader(connection, "sp_TransactionsGetByCategoryLastTen", new List<SqlParameter>
			{
				new SqlParameter("@categoryId",category.Id)
			});
			while (reader.Read())
			{
				transactions.Add(new Transaction
				{
					TransactionDate = Convert.ToDateTime(reader["TransactionDate"]),
					Amount = Convert.ToDecimal(reader["Amount"]),
					Location = Convert.ToString(reader["Merchant"]),
					Memo = Convert.ToString(reader["Memo"])
				});
			}
			reader.Close();
			connection.Close();
			return transactions;
		}
	}
}