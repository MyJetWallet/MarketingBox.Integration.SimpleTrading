using Autofac;
using MarketingBox.Integration.SimpleTrading.Bridge.Services.Integrations;
using MyJetWallet.Sdk.ServiceBus;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Modules
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var serviceBusClient = builder
                .RegisterMyServiceBusTcpClient(
                    Program.ReloadedSettings(e => e.MarketingBoxServiceBusHostPort),
                    Program.LogFactory);

            //var noSqlClient = builder.CreateNoSqlClient(Program.ReloadedSettings(e => e.MyNoSqlReaderHostPort));

            #region Leads

            // publisher (IPublisher<DepositUpdateMessage>)
            //builder.RegisterMyServiceBusPublisher<DepositUpdateMessage>(serviceBusClient, Topics.LeadDepositUpdateTopic, false);

            // publisher (IPublisher<PartnerRemoved>)
            //builder.RegisterMyServiceBusPublisher<PartnerRemoved>(serviceBusClient, Topics.LeadUpdatedTopic, false);

            // register writer (IMyNoSqlServerDataWriter<LeadNoSql>)
            //builder.RegisterMyNoSqlWriter<LeadNoSql>(Program.ReloadedSettings(e => e.MyNoSqlWriterUrl), LeadNoSql.TableName);

            #endregion
            builder.RegisterType<SimpleTradingHttpClient>()
                .As<ISimpleTradingHttpClient>()
                .WithParameter("baseUrl", Program.ReloadedSettings(e => e.BrandUrl).Invoke())
                .SingleInstance();
        }
    }
}
