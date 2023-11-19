using System;
namespace MiddlewareAuth.Models
{

    public class TransferFundResponseData
    {
        public string gtb_reference { get; set; }
        public string merchant_reference { get; set; }
        public string sender_name { get; set; }
        public string sender_address { get; set; }
        public string sender_phone_number { get; set; }
        public string sender_country { get; set; }
        public int sending_amount { get; set; }
        public string sending_currency { get; set; }
        public string sending_reason { get; set; }
        public string receiver_name { get; set; }
        public string receiver_phone_number { get; set; }
        public string receiver_bank_account { get; set; }
        public string receiver_bank_name { get; set; }
        public string receiver_bank_code { get; set; }
        public string transaction_status { get; set; }
        public string transaction_date { get; set; }
        public string remarks { get; set; }
        public string settlement_status { get; set; } = null;
        public string settlement_date { get; set; } = null;
    }

}

