using System;
using MiddlewareAuth.Models.Models;
using static MiddlewareAuth.Models.Models.MobileMoney;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace MomoApi.Services
{
    public class MomoService
    {


        HttpClient client = new HttpClient();
        MomoRepositoryService momoRepository = new MomoRepositoryService();
        ConfigurationManager configurationManager = new ConfigurationManager();


        public MomoService()
        {

        }


        private string GetProviderCorrectApiPath()
        {
            string provider = configurationManager.GetConnectionString("MomoProvider").ToString();
            if (provider.ToLower() == "hub2")
            {
                return "https://api.hub2.io";
            }
            return "https://api.hub2.io"; ;
        }

        private HttpResponseMessage TransferFunds(MobileMoneyPayload mobileMoney)
        {
            client = ApiConfiguration();
            string path = "/transfers";
            var json = JsonSerializer.Serialize(mobileMoney);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(path, content).Result;
            return response;
        }


        private HttpResponseMessage InitiatePayment(MomoPayment payment, string paymentId)
        {
            client = ApiConfiguration();
            string path = $"/payment-intents/{paymentId}/payments";
            var json = JsonSerializer.Serialize(payment);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(path, content).Result;
            return response;
        }

        private HttpResponseMessage InitiatePaymentIntent(MomoPaymentIntent momoPaymentIntent)
        {
            client = ApiConfiguration();
            string path = "/payment-intents";
            var json = JsonSerializer.Serialize(momoPaymentIntent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = client.PostAsync(path, content).Result;
            return response;
        }


        private HttpClient ApiConfiguration()
        {
            client.DefaultRequestHeaders.Accept.Clear();

            if (client.BaseAddress == null)
            {
                client.BaseAddress = new Uri(GetProviderCorrectApiPath());
            }

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.Add("MerchantId", configurationManager.GetConnectionString("MerchantID").ToString());
            client.DefaultRequestHeaders.Add("ApiKey", configurationManager.GetConnectionString("ApiKey").ToString());
            client.DefaultRequestHeaders.Add("Environment", configurationManager.GetConnectionString("Environment").ToString());
            return client;
        }

        public ReturnMessage GetMobileMoneyTransactionById(string trans_id)
        {
            ReturnMessage message = new ReturnMessage();
            client = ApiConfiguration();
            string path = $"/transfers/{trans_id}";
            HttpResponseMessage response = client.GetAsync(path).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (response.IsSuccessStatusCode)
            {
                MobileMoneyTransferResponse momoResponse = JsonSerializer.Deserialize<MobileMoneyTransferResponse>(responseContent, options);
                message.returnObject = momoResponse;
                message.code = response.StatusCode;
                return message;
            }
            message.code = response.StatusCode;
            message.message = response.ReasonPhrase;
            message.returnObject = JsonSerializer.Deserialize<dynamic>(responseContent, options);
            return message;

        }

        public ReturnMessage GetMobileMoneyPaymentIntentById(string intentId)
        {
            ReturnMessage message = new ReturnMessage();
            client = ApiConfiguration();
            string path = $"/payment-intents/{intentId}";
            HttpResponseMessage response = client.GetAsync(path).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (response.IsSuccessStatusCode)
            {
                MomoPaymentIntentResponse momoResponse = JsonSerializer.Deserialize<MomoPaymentIntentResponse>(responseContent, options);
                message.returnObject = momoResponse;
                message.code = response.StatusCode;
                LogHandler.WriteLog("\t|==> CALLING  GET MOBILE MONEY INTENT DETAIL  INTENTID = " + intentId + "  \n\t|==> RESPONSE : " + JsonSerializer.Serialize(momoResponse), "MOBILE MONEY");
                return message;
            }
            message.code = response.StatusCode;
            message.message = response.ReasonPhrase;
            message.returnObject = JsonSerializer.Deserialize<dynamic>(responseContent, options);
            LogHandler.WriteLog("\t|==> CALLING  GET MOBILE MONEY INTENT DETAIL  INTENTID = " + intentId + "  \n\t|==> RESPONSE : " + JsonSerializer.Serialize(message.returnObject), "MOBILE MONEY");

            return message;
        }

        public ReturnMessage GetMobileMoneyPaymentIntentFeesByIntentId(string intentId)
        {
            ReturnMessage message = new ReturnMessage();
            client = ApiConfiguration();
            string path = $"/payment-intents/{intentId}/payment-fees";
            HttpResponseMessage response = client.GetAsync(path).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (response.IsSuccessStatusCode)
            {
                List<MomoFees> momoResponse = JsonSerializer.Deserialize<List<MomoFees>>(responseContent, options);
                message.returnObject = momoResponse;
                message.code = response.StatusCode;
                LogHandler.WriteLog("\t|==> CALLING  GET MOBILE MONEY FEES  INTENTID = " + intentId + "  \n\t|==> RESPONSE : " + JsonSerializer.Serialize(momoResponse), "MOBILE MONEY");

                return message;
            }
            message.code = response.StatusCode;
            message.message = response.ReasonPhrase;
            message.returnObject = JsonSerializer.Deserialize<dynamic>(responseContent, options);
            LogHandler.WriteLog("\t|==> CALLING  GET MOBILE MONEY PAYMENT INTENTID = " + intentId + "  \n\t|==> RESPONSE : " + JsonSerializer.Serialize(message.returnObject), "MOBILE MONEY");
            return message;
        }

        public ReturnMessage GetMobileMoneyPayment()
        {
            ReturnMessage message = new ReturnMessage();
            client = ApiConfiguration();
            string path = $"/payments";
            HttpResponseMessage response = client.GetAsync(path).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (response.IsSuccessStatusCode)
            {
                List<MomoPaymentResponse> momoResponse = JsonSerializer.Deserialize<List<MomoPaymentResponse>>(responseContent, options);
                message.returnObject = momoResponse;
                message.code = response.StatusCode;
                LogHandler.WriteLog("\t|==> CALLING  GET MOBILE MONEY PAYMENT  \n\t|==> RESPONSE : " + JsonSerializer.Serialize(momoResponse), "MOBILE MONEY");
                return message;
            }
            message.code = response.StatusCode;
            message.message = response.ReasonPhrase;
            message.returnObject = JsonSerializer.Deserialize<dynamic>(responseContent, options);
            LogHandler.WriteLog("\t|==> CALLING  GET MOBILE MONEY PAYMENT  \n\t|==> RESPONSE : " + JsonSerializer.Serialize(message.returnObject), "MOBILE MONEY");
            return message;
        }

        public ReturnMessage GetMobileMoneyPaymentIntent()
        {
            ReturnMessage message = new ReturnMessage();
            client = ApiConfiguration();
            string path = $"/payment-intents";
            HttpResponseMessage response = client.GetAsync(path).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (response.IsSuccessStatusCode)
            {
                List<MomoPaymentIntentResponse> momoResponse = JsonSerializer.Deserialize<List<MomoPaymentIntentResponse>>(responseContent, options);
                message.returnObject = momoResponse;
                message.code = response.StatusCode;
                LogHandler.WriteLog("\t|==> CALLING  GET MOBILE MONEY PAYMENT  \n\t|==> RESPONSE : " + JsonSerializer.Serialize(momoResponse), "MOBILE MONEY");

                return message;
            }
            message.code = response.StatusCode;
            message.message = response.ReasonPhrase;
            message.returnObject = JsonSerializer.Deserialize<dynamic>(responseContent, options);
            LogHandler.WriteLog("\t|==> CALLING  GET MOBILE MONEY PAYMENT  \n\t|==> RESPONSE : " + JsonSerializer.Serialize(message.returnObject), "MOBILE MONEY");
            return message;
        }


        public ReturnMessage GetMobileMoneyTransactions()
        {
            client = ApiConfiguration();
            string path = $"/transfers";
            ReturnMessage message = new ReturnMessage();
            HttpResponseMessage response = client.GetAsync(path).Result;
            var responseContent = response.Content.ReadAsStringAsync().Result;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            if (response.IsSuccessStatusCode)
            {
                List<MobileMoneyTransferResponse> momoResponse = JsonSerializer.Deserialize<List<MobileMoneyTransferResponse>>(responseContent, options);
                message.returnObject = momoResponse;
                message.code = response.StatusCode;
                LogHandler.WriteLog("\t|==> CALLING  GET MOBILE MONEY TRANSACTION  \n\t|==> RESPONSE : " + JsonSerializer.Serialize(momoResponse), "MOBILE MONEY");
                return message;
            }

            message.code = response.StatusCode;
            message.message = response.ReasonPhrase;
            dynamic errorResponse = JsonSerializer.Deserialize<dynamic>(responseContent, options);
            LogHandler.WriteLog("\t|==> CALLING  MOBILEMONEYPAYOUT \n\t|==> RESPONSE : " + JsonSerializer.Serialize(errorResponse), "MOBILE MONEY");
            return message;

        }

        public ReturnMessage StartPaymentProcess(MomoPaymentPayload paymentPayload)
        {
            ReturnMessage message = new ReturnMessage();
            try
            {


                MomoPaymentIntent intent = new MomoPaymentIntent()
                {
                    amount = paymentPayload.amount,
                    currency = "XOF",
                    customerReference = "Akidi Payment",
                    purchaseReference = GenerateReferenceForMomo()
                };
                message = IntiateMomoPaymentIntent(intent);
                if (message.code == System.Net.HttpStatusCode.Created)
                {
                    MomoPaymentIntentResponse response = (MomoPaymentIntentResponse)message.returnObject;

                    MomoPayment payment = new MomoPayment()
                    {
                        country = "CI",
                        provider = paymentPayload.provider,
                        token = response.token,
                        paymentMethod = paymentPayload.paymentMethod,
                        mobileMoney = new MomoPaloadMsisdn()
                        {
                            msisdn = paymentPayload.mobileMoney
                        }

                    };
                    message = InitiateMomoPayment(payment, response.id);
                    MomoPaymentResponse paymentResponse = (MomoPaymentResponse)message.returnObject;
                    MomoPaymentToDb paymentToDb = new MomoPaymentToDb()
                    {
                        intent = response,
                        request = payment,
                        response = (MomoPaymentResponse)message.returnObject,
                        merchantId = paymentPayload.merchantId,
                        token = payment.token,
                        paymentId = paymentResponse.id,
                        akidiReference = response.purchaseReference

                    };
                    try
                    {
                        momoRepository.addMomoPaymentToDb(paymentToDb);
                        return message;
                    }
                    catch (Exception ex)
                    {
                        return message;
                    }
                }
                return message;
            }
            catch (Exception ex)
            {

                message.code = System.Net.HttpStatusCode.InternalServerError;
                message.message = ex.Message;
                return message;
            }

        }


        public ReturnMessage InitiateMomoPayment(MomoPayment momoPayment, string intentId)
        {
            ReturnMessage message = new ReturnMessage();
            dynamic momoResponse;
            MomoPaymentToDb payment;
            try
            {
                HttpResponseMessage response = InitiatePayment(momoPayment, intentId);
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                if (response.IsSuccessStatusCode)
                {
                    momoResponse = JsonSerializer.Deserialize<MomoPaymentResponse>(responseContent, options);
                    LogHandler.WriteLog("\t|==> CALLING  MOBILEMONEYPAYMENT \n\t|==> INTENT ID =  " + intentId + "  \n\t|==> REQUEST = " + JsonSerializer.Serialize(momoPayment) + " \n\t|==> RESPONSE : " + JsonSerializer.Serialize(momoResponse), "MOBILE MONEY");
                }
                else
                {
                    momoResponse = JsonSerializer.Deserialize<dynamic>(responseContent, options);
                    LogHandler.WriteLog("\t|==> CALLING  MOBILEMONEYPAYMENT \n\t|==> INTENT ID =  " + intentId + "  \n\t|==> REQUEST = " + JsonSerializer.Serialize(momoPayment) + " \n\t|==> RESPONSE : " + JsonSerializer.Serialize(momoResponse), "MOBILE MONEY");
                }
                message.code = response.StatusCode;
                message.returnObject = momoResponse;
                return message;
            }
            catch (Exception ex)
            {
                message.code = System.Net.HttpStatusCode.InternalServerError;
                message.message = ex.Message;
            }
            return message;
        }



        private ReturnMessage IntiateMomoPaymentIntent(MomoPaymentIntent momoPaymentIntent)
        {
            ReturnMessage message = new ReturnMessage();
            try
            {

                momoPaymentIntent.currency = "XOF";
                HttpResponseMessage response = InitiatePaymentIntent(momoPaymentIntent);

                var responseContent = response.Content.ReadAsStringAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                if (response.IsSuccessStatusCode)
                {
                    MomoPaymentIntentResponse momoResponse = JsonSerializer.Deserialize<MomoPaymentIntentResponse>(responseContent, options);
                    message.code = response.StatusCode;
                    message.message = "Transaction created";
                    message.returnObject = momoResponse;
                    LogHandler.WriteLog("\t|==> CALLING  MOBILEMONEYINTENTCREATION \n\t|==> " + JsonSerializer.Serialize(momoPaymentIntent) + " \n\t|==> RESPONSE : " + JsonSerializer.Serialize(momoResponse), "MOBILE MONEY");
                    return message;
                }
                message.code = response.StatusCode;
                message.message = "Error got during the process";
                message.returnObject = JsonSerializer.Deserialize<dynamic>(responseContent, options); ;
                LogHandler.WriteLog("\t|==> CALLING  MOBILEMONEYINTENTCREATION \n\t|==> " + JsonSerializer.Serialize(momoPaymentIntent) + " \n\t|==> RESPONSE : " + JsonSerializer.Serialize(message.returnObject), "MOBILE MONEY");
                return message;
            }
            catch (Exception ex)
            {
                message.code = System.Net.HttpStatusCode.InternalServerError;
                message.message = ex.Message;
            }
            return message;
        }


        public string GenerateReferenceForMomo()
        {
            var date = DateTime.Now.ToString("yyyyMMddTHHmmss");
            return "AKIDI" + "-" + date;
        }

        public ReturnMessage MobileMoneyPayOut(MobileMoneyPayload mobileMoneyPayload)
        {
            ReturnMessage message = new ReturnMessage();
            MomoTransfert transfer;
            try
            {

                mobileMoneyPayload.reference = GenerateReferenceForMomo();
                mobileMoneyPayload.currency = "XOF";
                TransferPayload payload = new TransferPayload()
                {
                    senderID = mobileMoneyPayload.MerchantId,
                    receiverID = 19.ToString(),
                    amount = mobileMoneyPayload.amount,
                    customerType = "1"
                };
                TransferFundService transferFundService = new TransferFundService();
                transferFundService.transferFundService(payload);
                HttpResponseMessage response = TransferFunds(mobileMoneyPayload);
                var responseContent = response.Content.ReadAsStringAsync().Result;
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                if (response.IsSuccessStatusCode)
                {
                    MobileMoneyTransferResponse momoResponse = JsonSerializer.Deserialize<MobileMoneyTransferResponse>(responseContent, options);
                    LogHandler.WriteLog("\t|==> CALLING  MOBILEMONEYPAYOUT \n\t|==> MERCHANT ID : " + mobileMoneyPayload.MerchantId + " \n\t|==> DATA PASSED : " + JsonSerializer.Serialize(mobileMoneyPayload) + " \n\t|==> RESPONSE : " + JsonSerializer.Serialize(momoResponse), "MOBILE MONEY");
                    transfer = new MomoTransfert()
                    {
                        request = mobileMoneyPayload,
                        response = momoResponse,
                        transactionType = "Mobile Money Transfer",
                        akidiReference = mobileMoneyPayload.reference
                    };
                    message = momoRepository.addMomoRequestToDb(transfer);
                    if (message.code == System.Net.HttpStatusCode.OK)
                    {
                        message.code = response.StatusCode;
                        message.message = "Transaction created";
                        message.returnObject = momoResponse;
                        return message;
                    }
                    return message;
                }


                message.code = response.StatusCode;
                message.message = response.ReasonPhrase;
                message.returnObject = JsonSerializer.Deserialize<dynamic>(responseContent, options);
                transfer = new MomoTransfert()
                {
                    request = mobileMoneyPayload,
                    response = message.returnObject,
                    transactionType = "Mobile Money Transfer",
                    akidiReference = mobileMoneyPayload.reference
                };
                momoRepository.addMomoRequestToDb(transfer);
                LogHandler.WriteLog("\t|==> CALLING  MOBILEMONEYPAYOUT \n\t|==> MERCHANT ID : " + mobileMoneyPayload.MerchantId + " \n\t|==> DATA PASSED : " + JsonSerializer.Serialize(mobileMoneyPayload) + " \n\t|==> RESPONSE : " + JsonSerializer.Serialize(message.returnObject), "MOBILE MONEY");
                return message;
            }
            catch (Exception ex)
            {
                message.code = System.Net.HttpStatusCode.InternalServerError;
                message.message = ex.Message;
                return message;
            }
        }
    }
}

