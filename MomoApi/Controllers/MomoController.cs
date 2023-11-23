using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MiddlewareAuth.Models.Models;
using MomoApi.Services;
using static MiddlewareAuth.Models.Models.MobileMoney;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MomoApi.Controllers
{
    [Route("[controller]")]
    public class MomoController : ControllerBase
    {
        private readonly MomoService mobileMoneyService = new MomoService();
        [HttpPost]
        [Route("api/momo/transfer")]
        public TransactionResponse MomoTransferPayOut(MobileMoneyPayload payload)
        {

            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new TransactionResponse() { code = 1010, transactionInfos = null, message = "Invalid Data" };
            }

            Utils.Utils.SaveLog("MomoController", "api/momo/transfer", JsonSerializer.Serialize(payload));
            var momoPaymentResponse = mobileMoneyService.MobileMoneyPayOut(payload);
            return new TransactionResponse() { code = 1000, message = momoPaymentResponse.message, transactionInfos = momoPaymentResponse.returnObject };

        }


        [HttpPost]
        [Route("api/momo/payment")]
        public TransactionResponse MomoPayment(MomoPaymentPayload payload)
        {

            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new TransactionResponse() { code = 1010, transactionInfos = null, message = "Invalid Data" };
            }

            Utils.Utils.SaveLog("MomoController", "api/momo/payment", JsonSerializer.Serialize(payload));
            var momoPaymentResponse = mobileMoneyService.StartPaymentProcess(payload);
            return new TransactionResponse() { code = 1000, message = momoPaymentResponse.message, transactionInfos = momoPaymentResponse.returnObject };
        }




        [HttpGet]
        [Route("api/momo/payment")]
        public TransactionResponse FetchMomoPayment()
        {

            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new TransactionResponse() { code = 1010, transactionInfos = null, message = "Invalid Data" };
            }

            Utils.Utils.SaveLog("MomoController", "api/momo/payment", "");
            var momoPaymentResponse = mobileMoneyService.GetMobileMoneyPayment();
            return new TransactionResponse() { code = 1000, message = momoPaymentResponse.message, transactionInfos = momoPaymentResponse.returnObject };
        }





        [HttpGet]
        [Route("api/momo/{trans_id}")]
        public TransactionResponse MomoTransferByTransactionId(string trans_id)
        {
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new TransactionResponse() { code = 1010, transactionInfos = null, message = "Invalid Data" };
            }

            Utils.Utils.SaveLog("MomoController", "api/momo/{trans_id}", JsonSerializer.Serialize(trans_id));
            var momoTransactionByIdResponce = mobileMoneyService.GetMobileMoneyTransactionById(trans_id);

            return new TransactionResponse() { code = 1000, message = momoTransactionByIdResponce.message, transactionInfos = momoTransactionByIdResponce.returnObject };

        }



        [HttpGet]
        [Route("api/momo")]
        public TransactionResponse MomoTransferByTransactions()
        {
            if (!ModelState.IsValid)
            {
                var errorList = (from item in ModelState.Values
                                 from error in item.Errors
                                 select error.ErrorMessage).ToArray();
                return new TransactionResponse() { code = 1010, transactionInfos = null, message = "Invalid Data" };
            }
            Utils.Utils.SaveLog("MomoController", "api/momo", "");
            var momoTransactionByIdResponce = mobileMoneyService.GetMobileMoneyTransactions();

            return new TransactionResponse() { code = 1000, message = momoTransactionByIdResponce.message, transactionInfos = momoTransactionByIdResponce.returnObject };

        }
    }
}

