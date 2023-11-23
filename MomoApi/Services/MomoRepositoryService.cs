using System;
using MiddlewareAuth.Models.Models;
using static MiddlewareAuth.Models.Models.MobileMoney;
using System.Data.SqlClient;
using System.Text.Json;
using MySql.Data.MySqlClient;

namespace MomoApi.Services
{
	public class MomoRepositoryService
	{
        DatabaseConnector databaseConnector = new DatabaseConnector();
        ConfigurationManager configurationManager = new ConfigurationManager();
        public ReturnMessage addMomoPaymentToDb(MomoPaymentToDb payment)
        {
            string connectionString = databaseConnector.connectionString;
            ReturnMessage message = new ReturnMessage();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    int rowsAffected = 0;
                    string query = "INSERT INTO momo_payment(request,response,provider,paymentId,token,status,intent,merchantId,akidiReference) VALUES (@request,@response,@Provider,@paymentId,@token,@status,@intent,@merchantId,@akidiReference)";
                    if (databaseConnector.OpenConnection())
                    {
                        MySqlCommand cmd = new MySqlCommand(query, databaseConnector.connection);

                        cmd.Parameters.AddWithValue("@request", JsonSerializer.Serialize(payment.request));
                        cmd.Parameters.AddWithValue("@response", JsonSerializer.Serialize(payment.response));
                        cmd.Parameters.AddWithValue("@token", payment.response.token);
                        cmd.Parameters.AddWithValue("@Provider", configurationManager.GetConnectionString("MomoProvider").ToString());
                        cmd.Parameters.AddWithValue("@merchantId", payment.merchantId);
                        cmd.Parameters.AddWithValue("@intent", JsonSerializer.Serialize(payment.intent));
                        cmd.Parameters.AddWithValue("@paymentId", payment.response.id);
                        cmd.Parameters.AddWithValue("@status", payment.status);
                        cmd.Parameters.AddWithValue("@akidiReference", payment.akidiReference);

                        try
                        {
                            rowsAffected = cmd.ExecuteNonQuery();
                            message.code = System.Net.HttpStatusCode.OK;
                            message.message = "Transaction logged successfully";
                        }
                        catch (MySqlException ex)
                        {
                            Console.WriteLine("Error occured during insertion : " + ex.Message);
                            message.code = System.Net.HttpStatusCode.InternalServerError;
                            message.message = ex.Message;
                        }
                    }
                    databaseConnector.CloseConnection();
                    return message;
                }
            }
            catch (Exception ex)
            {
                message.message = ex.Message;
                message.code = System.Net.HttpStatusCode.NotAcceptable;
            }
            return message;
        }

        public ReturnMessage addMomoRequestToDb(MomoTransfert transfert)
        {
            string connectionString = databaseConnector.connectionString;
            ReturnMessage message = new ReturnMessage();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    int rowsAffected = 0;
                    string query = "INSERT INTO momo_transactions(request,response,transactionType,Provider,createdAt,momoTransId,akidiReference,merchantId) VALUES (@request,@response,@transactionType,@Provider,@createdAt,@momoTransId,@akidiReference,@merchantId)";
                    if (databaseConnector.OpenConnection())
                    {
                        MySqlCommand cmd = new MySqlCommand(query, databaseConnector.connection);

                        cmd.Parameters.AddWithValue("@request", JsonSerializer.Serialize(transfert.request));
                        cmd.Parameters.AddWithValue("@response", JsonSerializer.Serialize(transfert.response));
                        cmd.Parameters.AddWithValue("@transactionType", transfert.transactionType);
                        cmd.Parameters.AddWithValue("@Provider", configurationManager.GetConnectionString("MomoProvider").ToString());
                        cmd.Parameters.AddWithValue("@createdAt", transfert.response.createdAt);
                        cmd.Parameters.AddWithValue("@momoTransId", transfert.response.id);
                        cmd.Parameters.AddWithValue("@akidiReference", transfert.akidiReference);
                        cmd.Parameters.AddWithValue("@merchantId", transfert.request.merchantId);


                        try
                        {
                            rowsAffected = cmd.ExecuteNonQuery();
                            message.code = System.Net.HttpStatusCode.OK;
                            message.message = "Transaction logged successfully";
                        }
                        catch (MySqlException ex)
                        {
                            Console.WriteLine("Error occured during insertion : " + ex.Message);
                            message.code = System.Net.HttpStatusCode.InternalServerError;
                            message.message = ex.Message;
                        }
                    }
                    databaseConnector.CloseConnection();
                    return message;
                }
            }
            catch (Exception ex)
            {
                message.message = ex.Message;
                message.code = System.Net.HttpStatusCode.NotAcceptable;
            }
            return message;

        }

        public ReturnMessage getMomoTransactionFromDb()
        {
            string connectionString = databaseConnector.connectionString;
            ReturnMessage message = new ReturnMessage();
            return message;
        }
    }
}

