using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akidi_core.Models.VFD
{
    public class VFDAccounts
    {
    }


    public class VFDIndividualAccount
    {
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string middlename { get; set; }
        public string dob { get; set; }
        public string address { get; set; }
        public string gender { get; set; }
        public string phone { get; set; }
        public string bvn { get; set; }
    }


    public class UserInquryPayload
    {
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string gender { get; set; }
        public string dateOfBirth { get; set; }
        public string phoneNo { get; set; }
        public string bvn { get; set; }
        public string pixBase64 { get; set; }
    }


    public class VFDCorporateAccount
    {
        public string rcNumber { get; set; }
        public string companyName { get; set; }
        public string incorporationDate { get; set; }
        public string bvn { get; set; }
    }


    public class VFDVirtualAccount
    {
        public string amount { get; set; }
        public string merchantName { get; set; }
        public string merchantId { get; set; }
        public string sreference { get; set; }
        public string validityTime { get; set; }
    }


    public class VFDReleaseAccount
    {
        public string accountNo { get; set; }
    }


    public class VFDEnquiryUser
    {
        public string beneficiaryAccountNo { get; set; }
        public string senderAccountNumber { get; set; }
        public string senderNarration { get; set; }
        public string amount { get; set; }
    }


    public class VFDEnquiryResponse : DefaultResponse
    {
        public UserInquryPayload data { get; set; }
    }


    public class VFDSubAccount
    {
        public string accountNo { get; set; }
        public string bank { get; set; }
    }

}
