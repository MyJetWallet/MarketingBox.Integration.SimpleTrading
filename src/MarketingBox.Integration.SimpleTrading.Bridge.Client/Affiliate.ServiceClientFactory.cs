using JetBrains.Annotations;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc;
using MyJetWallet.Sdk.Grpc;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Client
{
    [UsedImplicitly]
    public class IntegrationServiceClientFactory: MyGrpcClientFactory
    {
        public IntegrationServiceClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IIntegrationService GetPartnerService() => CreateGrpcService<IIntegrationService>();
    }
}
