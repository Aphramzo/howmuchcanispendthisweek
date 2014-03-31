using System.Data;
using System.Data.SqlClient;

namespace HowMuchCanISpend.Util
{
    public static class SqlHelper
    {
        public static SqlDataReader GetReader(SqlConnection connection, string sqlCommand)
        {
            var cmd = GetCommand(connection);
            cmd.CommandText = sqlCommand;

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

        private static SqlCommand GetCommand(SqlConnection sqlConnection1)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection1;
            return cmd;
        }

        public static void ExecuteNonReader(string sqlCommand)
        {
            var sqlConnection1 = GetConnection();
            var cmd = GetCommand(sqlConnection1);
            cmd.CommandText = sqlCommand;


            sqlConnection1.Open();

            cmd.ExecuteNonQuery();

            sqlConnection1.Close();
        }
    }
}