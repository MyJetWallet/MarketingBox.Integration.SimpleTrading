using System;
using MarketingBox.Integration.SimpleTrading.Bridge.Services.Integrations.Contracts.Enums;
using Newtonsoft.Json;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Services.Integrations.Contracts.Responses
{
    public class StatisticResponse
    {
        [JsonProperty("responseTime")] public string ResponseTime { get; set; }

    }
}