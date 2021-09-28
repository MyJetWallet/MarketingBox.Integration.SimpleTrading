using MyJetWallet.Sdk.Postgres;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Postgres.DesignTime
{
    public class ContextFactory : MyDesignTimeContextFactory<DatabaseContext>
    {
        public ContextFactory() : base(options => new DatabaseContext(options))
        {

        }
    }
}