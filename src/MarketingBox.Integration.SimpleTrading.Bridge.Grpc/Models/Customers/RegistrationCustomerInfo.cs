using System.Runtime.Serialization;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Customers
{
    [DataContract]
    public class 
        RegistrationCustomerInfo
    {
        [DataMember(Order = 1)]
        public string CustomerId { get; set; }

        [DataMember(Order = 2)]
        public string Password { get; set; }

        [DataMember(Order = 3)]
        public string Token { get; set; }

        [DataMember(Order = 5)]
        public string LoginUrl { get; set; }
    }
}