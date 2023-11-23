using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiddlewareAuth.Models.Models
{
    public class TransferPayload
    {
        public string senderID { get; set; }
        public string receiverID { get; set; }
        public string customerType { get; set; }
        public double amount { get; set; }
    }
}