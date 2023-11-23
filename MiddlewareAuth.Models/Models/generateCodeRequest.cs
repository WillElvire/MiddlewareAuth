using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiddlewareAuth.Models.Models
{
    public class GenerateCodeRequest
    {
        public string transactionType { get; set; } // Mandatory
        public string senderAccount { get; set; } // Mandatory
        public string senderAccountName { get; set; } // Mandatory
        public string clientTransReference { get; set; } // Mandatory
        public string inhouseAccountCredited { get; set; }
        public string senderEmail { get; set; }
        public string senderMobileNumber { get; set; } // Mandatory
        public int sendingAmount { get; set; } // Mandatory
        public string sendingReason { get; set; } // Mandatory
        public string encryptedData { get; set; }      // Mandatory
        public string key { get; set; }      // Mandatory
        public string receiverAccount { get; set; } // Mandatory
        public string receiverAccountName { get; set; } // Mandatory
        public string receiverEmail { get; set; }
        public string receiverMobileNumber { get; set; } // Mandatory
        public string secretAnswer { get; set; } // Mandatory
    }

    public class transferResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public string description { get; set; } = null;
    }

    public class codeGenerationResponseData
    {
        public string reference { get; set; }
        public string transCode { get; set; }
        public string merchant_reference { get; set; }
        public string sender_name { get; set; }
        public string sender_address { get; set; }
        public string sender_phone_number { get; set; }
        public string sender_country { get; set; }
        public int sending_amount { get; set; }
        public string sending_currency { get; set; }
        public string sending_reason { get; set; }

        public string receiverAccount { get; set; } // Mandatory
        public string receiverAccountName { get; set; } // Mandatory

        public string receiverEmail { get; set; }
        public string receiverMobileNumber { get; set; } // Mandatory
        public string expirationTime { get; set; }
        public string receiver_bank_name { get; set; }
        public string receiver_bank_code { get; set; }
        public string transaction_status { get; set; }
        public string transaction_date { get; set; }
        public string settlement_status { get; set; } = null;
        public string settlement_date { get; set; } = null;
    }


    public class codeGenrateResponse : transferResponse
    {
        public codeGenerationResponseData data { get; set; }
    }

    public class payCancelCodeRequest
    {
        public string senderAccount { get; set; } // Mandatory
        public string transCode { get; set; } // Mandatory
        public string clientTransReference { get; set; } // Mandatory
        public string encryptedData { get; set; }      // Mandatory
        public string key { get; set; }      // Mandatory
    }

    public class payCodeRequest
    {
        public string secretAnswer { get; set; } // Mandatory
        public string transCode { get; set; } // Mandatory
        public string encryptedData { get; set; }      // Mandatory
        public string key { get; set; }      // Mandatory
    }
}