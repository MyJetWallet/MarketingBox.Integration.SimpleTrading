using Autofac;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc;

// ReSharper disable UnusedMember.Global

namespace MarketingBox.Integration.SimpleTrading.Bridge.Client
{
    public static class AutofacHelper
    {
        public static void RegisterSimpleTradingBridgeClient(this ContainerBuilder builder, string grpcServiceUrl)
        {
            var factory = new SimpleTradingBridgeClientFactory(grpcServiceUrl);

            builder.RegisterInstance(factory.GetPartnerService()).As<IRegisterService>().SingleInstance();
        }
    }
}
