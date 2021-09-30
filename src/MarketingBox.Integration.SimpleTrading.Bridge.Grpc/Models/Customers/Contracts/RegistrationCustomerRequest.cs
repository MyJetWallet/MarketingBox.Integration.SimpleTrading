using System.Runtime.Serialization;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Customers;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Leads.Contracts
{
    [DataContract]
    public class RegistrationCustomerRequest
    {
        [DataMember(Order = 1)]
        public string TenantId { get; set; }

        [DataMember(Order = 2)]
        public long LeadId { get; set; }

        [DataMember(Order = 3)]
        public string LeadUniqueId { get; set; }

        [DataMember(Order = 4)]
        public RegistrationLeadInfo Info { get; set; }

        [DataMember(Order = 5)]
        public RegistrationLeadAdditionalInfo AdditionalInfo { get; set; }
    }
}
