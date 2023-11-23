using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MiddlewareAuth.Models.Models
{
    public class UserCredentials
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string userType { get; set; }// customer || bank || agent
        //public int isActivated { get; set; }

    }


    public class UserPasswordEditPayload
    {
        public string userName { get; set; }
        public string oldPassword { get; set; }
        public string newPassword { get; set; }
        public string userType { get; set; }// customer || bank || agent

    }

    public class UserCredentialsEnc
    {
        public string payload { get; set; }

    }

}