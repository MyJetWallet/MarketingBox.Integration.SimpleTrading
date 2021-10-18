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

            builder.RegisterType<SimpleTradingHttpClient>()
                .As<ISimpleTradingHttpClient>()
                .WithParameter("baseUrl", Program.ReloadedSettings(e => e.BrandUrl).Invoke())
                .SingleInstance();
        }
    }
}
