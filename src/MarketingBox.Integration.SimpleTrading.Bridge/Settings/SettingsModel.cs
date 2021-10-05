using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Settings
{
    public class SettingsModel
    {
        [YamlProperty("MarketingBoxIntegrationSimpleTradingBridge.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("MarketingBoxIntegrationSimpleTradingBridge.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("MarketingBoxIntegrationSimpleTradingBridge.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }

        //[YamlProperty("MarketingBoxIntegrationService.PostgresConnectionString")]
        //public string PostgresConnectionString { get; set; }

        [YamlProperty("MarketingBoxIntegrationSimpleTradingBridge.MyNoSqlWriterUrl")]
        public string MyNoSqlWriterUrl { get; set; }

        //[YamlProperty("MarketingBoxIntegrationService.MyNoSqlReaderHostPort")]
        //public string MyNoSqlReaderHostPort { get; set; }

        [YamlProperty("MarketingBoxIntegrationSimpleTradingBridge.MarketingBoxServiceBusHostPort")]
        public string MarketingBoxServiceBusHostPort { get; set; }

        [YamlProperty("MarketingBoxIntegrationSimpleTradingBridge.IntegrationServiceUrl")]
        public string IntegrationServiceUrl { get; set; }

        [YamlProperty("MarketingBoxIntegrationSimpleTradingBridge.Brand.Url")]
        public string BrandUrl { get; set; }

        [YamlProperty("MarketingBoxIntegrationSimpleTradingBridge.Brand.AffiliateId")]
        public string BrandAffiliateId { get; set; }

        [YamlProperty("MarketingBoxIntegrationSimpleTradingBridge.Brand.BrandId")]
        public string BrandBrandId { get; set; }


        [YamlProperty("MarketingBoxIntegrationSimpleTradingBridge.Brand.AffiliateKey")]
        public string BrandAffiliateKey { get; set; }
    }
}
