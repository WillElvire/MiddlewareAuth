using System;
using MomoApi.Services;
using MySql.Data.MySqlClient;

namespace MomoApi.Utils
{
	public class Utils
	{
		public Utils()
		{
		}

        


        public static int SaveLog(string controller, string end_point_name, string data)
        {
#if DEBUG
            return 1;
#endif
            DatabaseConnector databaseConnector = new DatabaseConnector();

            databaseConnector.OpenConnection();

            int rowsAffected = 0;
            string query = null;


            query = $"INSERT INTO app_log (controller,end_point_name,data,insert_date) VALUES (@controller,@end_point_name,@data,@insert_date)";

            MySqlCommand cmd = new MySqlCommand(query, databaseConnector.connection);
            cmd.Parameters.AddWithValue("@controller", controller);
            cmd.Parameters.AddWithValue("@end_point_name", end_point_name);
            cmd.Parameters.AddWithValue("@data", data);
            cmd.Parameters.AddWithValue("@insert_date", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));//YYYY - MM - DD HH: MM: SS



            try
            {
                rowsAffected = cmd.ExecuteNonQuery();

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error occured during insertion : " + ex.Message);
            }



            databaseConnector.CloseConnection();

            return rowsAffected;


        }

    }
}

