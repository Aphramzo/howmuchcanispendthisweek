using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HowMuchCanISpend.Util
{
    public static class SqlHelper
    {
        public static SqlDataReader GetReader(SqlConnection connection, string storedProc, List<SqlParameter> sqlParameters)
        {
	        var sqlParams = sqlParameters ?? new List<SqlParameter>();
            var cmd = GetCommand(connection, storedProc);
	        foreach (var parameter in sqlParams)
	        {
		        cmd.Parameters.Add(parameter);
	        }

            var reader =  cmd.ExecuteReader();

            return reader;
        }

        public static SqlConnection GetConnection()
        {
            SqlConnection sqlConnection1 =
                new SqlConnection(
                    System.Configuration.ConfigurationManager.ConnectionStrings["SQLSERVER_CONNECTION_STRING"].ConnectionString);
            return sqlConnection1;
        }

        private static SqlCommand GetCommand(SqlConnection sqlConnection1, string storedProc)
        {
            SqlCommand cmd = new SqlCommand(storedProc, sqlConnection1);
            cmd.CommandType = CommandType.StoredProcedure;
            return cmd;
        }

        public static void ExecuteNonReader(string storedProc, List<SqlParameter> sqlParameters )
        {
			var sqlParams = sqlParameters ?? new List<SqlParameter>();
			var sqlConnection = GetConnection();
            var cmd = GetCommand(sqlConnection, storedProc);
			foreach (var parameter in sqlParams)
			{
				cmd.Parameters.Add(parameter);
			}
            sqlConnection.Open();

            cmd.ExecuteNonQuery();

            sqlConnection.Close();
        }
    }
}