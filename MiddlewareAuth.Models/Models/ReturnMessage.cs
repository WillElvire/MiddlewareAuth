using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;

namespace MiddlewareAuth.Models.Models
{
    public class ReturnMessage
    {
        public string message { get; set; }
        public Object returnObject { get; set; }

        public HttpStatusCode code { get; set; }
    }


    public class ReturnError
    {
       public int error { get; set; }
        public string title { get; set; }
    }

    public enum ReturnedCode
    {
        OK = 200,
        BAD_REQUEST = 400,
        UNAUTHORIZED = 401,
        PAYMENT_REQUIRED = 402,
        WRONG_AUTHENTIFICATION = 403,
        NOT_FOUND = 404,
        DELETED = 409,
        FAILED = 500,
        CREATED = 201,
        IMPORTED = 204,
        PARTIAL = 206,
        NOT_VALIDATED = 421
    }
}