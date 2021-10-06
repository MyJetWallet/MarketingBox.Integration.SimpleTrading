using JetBrains.Annotations;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc;
using MyJetWallet.Sdk.Grpc;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Client
{
    [UsedImplicitly]
    public class SimpleTradingBridgeClientFactory: MyGrpcClientFactory
    {
        public SimpleTradingBridgeClientFactory(string grpcServiceUrl) : base(grpcServiceUrl)
        {
        }

        public IRegisterService GetPartnerService() => CreateGrpcService<IRegisterService>();
    }
}
