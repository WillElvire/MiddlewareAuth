using MiddlewareAuth.Models;
using static MiddlewareAuth.Models.Models.MobileMoney;

namespace MiddlewareAuth
{
    public interface IMerchantValidation
	{
        string IsUserAuthorized(string authorizationParameterFromHeader, string IncomingRequestIPAddress, string endPoint);

        string GetMerchantAccount(string apiKey, string apiSecret);

        void LogAPIAccessActivity(string merchant, string IncomingRequestIPAddress, string endPoint);

        transferResponse IsServiceActivatedForMerchantNew(string merchant, string endpoint);

        int checkTransferActive(string merchantId, string endpoint);

        string GetHashOf(string dataToBeHashed);

        (bool status, transferResponse response) ValidateTransfer(MobileMoneyPayload requestParam, string merchant);
    }
}

