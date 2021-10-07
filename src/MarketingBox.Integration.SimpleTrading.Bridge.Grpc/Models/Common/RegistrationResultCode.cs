using System.ComponentModel;

namespace MarketingBox.Integration.SimpleTrading.Bridge.Grpc.Models.Common
{
    public enum RegistrationResultCode
    {
        [Description("Registration failed")]
        Failed = 0,
        [Description("Registration completed successfully")]
        CompletedSuccessfully = 1,
        [Description("Required brand authentication")]
        RequiredAuthentication = 2,
    }
}