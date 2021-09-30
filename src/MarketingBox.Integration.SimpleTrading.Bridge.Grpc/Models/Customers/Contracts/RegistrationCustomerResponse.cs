using System.Runtime.Serialization;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Common;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Customers;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Leads.Contracts
{
    [DataContract]
    public class RegistrationCustomerResponse
    {
        [DataMember(Order = 1)]
        public string Status { get; set; }

        [DataMember(Order = 2)]
        public string Message { get; set; }

        [DataMember(Order = 3)]
        public RegistrationCustomerInfo RegistrationInfo { get; set; }

        [DataMember(Order = 4)]
        public string FallbackUrl { get; set; }


        [DataMember(Order = 100)]
        public Error Error { get; set; }

        public static RegistrationCustomerResponse Successfully(RegistrationCustomerInfo brandRegistrationInfo)
        {
            return new RegistrationCustomerResponse()
            {
                Status = "successful",
                Message = brandRegistrationInfo.LoginUrl,
                RegistrationInfo = brandRegistrationInfo
            };
        }

        public static RegistrationCustomerResponse Failed(Error error)
        {
            return new RegistrationCustomerResponse()
            {
                Status = "failed",
                Message = error.Message,
                Error = error
            };
        }
    }
}