using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MiddlewareAuth.Models.Models
{

    public class SuperCustomer
    {
        public string userId { get; set; }
    }
    public class Customer : UserCredentials
    {
        //[Key]
        //public string customerId { get; set; }

        public int userId { get; set; }
        public string customerName { get; set; }
        public string customerPhone { get; set; }
        public string customerEmail { get; set; }
        public string customerBankCode { get; set; }
        public string status { get; set; }
        public int tryCount { get; set; }
        public string deviceId { get; set; }
        public string datecreated { get; set; }
        public string lastLogin { get; set; }
        public int isActivated { get; set; }
        public int current_balance { get; set; }
        public string pinCode { get; set; }

        public int walletId { get; set; }
        public string hashedPin { get; set; }
        public string saltedPin { get; set; }



    }
    //public class CustomerResponse : Customer
    //{
    //    //[Key]
    //    //public string customerId { get; set; }

    //    public int walletId { get; set; }
    //    public string hashedPin { get; set; }
    //    public string saltedPin { get; set; }


    //}






    public class Agent : UserCredentials
    {
        public int userId { get; set; }
        public string agentName { get; set; }
        public string agentPhone { get; set; }
        public string agentEmail { get; set; }
        public string agentBankCode { get; set; }
        public string status { get; set; }
        public int tryCount { get; set; }
        public string deviceId { get; set; }
        public string datecreated { get; set; }
        public string lastLogin { get; set; }
        public int isActivated { get; set; }
        public int current_balance { get; set; }

    }
}