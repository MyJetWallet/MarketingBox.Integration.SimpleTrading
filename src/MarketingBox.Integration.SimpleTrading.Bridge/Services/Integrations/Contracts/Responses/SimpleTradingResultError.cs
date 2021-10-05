using Newtonsoft.Json;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Services.Integrations.Contracts.Responses
{
    public class SimpleTradingResultError
    {
        [JsonProperty("errorCode")] public string ErrorCode { get; set; }
        [JsonProperty("errorMessage")] public string ErrorMessage { get; set; }
        [JsonProperty("advice")] public string Advice { get; set; }
    }
}
