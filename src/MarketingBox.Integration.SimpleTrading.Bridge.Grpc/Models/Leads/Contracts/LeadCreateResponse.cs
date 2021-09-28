using System.Runtime.Serialization;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Common;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Leads.Contracts
{
    [DataContract]
    public class LeadCreateResponse
    {
        [DataMember(Order = 1)]
        public string Status { get; set; }

        [DataMember(Order = 2)]
        public string Message { get; set; }

        [DataMember(Order = 3)]
        public LeadBrandRegistrationInfo RegistrationInfo { get; set; }

        [DataMember(Order = 4)]
        public string FallbackUrl { get; set; }

        [DataMember(Order = 5)]
        public LeadGeneralInfo OriginalData { get; set; }

        [DataMember(Order = 100)]
        public Error Error { get; set; }

        public static LeadCreateResponse Successfully(LeadBrandRegistrationInfo brandRegistrationInfo)
        {
            return new LeadCreateResponse()
            {
                Status = "successful",
                Message = brandRegistrationInfo.LoginUrl,
                RegistrationInfo = brandRegistrationInfo
            };
        }

        public static LeadCreateResponse Failed(Error error, LeadGeneralInfo originalData)
        {
            return new LeadCreateResponse()
            {
                Status = "failed",
                Message = error.Message,
                Error = error,
                OriginalData = originalData
            };
        }
    }
}