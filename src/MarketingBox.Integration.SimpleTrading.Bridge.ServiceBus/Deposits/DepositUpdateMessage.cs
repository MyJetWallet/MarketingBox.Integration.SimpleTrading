using System.Runtime.Serialization;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Messages.Deposits
{
    [DataContract]
    public class DepositUpdateMessage
    {
        [DataMember(Order = 1)]
        public string TenantId { get; set; }

        [DataMember(Order = 2)]
        public string CustomerId { get; set; }

        [DataMember(Order = 3)]
        public string Email { get; set; }

        [DataMember(Order = 4)]
        public string BrandName { get; set; }

        [DataMember(Order = 5)]
        public long Sequence { get; set; }
    }
}
