using System;
using System.Linq;
using System.Threading.Tasks;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Common;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Customers;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Customers.Contracts;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Leads.Contracts;
using MarketingBox.Integration.SimpleTrading.Bridge.Services.Integrations;
using MarketingBox.Integration.SimpleTrading.Bridge.Services.Integrations.Contracts.Enums;
using MarketingBox.Integration.SimpleTrading.Bridge.Services.Integrations.Contracts.Requests;
using MarketingBox.Integration.SimpleTrading.Bridge.Services.Integrations.Contracts.Responses;
using Microsoft.Extensions.Logging;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Services
{
    public class BridgeService : IBridgeService
    {
        private readonly ILogger<BridgeService> _logger;
        private readonly ISimpleTradingHttpClient _simpleTradingHttpClient;

        public BridgeService(ILogger<BridgeService> logger,
            ISimpleTradingHttpClient simpleTradingHttpClient)
        {
            _logger = logger;
            _simpleTradingHttpClient = simpleTradingHttpClient;
        }

        public async Task<RegistrationCustomerResponse> RegisterCustomerAsync(RegistrationCustomerRequest request)
        {
            _logger.LogInformation("Creating new LeadInfo {@context}", request);
            try
            {
                var registerResult =
                    await _simpleTradingHttpClient.RegisterTraderAsync(new RegisterRequest()
                    {
                        FirstName = request.Info.FirstName,
                        LastName = request.Info.LastName,
                        Password = request.Info.Password,
                        Email = request.Info.Email,
                        Phone = request.Info.Phone,
                        LangId = request.Info.Language,
                        Ip = request.Info.Ip,
                        CountryByIp = request.Info.Country,
                        AffId = Convert.ToInt32(Program.Settings.BrandAffiliateId),
                        BrandId = Program.Settings.BrandBrandId,
                        SecretKey = Program.Settings.BrandAffiliateKey,
                        ProcessId = DateTimeOffset.UtcNow.ToString(),
                        CountryOfRegistration = request.Info.Country,
                    });

                // Failed
                if (registerResult.IsFailed)
                {
                    return FailedMapToGrpc(new Error()
                    {
                        Message = registerResult.FailedResult.Message,
                        Type = ErrorType.Unknown
                    });
                }

                // Success
                if (registerResult.SuccessResult.IsSuccessfully())
                {
                    // Success
                    return SuccessMapToGrpc(registerResult.SuccessResult);
                }

                // Success, but software failure
                if ((SimpleTradingResultCode)registerResult.SuccessResult.Status ==
                    SimpleTradingResultCode.UserExists)
                {
                    return FailedMapToGrpc(new Error()
                    {
                        Message = "Registration already exists",
                        Type = ErrorType.RegistrationAlreadyExist
                    });
                }

                if ((SimpleTradingResultCode)registerResult.SuccessResult.Status ==
                    SimpleTradingResultCode.InvalidUserNameOrPassword)
                {
                    return FailedMapToGrpc(new Error()
                    {
                        Message = "Invalid username or password",
                        Type = ErrorType.InvalidParameter
                    });
                }

                if ((SimpleTradingResultCode)registerResult.SuccessResult.Status ==
                    SimpleTradingResultCode.PersonalDataNotValid)
                {
                    return FailedMapToGrpc(new Error()
                    {
                        Message = "Registration data not valid",
                        Type = ErrorType.InvalidParameter
                    });
                }

                if ((SimpleTradingResultCode)registerResult.SuccessResult.Status ==
                    SimpleTradingResultCode.SystemError)
                {
                    return FailedMapToGrpc(new Error()
                    {
                        Message = "Brand Error",
                        Type = ErrorType.Unknown
                    });
                }

                return FailedMapToGrpc(new Error()
                {
                    Message = "Unknown Error",
                    Type = ErrorType.Unknown
                });

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating lead {@context}", request);

                return new RegistrationCustomerResponse() { Error = new Error() { Message = "Internal error", Type = ErrorType.Unknown } };
            }
        }

        public static RegistrationCustomerResponse SuccessMapToGrpc(RegisterResponse brandRegistrationInfo)
        {
            return new RegistrationCustomerResponse()
            {
                Status = "successful",
                Message = brandRegistrationInfo.RedirectUrl,
                RegistrationInfo = new RegistrationCustomerInfo()
                {
                    CustomerId = brandRegistrationInfo.TraderId,
                    LoginUrl = brandRegistrationInfo.RedirectUrl,
                    Token = brandRegistrationInfo.Token
                }
            };
        }

        public static RegistrationCustomerResponse FailedMapToGrpc(Error error)
        {
            return new RegistrationCustomerResponse()
            {
                Status = "failed",
                Message = error.Message,
                Error = error
            };
        }

        //private static Random random = new Random();
        //public static string RandomString(int length)
        //{
        //    const string chars = "123456789";
        //    return new string(Enumerable.Repeat(chars, length)
        //        .Select(s => s[random.Next(s.Length)]).ToArray());
        //}

        //public async Task<RegistrationCustomerResponse> BrandRegisterAsync(RegistrationCustomerRequest customer)
        //{
        //    string brandLoginUrl = @"https://trading-test.handelpro.biz/lpLogin/6DB5D4818181B806DBF7B19EBDC5FD97F1B82759077317B6481BC883F071783DBEF568426B81DF43044E326C26437E097F21A2484110D13420E9EC6E44A1B2BE?lang=PL";
        //    string brandName = "Monfex";
        //    string brandCustomerId = "02537c06cab34f62931c263bf3480" + RandomString(5);
        //    string customerEmail = "yuriy.test.2020.09.22.01@mailinator.com";
        //    string brandToken = "6DB5D4818181B806DBF7B19EBDC5FD97F1B82759077317B6481BC883F071783DBEF568426B81DF43044E326C26437E097F21A2484110D13420E9EC6E44A1B2BE";



        //    var brandInfo = new RegistrationCustomerResponse()
        //    {
        //        Status = "successful",
        //        Error = null,
        //        FallbackUrl = "",
        //        Message = brandLoginUrl,
        //        RegistrationInfo = new RegistrationCustomerInfo()
        //        {
        //            LoginUrl = brandLoginUrl,
        //            CustomerId = brandCustomerId,
        //            Token = brandToken,
        //        }
        //    };
        //    await Task.Delay(1000);
        //    return brandInfo;
        //}
    }
}
