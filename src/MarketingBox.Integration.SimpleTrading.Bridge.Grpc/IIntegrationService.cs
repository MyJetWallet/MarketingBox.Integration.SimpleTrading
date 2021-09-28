using System.ServiceModel;
using System.Threading.Tasks;
using MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Leads.Contracts;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Grpc
{
    [ServiceContract]
    public interface IIntegrationService
    {
        [OperationContract]
        Task<LeadCreateResponse> CreateAsync(LeadCreateRequest request);
    }
}
