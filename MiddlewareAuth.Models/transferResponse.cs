using System;
namespace MiddlewareAuth.Models
{
    public class transferResponse
    {
        public int code { get; set; }
        public string message { get; set; }
        public string description { get; set; } = null;
    }
}

