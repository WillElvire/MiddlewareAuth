using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Http;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using MiddlewareAuth.Models;

namespace MiddlewareAuth
{
    public class MerchantValidation : IMerchantValidation
    {
        private const string MERCHANT_ACCOUNT_ACTIVATED = "1";
        private const string MERCHANT_ACCOUNT_DEACTIVATED = "0";
        private string _payoutDBConString;

        public MerchantValidation(string payoutDBConString)
        {
            _payoutDBConString = payoutDBConString;
        }

        public int checkTransferActive(string merchantId)
        {
            if (string.IsNullOrEmpty(merchantId))
            {
                return 0;
            }

            try
            {
#if DEBUG
                return 1;
#endif

                SqlConnection conn = new SqlConnection(_payoutDBConString);
                SqlCommand commandSql = new SqlCommand("CheckTransferActive", conn);
                commandSql.CommandType = CommandType.StoredProcedure;
                commandSql.Parameters.AddWithValue("@merchantId", merchantId);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlDataReader reader = commandSql.ExecuteReader();
                if (!reader.HasRows)
                {
                    return 0;
                }



                reader.Close();
                conn.Close();
                return 1;
            }
            catch (Exception e)
            {
                LogHandler.WriteLog(e.Message);
                return 0;
            }
        }

        public string GetHashOf(string dataToBeHashed)
        {
            using SHA256 sha256Hash = SHA256.Create();
            byte[] sourceBytes = Encoding.UTF8.GetBytes(dataToBeHashed);
            byte[] hashBytes = sha256Hash.ComputeHash(sourceBytes);
            return BitConverter.ToString(hashBytes).Replace("-", String.Empty);
        }

        public string GetMerchantAccount(string apiKey, string apiSecret)
        {
            string merchandID = string.Empty;

            if (string.IsNullOrEmpty(apiKey) || string.IsNullOrEmpty(apiSecret))
            {
                return null;
            }


#if DEBUG
            merchandID = "bankAccountNumber|merchantId|IPAddress|merchantName|api_key|status";
            return merchandID;
#endif

            try
            {
                SqlConnection conn = new SqlConnection(_payoutDBConString);
                SqlCommand commandSql = new SqlCommand("GetMerchandDetails", conn);
                commandSql.CommandType = CommandType.StoredProcedure;
                commandSql.Parameters.AddWithValue("@apikey", apiKey);
                commandSql.Parameters.AddWithValue("@apisecret", apiSecret);
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                SqlDataReader reader = commandSql.ExecuteReader();
                if (!reader.HasRows)
                {
                    return null;
                }

                while (reader.Read())
                {

                    merchandID = reader["bankAccountNumber"].ToString() + "|" + //account_number
                                 reader["merchantId"].ToString() + "|" + //id
                                 reader["IPAddress"].ToString() + "|" +
                                 reader["merchantName"].ToString() + "|" +
                                 reader["api_key"].ToString() + "|" +
                                 //  reader["apiServicesConfig"].ToString() + "|" +
                                 reader["status"].ToString();
                }

                reader.Close();
                conn.Close();
                return merchandID;
            }
            catch (Exception e)
            {
                LogHandler.WriteLog(e.Message);
                return null;
            }
        }

        public transferResponse IsServiceActivatedForMerchantNew(string merchant)
        {
            //extract the merchant Id
            string merchantId = merchant.ToString().Split('|')[1].Trim();
            string merchantName = merchant.ToString().Split('|')[3].Trim();  // Merchant name

            int responseCheck = checkTransferActive(merchantId);
            if (string.IsNullOrEmpty(responseCheck.ToString()) || responseCheck != 1)
            {
                //LogHandler.WriteLog("\t|==> CALLING GET BANK LIST \n\t|==> MERCHANT NAME : " + merchantName + " \n\t|==> RESPONSE : SERVICE ACCESS IS NULL", "TRANSFER");
                return new transferResponse
                {
                    code = 401,
                    message = "ERROR_SERVICE_ACCESS",
                    description = "TRANSFER SERVICES NOT ACTIVATED ON YOUR PROFILE"
                };
            }

            return new transferResponse
            {
                code = 201,
                message = "SERVICE ACCESS GRANTED"
            };

        }

