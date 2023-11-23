using System;
using System.Data.SqlClient;
using MomoApi.Utils;
using MySql.Data.MySqlClient;

namespace MomoApi.Services
{
	public class DatabaseConnector
	{
        public MySqlConnection connection;
        public string connectionString;

        Config config = new Config();

        public DatabaseConnector()
        {
            string connectionString = config.configuration.GetConnectionString("akidiCnStr");

            //connectionString = ConfigurationManager.AppSettings["ConString"].ToString();
            connection = new MySqlConnection(connectionString);
        }

        //public DatabaseConnector()
        //{
        //    connectionString = ConfigurationManager.AppSettings["ConString"].ToString();
        //    connection = new MySqlConnection(connectionString);
        //}

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;

            }
            catch (MySqlException ex)

            {
                Console.WriteLine("Erreur lors de l'ouverture de la connexion : " + ex.Message);
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Erreur lors de la fermeture de la connexion : " + ex.Message);
                return false;
            }
        }

        public void PerformSelectQuery(string query)
        {
            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = cmd.ExecuteReader();


                while (dataReader.Read())
                {
                    string value1 = dataReader.GetString(0);
                    int value2 = dataReader.GetInt32(1);

                    Console.WriteLine($"Colonne 1 : {value1}, Colonne 2 : {value2}");
                }

                dataReader.Close();
                CloseConnection();
            }
        }

        public int InsertData(string tableName, string column1Value, int column2Value)
        {
            int rowsAffected = 0;
            string query = $"INSERT INTO {tableName} (Column1, Column2) VALUES (@Column1Value, @Column2Value)";

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Column1Value", column1Value);
                cmd.Parameters.AddWithValue("@Column2Value", column2Value);

                try
                {
                    rowsAffected = cmd.ExecuteNonQuery();

                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error occured during insertion : " + ex.Message);
                }

                CloseConnection();
            }
            return rowsAffected;
        }

        // Dynamic Select
        public int InsertDataDynamic(string tableName, string column1Value, int column2Value)
        {
            int rowsAffected = 0;
            string query = $"INSERT INTO {tableName} (Column1, Column2) VALUES (@Column1Value, @Column2Value)";

            if (OpenConnection())
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Parameters.AddWithValue("@Column1Value", column1Value);
                cmd.Parameters.AddWithValue("@Column2Value", column2Value);

                try
                {
                    rowsAffected = cmd.ExecuteNonQuery();
                }
                catch (MySqlException ex)
                {
                    Console.WriteLine("Error occured during insertion: " + ex.Message);
                }

                CloseConnection();
            }
            return rowsAffected;
        }
    }
}

