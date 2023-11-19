using MiddlewareAuth.Models;

namespace MiddlewareAuth
{
    public interface IMerchantValidation
	{
        string IsUserAuthorized(string authorizationParameterFromHeader, string IncomingRequestIPAddress, string endPoint);

        string GetMerchantAccount(string apiKey, string apiSecret);

        void LogAPIAccessActivity(string merchant, string IncomingRequestIPAddress, string endPoint);

        transferResponse IsServiceActivatedForMerchantNew(string merchant);

        int checkTransferActive(string merchantId);

        string GetHashOf(string dataToBeHashed);

        (bool status, transferResponse response) ValidateTransfer(transferRequestToBank requestParam, string merchant);
    }
}