        public string IsUserAuthorized(string authorizationParameterFromHeader, string IncomingRequestIPAddress, string endPoint)
        {
            if (string.IsNullOrEmpty(authorizationParameterFromHeader))
            {
                return null;
            }
            var decodedAuthenticationToken = Encoding.UTF8.GetString(Convert.FromBase64String(authorizationParameterFromHeader));
            var usernamePasswordArray = decodedAuthenticationToken.Split(':');
            var username = usernamePasswordArray[0];
            var password = usernamePasswordArray[1];
            string merchant = GetMerchantAccount(username.ToString(), password.ToString());
            LogAPIAccessActivity(merchant, IncomingRequestIPAddress, endPoint);
            // If the merchant details could not be found 
            if (string.IsNullOrEmpty(merchant))
            {
                return null;
            }

            //  Get Merchant account number and status
            string merchantBankAccountNumber = merchant.Split('|')[0].Trim();
            string merchantAccountStatus = merchant.Split('|')[6].Trim();

            // merchant account status
            if (!string.IsNullOrEmpty(merchantAccountStatus) && merchantAccountStatus == MERCHANT_ACCOUNT_DEACTIVATED)
            {
                return null;
            }

            // Merchant Bank Account number is null or empty 
            if (string.IsNullOrEmpty(merchantBankAccountNumber))
            {
                return null;
            }

            // Get the list of merchant's configured Public IP Addresses
            string merchantIPAddressList = merchant.Split('|')[2].Trim();


            /*****************************************************************
             * 
             * check if Incoming IP Address is allow to access the Service
             * 
             * ****************************************************************/

            // check if merchant IP Addresses contains ';' means multiple IP Addresses
            if (merchantIPAddressList.Contains(";"))
            {

                // Get an Array of list of merchant Public IP Adresses
                string[] IPAdresses = merchantIPAddressList.Split(';');

                // If the incoming Public IP Address is not within the above array
                // the access is denied
                if (!IPAdresses.Contains(IncomingRequestIPAddress))
                {
                    return null;
                }

            }
            else
            { // else the merchant IP Adresses doesn't contains ';' means only one IP Address

                // check if incoming and current IP Address match
                if (string.Compare(merchantIPAddressList.ToLower(), IncomingRequestIPAddress.ToLower()) != 0)
                {
                    return null;
                }
            }

            return merchant;
        }

        public void LogAPIAccessActivity(string merchant, string IncomingRequestIPAddress, string endPoint)
        {
            string merchantName = (!string.IsNullOrEmpty(merchant)) ? merchant.ToString().Split('|')[3].Trim() : "NO RECORD FOUND";  // Merchant name
            string merchantIPAddress = (!string.IsNullOrEmpty(merchant)) ? merchant.Split('|')[2].Trim() : "NO RECORD FOUND";

            string LogAccessMessage = "[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] FROM_IP: " + IncomingRequestIPAddress.ToString() + " **** MERCHANT_NAME: " + merchantName + " **** SERVICE_CALLED: " + endPoint;

            // write the log file
            LogHandler.WriteLog(LogAccessMessage, "ACCESS_LOGS", true);
        }

