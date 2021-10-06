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
    public class RegisterService : IRegisterService
    {
        private readonly ILogger<RegisterService> _logger;
        private readonly ISimpleTradingHttpClient _simpleTradingHttpClient;

        public RegisterService(ILogger<RegisterService> logger,
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
                        Type = RegisterErrorType.Unknown
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
                        Type = RegisterErrorType.RegistrationAlreadyExist
                    });
                }

                if ((SimpleTradingResultCode)registerResult.SuccessResult.Status ==
                    SimpleTradingResultCode.InvalidUserNameOrPassword)
                {
                    return FailedMapToGrpc(new Error()
                    {
                        Message = "Invalid username or password",
                        Type = RegisterErrorType.InvalidParameter
                    });
                }

                if ((SimpleTradingResultCode)registerResult.SuccessResult.Status ==
                    SimpleTradingResultCode.PersonalDataNotValid)
                {
                    return FailedMapToGrpc(new Error()
                    {
                        Message = "Registration data not valid",
                        Type = RegisterErrorType.InvalidParameter
                    });
                }

                if ((SimpleTradingResultCode)registerResult.SuccessResult.Status ==
                    SimpleTradingResultCode.SystemError)
                {
                    return FailedMapToGrpc(new Error()
                    {
                        Message = "Brand Error",
                        Type = RegisterErrorType.Unknown
                    });
                }

                return FailedMapToGrpc(new Error()
                {
                    Message = "Unknown Error",
                    Type = RegisterErrorType.Unknown
                });

            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating lead {@context}", request);

                return FailedMapToGrpc(new Error()
                {
                    Message = "Brand response parse error",
                    Type = RegisterErrorType.Unknown
                });
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
    }
}
