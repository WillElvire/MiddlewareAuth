using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace akidi_core.Models.VFD
{
    public class VFDOnborarding
    {

        public string username { get; set; }
        public string walletName { get; set; }
        public string webhookUrl { get; set; }
        public string shortName { get; set; }
        public string implementation { get; set; }

    }


    public class CustomerOnboardInformation
    {
        public string username { get; set; }
        public string walletName { get; set; }
        public string webhookUrl { get; set; }
        public string shortName { get; set; }
        public string implementation { get; set; }
    }

    public class BillerResponce : DefaultResponse
    {
        public BillerCategory data { get; set; }
    }

    public class BillerCategory
    {
        public string category { get; set; }

    }

    public class PhoneVerification
    {
        public string phoneNumber { get; set; }

    }


    public class PhoneVerificationPayload : DefaultResponse
    {
        public dynamic data { get; set; }


        public PhoneVerificationPayload(PhoneVerificationResponse phoneVerificationResponse)
        {
            this.data = phoneVerificationResponse;
        }

        public PhoneVerificationPayload(PhoneVerificationError phoneVerification)
        {
            this.data = phoneVerification;
        }
    }





    public class PhoneVerificationError
    {
        public bool available { get; set; }
        public bool valid { get; set; }
        public string error { get; set; }
    }


    public class TransactionStatusResponse : DefaultResponse
    {
        public TransactionStatus data { get; set; }
    }

    public class TransactionStatus
    {
        public string TxnId { get; set; }
        public string amount { get; set; }
        public string accountNo { get; set; }
        public string fromAccountNo { get; set; }
        public string transactionStatus { get; set; }
        public string transactionDate { get; set; }
        public string toBank { get; set; }
        public string fromBank { get; set; }
        public string sessionId { get; set; }
        public string bankTransactionId { get; set; }
        public string transactionType { get; set; }
    }



    public class DefaultResponse
    {
        public string status { get; set; }
        public string message { get; set; }
    }

    public class PhoneVerificationResponse
    {
        public bool available { get; set; }
        public bool valid { get; set; }
        public string idNumber { get; set; }
        public string idType { get; set; }
        public string country { get; set; }
        public string currentlyEmployed { get; set; }
        public string fullName { get; set; }
        public string nextOfKin { get; set; }
        public string imagePath { get; set; }
        public string applicantFirstName { get; set; }
        public string applicantLastName { get; set; }
        public string applicantMiddleName { get; set; }
        public string applicantPhoneNumber { get; set; }
        public string applicantGender { get; set; }
        public string nin { get; set; }
        public string firstName { get; set; }
        public string middleName { get; set; }
        public string lastName { get; set; }
        public string maideNname { get; set; }
        public string otherName { get; set; }
        public string phoneNumber { get; set; }
        public string state { get; set; }
        public string city { get; set; }
        public string title { get; set; }
        public string height { get; set; }
        public string email { get; set; }
        public string dateOfBirth { get; set; }
        public string stateOfBirth { get; set; }
        public string countryOfBirth { get; set; }
        public string centralId { get; set; }
        public string documentNo { get; set; }
        public string educationallevel { get; set; }
        public string employmentStatus { get; set; }
        public string maritalStatus { get; set; }
        public string photo { get; set; }
        public string pFirstName { get; set; }
        public string pMiddleName { get; set; }
        public string pSurname { get; set; }
        public string occupation { get; set; }
        public string nSpokenLang { get; set; }
        public string oSpokenLang { get; set; }
        public string religion { get; set; }
        public string residenceTown { get; set; }
        public string lgaOfresidence { get; set; }
        public string stateOfResidence { get; set; }
        public string residenceStatus { get; set; }
        public string address { get; set; }
        public string residentialAddress { get; set; }
        public string lgaOfOrigin { get; set; }
        public string placeOfOrigin { get; set; }
        public string stateOfOrigin { get; set; }
        public string signature { get; set; }
        public string nationality { get; set; }
        public string gender { get; set; }
        public string reference { get; set; }


    }
}
