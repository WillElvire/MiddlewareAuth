using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiddlewareAuth.Models.Models
{
    public class CodeTransaction 
    {
        public string merchantId { get; set; }
        public string merchantName { get; set; }
        public string merchantCode { get; set; }
        public string bankCode { get; set; }
        public string customerAccountNumber { get; set; }
        public string customerName { get; set; }
        public string customerEmail { get; set; }
        public string customerPhoneNumber { get; set; }
        public int transType { get; set; }
        public int transAmount { get; set; }
        public int transactionCode { get; set; }
        public int transactionPin { get; set; }
        public int transactionReference { get; set; }
        public int transStatus { get; set; }
        public int transReason { get; set; }
        public string merchantTransReference { get; set; }
        public string agentCustomerFlag { get; set; }
        public string settlementAccount { get; set; }
        public string settlementCommissionAccount { get; set; }
        public string transactionDate { get; set; }
        public string treatednDate { get; set; }

    }


}