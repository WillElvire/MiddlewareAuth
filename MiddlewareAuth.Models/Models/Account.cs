using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiddlewareAuth.Models.Models
{
    public class UserAccountFiler
    {
        public string userId { get; set; } // agentId || customerID

    }

    public class Account
    {
        public string userId { get; set; } // agentId || customerID
        public string accountType { get; set; }
        public string accountProvider { get; set; }
        public string customerName { get; set; }
        public string customerBankCode { get; set; }
        public string accountNumber { get; set; }
        public string status { get; set; }
        public string dateUpdated { get; set; }
        public string dateCreated { get; set; }
        public string bankName { get; set; }
        public string userType { get; set; }

    }

    public class UserAccount
    {
        public int userId { get; set; } // agentId || customerID
        public string userType { get; set; } // agentId || customerID
        public string accountType { get; set; }
        public string accountProvider { get; set; }
        public string customerName { get; set; }
        public string customerBankCode { get; set; }
        public string accountNumber { get; set; }
        public string status { get; set; }
        public string dateUpdated { get; set; }
        public string dateCreated { get; set; }
        public string bankName { get; set; }
        public int current_balance { get; set; }


    }

    public class AccountFilter
    {
        public string userId { get; set; } // agentId || customerID
        public string userType { get; set; } // merchant, agent, customer

    }
}