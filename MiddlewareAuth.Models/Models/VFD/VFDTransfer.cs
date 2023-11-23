using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akidi_core.Models.VFD
{
    public class VFDTransfer
    {
        public string fromAccount { get; set; }
        public string fromClientId { get; set; }
        public string fromClient { get; set; }
        public string fromSavingsId { get; set; }
        public string fromBvn { get; set; }
        public string toClientId { get; set; }
        public string toClient { get; set; }
        public string toSavingsId { get; set; }
        public string toSession { get; set; }
        public string toBvn { get; set; }
        public string toAccount { get; set; }
        public string toBank { get; set; }
        public string signature { get; set; }
        public string amount { get; set; }
        public string remark { get; set; }
        public string transferType { get; set; }
        public string reference { get; set; }
    }



    public class VFDQrCode

    {
        public string accountNo { get; set; }
        public string qrType { get; set; }
        public string amount { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
    }


    public class VFDQrCodeResponse : DefaultResponse
    {
        public class data
        {
            public string qrCode { get; set; }
        }
    }



    public class VFDCredit
    {

        public string amount { get; set; }
        public string accountNo { get; set; }
        public string senderAccountNo { get; set; }
        public string senderBank { get; set; }
        public string senderNarration { get; set; }

    }

    public class VFDPayQr
    {
        public string amount { get; set; }
        public string merchantNo { get; set; }
        public string subMerchantNo { get; set; }
        public string subMerchantName { get; set; }
        public string qrType { get; set; }
        public string signature { get; set; }
    }
}
