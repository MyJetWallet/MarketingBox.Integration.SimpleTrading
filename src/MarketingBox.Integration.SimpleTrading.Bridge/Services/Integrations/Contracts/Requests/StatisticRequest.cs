using Destructurama.Attributed;
using Newtonsoft.Json;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Services.Integrations.Contracts.Requests
{
    public class StatisticRequest
    {
        [JsonProperty("requestTime")] public string RequestTime { get; set; }
        [JsonProperty("apiVersion")] public string ApiVersion { get; set; }
        [LogMasked(ShowFirst = 1, ShowLast = 1, PreserveLength = true)]
        [JsonProperty("mId")] public string Mid { get; set; }
        [LogMasked(ShowFirst = 1, ShowLast = 1, PreserveLength = true)]
        [JsonProperty("maId")] public string MaId { get; set; }
        [LogMasked]
        [JsonProperty("userName")] public string UserName { get; set; }
        [LogMasked]
        [JsonProperty("password")] public string Password { get; set; }
        [JsonProperty("txId")] public string TxId { get; set; }
    }
}