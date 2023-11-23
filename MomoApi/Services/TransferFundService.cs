using System;
using MiddlewareAuth.Models.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace MomoApi.Services
{
	public class TransferFundService
	{
        ConfigurationManager configurationManager = new ConfigurationManager();

        public TransferFundService()
		{
		}

        public string transferFundService(TransferPayload transferPayload)
        {
            DatabaseConnector databaseConnector = new DatabaseConnector();
            string connectionString = configurationManager.GetConnectionString("ConString").ToString();
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "CALL `TransferFunds_Wallet`(" + 1 + ", " + transferPayload.senderID + ", " + transferPayload.receiverID + ", " + transferPayload.amount + ")";
                using (MySqlCommand command = connection.CreateCommand())
                {
                    try
                    {
                        command.CommandText = query;
                        command.CommandType = CommandType.Text;
                        string x = command.ExecuteScalar().ToString();
                        return x;
                    }
                    catch (Exception ex)
                    {
                        return ex.Message;
                    }
                }
            }
        }

    }
}

