using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using MiddlewareAuth;
using MiddlewareAuth.Models;
using Org.BouncyCastle.Asn1.Ocsp;
using static MiddlewareAuth.Models.Models.MobileMoney;

namespace MomoApi.CustomMiddleware
{
    public class MerchantValidationMiddleware
	{
        private readonly RequestDelegate next;
        private readonly IMerchantValidation _merchantValidation;

        public MerchantValidationMiddleware(RequestDelegate next, IMerchantValidation merchantValidation)
		{
            this.next = next;
            this._merchantValidation = merchantValidation;
        }

        public async Task Invoke(HttpContext context)
        {
            // Get the client IP address from the request
            string clientIpAddress = context.Connection.RemoteIpAddress.ToString();
            // Get the client auth from the request
            string authParameter = context.Request.Headers.Authorization.ToString();
            var endpoint = context.Request.Path.HasValue ? context.Request.Path.Value : "";

            var merchant = _merchantValidation.IsUserAuthorized(authParameter, clientIpAddress, endpoint);
            if (string.IsNullOrEmpty(merchant))
            {
                await EndRequest(context);
            }
            else
            {
                var serviceActivatedChecking = _merchantValidation.IsServiceActivatedForMerchantNew(merchant, endpoint);
                if (serviceActivatedChecking.code != 201)
                {
                    await EndAllTransferRequest(context, serviceActivatedChecking);
                }
                else
                {
                    if (endpoint.Contains("momo/transfer", StringComparison.OrdinalIgnoreCase))
                    {
                        // Read the request bo
                        var request = context.Request;

                        request.EnableBuffering();
                        var buffer = new byte[Convert.ToInt32(request.ContentLength)];
                        await request.Body.ReadAsync(buffer, 0, buffer.Length);
                        var requestContent = Encoding.UTF8.GetString(buffer);
                        var transferParam = JsonSerializer.Deserialize<MobileMoneyPayload>(requestContent);
                        var response = _merchantValidation.ValidateTransfer(transferParam, merchant);

                        if (!response.status)
                        {
                            request.Body.Position = 0;  //rewinding the stream to 0

                            await EndAllTransferRequest(context, response.response);
                        }
                        else
                        {
                            // Continue to the next middleware in the pipeline
                            request.Body.Position = 0;  //rewinding the stream to 0
                            await next(context);
                        }

                    }
                    else {
                        // Continue to the next middleware in the pipeline
                        await next(context);
                    }
                }
            }
        }

        private static async Task EndRequest(HttpContext context)
        {
            // Return an unauthorized / forbidden response if the check is not valid
            var response = Activator.CreateInstance<transferResponse>();
            response.message = "UNAUTHORIZED";
            response.code = 401;
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            await context.Response.WriteAsJsonAsync(response);
        }

        private static async Task EndAllTransferRequest<T>(HttpContext context, T response)
        {
            // Return an unauthorized / forbidden response if the check is not valid
            await context.Response.WriteAsJsonAsync<T>(response);
        }
    }
}

