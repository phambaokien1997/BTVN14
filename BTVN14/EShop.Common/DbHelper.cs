using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Common
{
    public static class DbHelper
    {
        public static SqlConnection GetSqlConnection()
        {
            string connectionString = "Data Source=BKS-20240209BOY;Initial Catalog=BTVN14;Integrated Security=True;";
            SqlConnection sqlConnection = new SqlConnection(connectionString);

            try
            {
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                return sqlConnection;
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Lỗi khi mở kết nối cơ sở dữ liệu: {ex.Message}");
                throw; // Ném lại ngoại lệ để báo lỗi và dừng chương trình (nếu cần)
            }
            finally
            {
                if (sqlConnection.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection.Close();
                }
            }
        }
    }
}