        public (bool status, transferResponse response) ValidateTransfer(transferRequestToBank requestParam, string merchant)
        {
            string MerchantBankAccount = merchant.Split('|')[0].ToString().Trim(); // Bank Account 
            string MerchantID = merchant.Split('|')[1].ToString().Trim(); // Merchant ID
            string merchantName = merchant.ToString().Split('|')[3].Trim();  // Merchant name
            string merchantAPIKey = merchant.ToString().Split('|')[4].Trim();  // Merchant API Key
            string merchantServiceAccessConfig = merchant.Split('|')[5].ToString().Trim(); // Service Access Config for a merchant

            try
            {

                // check security Key 
                if (string.IsNullOrEmpty(requestParam.key))
                {
                    LogHandler.WriteLog("\t|==> CALLING TRANSFER \n\t|==> MERCHANT NAME : " + merchantName + " \n\t|==> DATA PASSED : " + JsonSerializer.Serialize(requestParam) + " \n\t|==> ERROR : SECURITY FOR TRANSACTION IS NULL OR EMPTY", "TRANSFER");
                    return (false, new TransferFundResponse
                    {
                        code = 717,
                        message = "ERROR_TRANSACTION_DATA_INVALID",
                        description = "SECURITY KEY FOR TRANSACTION IS NULL OR EMPTY"
                    });
                }

                /**
                * This Part Check the params 
                * return the params that are empty
                **/
                string[] ExcludedParams = new string[] { "sender_phone_number", "receiver_phone_number", "remarks" };
                string checkParamsResult = ValidateRequestField(requestParam, ExcludedParams);
                if (!string.IsNullOrEmpty(checkParamsResult))
                {
                    LogHandler.WriteLog("\t|==> CALLING TRANSFER \n\t|==> MERCHANT NAME : " + merchantName + " \n\t|==> DATA PASSED : " + JsonSerializer.Serialize(requestParam) + " \n\t|==> ERROR : " + checkParamsResult + " CANNOT BE EMPTY OR NULL", "TRANSFER");
                    return (false, new TransferFundResponse
                    {
                        code = 717,
                        message = "ERROR_TRANSACTION_DATA_INVALID",
                        description = checkParamsResult + " CANNOT BE EMPTY OR NULL"
                    });
                }

                // If the hash key provided by merchant is different from the one               
                string requestDataToBeHashed = merchantAPIKey + "|" +
                    MerchantID + "|" +
                    requestParam.sendingAmount.ToString() + "|" +
                    requestParam.beneficiaryAccountNumber.ToString() + "|" +
                    requestParam.beneficiaryBankCode.ToString() + "|" +
                    requestParam.merchantReference.ToString() + "|" +
                    DateTime.Now.ToString("yyyy.MM.dd");

                string requestDataHashed = GetHashOf(requestDataToBeHashed);  // we calculate here

                if (!requestDataHashed.ToLower().Equals(requestParam.key.ToLower()))
                {
                    LogHandler.WriteLog("\t|==> CALLING TRANSFER \n\t|==> MERCHANT NAME : " + merchantName + " \n\t|==> DATA PASSED : " + JsonSerializer.Serialize(requestParam) + " \n\t|==> ERROR : UNABLE TO VALIDATE THE TRANSACTION KEY", "TRANSFER");
                    return (false, new TransferFundResponse()
                    {
                        code = 717,
                        message = "ERROR_TRANSACTION_DATA_INVALID",
                        description = "UNABLE TO VALIDATE THE TRANSACTION KEY"
                    });
                }
                return (true, new transferResponse());
            }
            catch (Exception ex)
            {
                return (false, new transferResponse());
            }
        }

        /********************************
 * 
 * COMMON BANK TRANSFER METHODS
 * 
 *******************************/
        private static string ValidateRequestField(transferRequestToBank requestData, string[] ExcludedField = null)
        {
            try
            {
                List<string> emptyParamsList = new List<string>();  // Holds the list of Params that are empty
                PropertyInfo[] properties = requestData.GetType().GetProperties();
                foreach (PropertyInfo property in properties)
                {
                    string propertyName = property.Name.ToString();     // Getting the Object property name
                    string propertyType = property.PropertyType.Name;   // Gettign the type "string; Int32, Float ..." of the property
                    if (ExcludedField != null)
                    {                        // if an excluded list of parameters is passed
                        if (!ExcludedField.Contains(propertyName))
                        {
                            if (propertyType == "String")
                            {
                                string propertyValue = (string)property.GetValue(requestData);
                                if (string.IsNullOrEmpty(propertyValue))
                                {
                                    emptyParamsList.Add(propertyName.Replace("_", " ").ToUpper());
                                }
                            }

                            if (propertyType == "Int32")
                            {
                                int propertyValue = (int)property.GetValue(requestData);
                                if (propertyValue <= 0)
                                {
                                    emptyParamsList.Add(propertyName.Replace("_", " ").ToUpper());
                                }
                            }
                        }
                    }
                    else
                    {
                        if (propertyType == "String")
                        {
                            string propertyValue = (string)property.GetValue(requestData);
                            if (string.IsNullOrEmpty(propertyValue))
                            {
                                emptyParamsList.Add(propertyName.Replace("_", " ").ToUpper());
                            }
                        }

                        if (propertyType == "Int32")
                        {
                            int propertyValue = (int)property.GetValue(requestData);
                            if (propertyValue == 0)
                            {
                                emptyParamsList.Add(propertyName.Replace("_", " ").ToUpper());
                            }
                        }
                    }
                }

                // If List empty means there is no error
                if (emptyParamsList.Count == 0)
                {
                    return null;
                }

                // else there errors
                // return the request params involved
                return string.Join(", ", emptyParamsList.ToArray());
            }
            catch (Exception ex)
            {
                LogHandler.WriteLog("TRANSFER_FUND Trying validating request data with error : " + ex.Message);
                return null;
            }
        }

    }
}

