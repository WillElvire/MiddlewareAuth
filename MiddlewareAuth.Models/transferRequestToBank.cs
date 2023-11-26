using System;
using static MiddlewareAuth.Models.Models.MobileMoney;

namespace MiddlewareAuth.Models
{
    public class transferRequestToBank
    {
        public string merchantReference { get; set; } // Mandatory
        public string senderAccount { get; set; } // Mandatory
        public string senderAccountName { get; set; } // Mandatory
        public string inhouseAccountCredited { get; set; }
        public string senderEmail { get; set; } // Mandatory
        public string senderMobileNumber { get; set; } // Mandatory
        public int sendingAmount { get; set; } // Mandatory
        public string sendingReason { get; set; } // Mandatory
        public string senderAddress { get; set; } // Mandatory
        public string sendingCountry { get; set; } // Mandatory
        public string beneficiaryAccountNumber { get; set; } // Mandatory
        public string beneficiaryAccountName { get; set; } // Mandatory

        public string beneficiaryBank { get; set; } // Mandatory

        public string beneficiaryBankCode { get; set; }   // Mandatory    
        public string beneficiaryMobileNumber { get; set; }

        public string beneficiaryEmail { get; set; }

        public string remarks { get; set; }
        public string encryptedData { get; set; }      // Mandatory
        public string key { get; set; }      // Mandatory

        public int amount { get; set; }
        public string description { get; set; }
        public string merchantId { get; set; }
        public string requestkey { get; set; }
        public string reference { get; set; }
        public string currency { get; set; }
        public Destination destination { get; set; }
    }
}

