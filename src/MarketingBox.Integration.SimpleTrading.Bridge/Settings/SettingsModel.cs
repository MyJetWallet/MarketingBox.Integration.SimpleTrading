using MyJetWallet.Sdk.Service;
using MyYamlParser;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Settings
{
    public class SettingsModel
    {
        [YamlProperty("MarketingBoxIntegrationService.SeqServiceUrl")]
        public string SeqServiceUrl { get; set; }

        [YamlProperty("MarketingBoxIntegrationService.ZipkinUrl")]
        public string ZipkinUrl { get; set; }

        [YamlProperty("MarketingBoxIntegrationService.ElkLogs")]
        public LogElkSettings ElkLogs { get; set; }

        //[YamlProperty("MarketingBoxIntegrationService.PostgresConnectionString")]
        //public string PostgresConnectionString { get; set; }

        [YamlProperty("MarketingBoxIntegrationService.MyNoSqlWriterUrl")]
        public string MyNoSqlWriterUrl { get; set; }

        //[YamlProperty("MarketingBoxIntegrationService.MyNoSqlReaderHostPort")]
        //public string MyNoSqlReaderHostPort { get; set; }

        [YamlProperty("MarketingBoxIntegrationService.MarketingBoxServiceBusHostPort")]
        public string MarketingBoxServiceBusHostPort { get; set; }

        [YamlProperty("MarketingBoxIntegrationService.IntegrationServiceUrl")]
        public string IntegrationServiceUrl { get; set; }

        [YamlProperty("MarketingBoxIntegrationService.Brand.Url")]
        public string BrandUrl { get; set; }

        [YamlProperty("MarketingBoxIntegrationService.Brand.AffiliateId")]
        public string BrandAffiliateId { get; set; }

        [YamlProperty("MarketingBoxIntegrationService.Brand.AffiliateKey")]
        public string BrandAffiliateKey { get; set; }
    }
}
