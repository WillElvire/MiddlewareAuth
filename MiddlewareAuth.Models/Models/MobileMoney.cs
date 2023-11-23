using System;


namespace MiddlewareAuth.Models.Models
{
    public class MobileMoney
    {
        public class MobileMoneyPayload
        {
            public string reference { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string description { get; set; }
            public Destination destination { get; set; }
            public string merchantId { get; set; }
        }

        public class Destination
        {
            public string type { get; set; }
            public string country { get; set; }
            public string recipientName { get; set; }
            public string msisdn { get; set; }
            public string provider { get; set; }
        }

        public class MobileMoneyTransferResponse
        {
            public Destination destination { get; set; }
            public string id { get; set; }
            public DateTime createdAt { get; set; }
            public DateTime updatedAt { get; set; }
            public string reference { get; set; }
            public string description { get; set; }
            public string[] fees { get; set; }
            public string mode { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string status { get; set; }
            public bool isIrt { get; set; }
            public MomoFaillureCause failureCause { get; set; }
            public string origin { get; set; }
            public string overrideBusinessName { get; set; }

        }

        public class MomoFaillureCause
        {
            public string code { get; set; }
            public string message { get; set; }
        }

        public class MomoTransfert {

            public int transferId { get; set; }
            public MobileMoneyPayload request { get; set; }
            public dynamic response { get; set; }
            public string transactionType { get; set; }
            public string Provider { get; set; }
            public string momoTransId { get; set; }
            public PaymenStatus status { get; set; }

            public string akidiReference { get; set; }

        } 


        public class MomoPaloadMsisdn
        {
            public string msisdn { get; set; }
        }


        public class MomoPayment
        {
            public string token { get; set; }
            public string paymentMethod { get; set; }
            public string country { get; set; }
            public string provider { get; set; }
            public MomoPaloadMsisdn mobileMoney { get; set; }

        }


        public class MomoPaymentPayload
        {
            public string merchantId { get; set; }
            public int amount { get; set; }
            public string paymentMethod { get;set;}
            public string provider { get; set; }
            public string mobileMoney { get; set; } 
       }


        public  class MomoPaymentToDb
        {
            public  int id { get; set; }
            public  string token { get; set; }
            public  string paymentId { get; set; }
            public MomoPayment request { get; set; }
            public dynamic response { get; set; }
            public string merchantId { get; set; }
            public MomoPaymentIntentResponse  intent { get; set; }
            public  string provider { get; set; }
            public PaymenStatus  status { get; set; }

            public string akidiReference { get; set; }
        }

        public enum PaymenStatus
        {
            PROCESS = 1 ,
            FAILED  = 0 ,
            SUCCESS = 2 ,
            CREATED = 3
        }

        public class  MomoPaymentIntent
        {
            public string customerReference { get; set; }
            public string purchaseReference { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
        }

        public class MomoFees
        {
            public string currency { get; set; }
            public string id { get; set; }
            public string label { get; set; }  
            public int rate { get; set; } 
            public string  rateType { get; set; }
            public int amount { get; set; }            
        }


        public class MomoPaymentDetail
        {
           public string  id { get; set; }
           public string intentId { get; set; }
           public string status { get; set; }
           public int amount { get; set; }
           public string currency { get; set; }
           public string method { get; set; }
           public MomoFees[] fees { get; set; }
           public MomoFaillureCause faillure { get; set; }
        }

        public class MomoPaymentResponse  
        {
            public string token { get; set; }
            public string currency { get; set; }
            public string mode { get; set; }
            public string id { get; set; }
            public DateTime  createdAt { get; set; }
            public string merchantId { get; set; }        
            public string status { get; set; }
            public string customerReference { get; set; }
            public string purchaseReference { get; set; }
            public int amount { get; set; }
            public MomoPaymentDetail[] payments { get; set; }

        }

        public class MomoPaymentIntentResponse
        {
            public string id { get; set; }
            public string createdAt { get; set; }
            public string updatedAt { get; set; }
            public string merchantId { get; set; }
            public string purchaseReference { get; set; }
            public string customerReference { get; set; }
            public int amount { get; set; }
            public string currency { get; set; }
            public string token { get; set; }
            public string status { get; set; }
            public string[] payments { get; set; }
            public string mode { get; set; }
        }

    }
}