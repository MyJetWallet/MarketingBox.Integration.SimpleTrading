using System.Runtime.Serialization;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Leads.Contracts
{
    [DataContract]
    public class LeadCreateRequest
    {
        [DataMember(Order = 1)]
        public string TenantId { get; set; }

        [DataMember(Order = 2)]
        public LeadInfo LeadInfo { get; set; }

        [DataMember(Order = 3)]
        public LeadBrandInfo BrandInfo { get; set; }
    }
}
