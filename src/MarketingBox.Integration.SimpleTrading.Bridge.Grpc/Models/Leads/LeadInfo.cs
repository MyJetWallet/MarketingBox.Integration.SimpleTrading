using System.Runtime.Serialization;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Leads
{
    [DataContract]
    public class LeadInfo
    {
        [DataMember(Order = 1)]
        public string LeadId { get; set; }

        [DataMember(Order = 2)]
        public string UniqueId { get; set; }

        [DataMember(Order = 3)]
        public LeadGeneralInfo GeneralInfo { get; set; }

        [DataMember(Order = 4)]
        public LeadAdditionalInfo AdditionalInfo { get; set; }
    }
}
