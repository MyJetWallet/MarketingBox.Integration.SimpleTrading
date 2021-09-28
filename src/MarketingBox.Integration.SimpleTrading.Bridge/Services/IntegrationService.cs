using System;
using System.Threading.Tasks;
using DotNetCoreDecorators;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Common;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Leads.Contracts;
using MarketingBox.Integration.SimpleTrading.Bridge.Messages.Deposits;
using MarketingBox.Integration.SimpleTrading.Bridge.MyNoSql.Leads;
using MarketingBox.Integration.SimpleTrading.Bridge.Postgres;
using MarketingBox.Integration.SimpleTrading.Bridge.Postgres.Entities.Lead;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MyNoSqlServer.Abstractions;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Services
{
    public class IntegrationService : IIntegrationService
    {
        private readonly ILogger<IntegrationService> _logger;
        private readonly DbContextOptionsBuilder<DatabaseContext> _dbContextOptionsBuilder;
        private readonly IPublisher<DepositUpdateMessage> _publisherLeadUpdated;
        private readonly IMyNoSqlServerDataWriter<LeadNoSql> _myNoSqlServerDataWriter;

        public IntegrationService(ILogger<IntegrationService> logger,
            DbContextOptionsBuilder<DatabaseContext> dbContextOptionsBuilder,
            IPublisher<DepositUpdateMessage> publisherLeadUpdated,
            IMyNoSqlServerDataWriter<LeadNoSql> myNoSqlServerDataWriter)
        {
            _logger = logger;
            _dbContextOptionsBuilder = dbContextOptionsBuilder;
            _publisherLeadUpdated = publisherLeadUpdated;
            _myNoSqlServerDataWriter = myNoSqlServerDataWriter;
        }

        public async Task<LeadCreateResponse> CreateAsync(Bridge.Grpc.Models.Leads.Contracts.LeadCreateRequest request)
        {
            _logger.LogInformation("Creating new LeadInfo {@context}", request);
            using var ctx = new DatabaseContext(_dbContextOptionsBuilder.Options);

            try
            {

                //await _publisherLeadUpdated.PublishAsync(MapToMessage(leadEntity));
                //_logger.LogInformation("Sent partner update to service bus {@context}", request);

                //var nosql = MapToNoSql(leadEntity);
                //await _myNoSqlServerDataWriter.InsertOrReplaceAsync(nosql);
                //_logger.LogInformation("Sent partner update to MyNoSql {@context}", request);

                var brandInfo = await BrandRegisterAsync(null);

                return MapToGrpc(null, null);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error creating lead {@context}", request);

                return new LeadCreateResponse() { Error = new Error() { Message = "Internal error", Type = ErrorType.Unknown } };
            }
        }

        public async Task<Grpc.Models.Leads.LeadBrandInfo> BrandRegisterAsync(LeadEntity leadEntity)
        {
            string brandLoginUrl = @"https://trading-test.handelpro.biz/lpLogin/6DB5D4818181B806DBF7B19EBDC5FD97F1B82759077317B6481BC883F071783DBEF568426B81DF43044E326C26437E097F21A2484110D13420E9EC6E44A1B2BE?lang=PL";
            string brandName = "Monfex";
            string brandCustomerId = "02537c06cab34f62931c263bf3480959";
            string customerEmail = "yuriy.test.2020.09.22.01@mailinator.com";
            string brandToken = "6DB5D4818181B806DBF7B19EBDC5FD97F1B82759077317B6481BC883F071783DBEF568426B81DF43044E326C26437E097F21A2484110D13420E9EC6E44A1B2BE";

            var brandInfo = new Grpc.Models.Leads.LeadBrandInfo()
            {
                //Status = "successful",
                //Data = new LeadBrandRegistrationInfo()
                //{
                //    Email = customerEmail, //leadEntity.Email,
                //    UniqueId = leadEntity.LeadId.ToString(),
                //    LoginUrl = brandLoginUrl,
                //    Broker = brandName,
                //    CustomerId = brandCustomerId,
                //    Token = brandToken
                //}
            };
            await Task.Delay(1000);
            return brandInfo;
        }


        private static LeadCreateResponse MapToGrpc(LeadEntity leadEntity, 
            Grpc.Models.Leads.LeadBrandRegistrationInfo brandInfo)
        {
            //TODO: Remove
            return new LeadCreateResponse() 
            {
                //Status = ,
                //FallbackUrl = String.Empty,
                //Message = .Data.LoginUrl,
                //Error = null,
                //OriginalData = null,
            };
        }

        private static DepositUpdateMessage MapToMessage(LeadEntity leadEntity)
        {
            return new DepositUpdateMessage()
            {
                //TenantId = leadEntity.TenantId,
                //AffiliateId = leadEntity.LeadId,
                //GeneralInfo = new Messages.Partners.PartnerGeneralInfo()
                //{
                //    //CreatedAt = leadEntity.BrandInfo.CreatedAt.UtcDateTime,
                //    //Email = leadEntity.BrandInfo.Email,
                //    ////Password = leadEntity.BrandInfo.Password,
                //    //Phone = leadEntity.BrandInfo.Phone,
                //    //Role = leadEntity.BrandInfo.Role.MapEnum<Messages.Partners.PartnerRole>(),
                //    //Skype = leadEntity.BrandInfo.Skype,
                //    //Type = leadEntity.BrandInfo.Type.MapEnum<Messages.Partners.PartnerState>(),
                //    //Username = leadEntity.BrandInfo.Username,
                //    //ZipCode = leadEntity.BrandInfo.ZipCode
                //}
            };
        }

        private static LeadNoSql MapToNoSql(LeadEntity leadEntity)
        {
            return LeadNoSql.Create(
                leadEntity.TenantId,
                leadEntity.LeadId,
                new MyNoSql.Leads.LeadGeneralInfo()
                {
                    CreatedAt = leadEntity.CreatedAt,
                    Email = leadEntity.Email,
                    Username = leadEntity.FirstName + " " + leadEntity.LastName
                }
                );
        }
    }
}
