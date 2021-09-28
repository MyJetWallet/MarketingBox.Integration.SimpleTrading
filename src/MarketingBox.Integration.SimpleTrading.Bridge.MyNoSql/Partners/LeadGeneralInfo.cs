using System;
using MarketingBox.Integration.SimpleTrading.Bridge.MyNoSql.Common;

namespace MarketingBox.Integration.SimpleTrading.Bridge.MyNoSql.Leads
{
    public class LeadGeneralInfo
    {
        public string Username { get; set; }
        
        public string Email { get; set; }
        
        public string Phone { get; set; }
        
        public PartnerRole Role { get; set; }
        
        public PartnerState State { get; set; }
        
        public Currency Currency { get; set; }
        
        public DateTime CreatedAt { get; set; }
    }
}