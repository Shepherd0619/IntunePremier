using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutopilotHelper.Models
{
    public class AutopilotDDSZTD
    {
        public string AutopilotServiceCorrelationId { get; set; }
        public string ZtdRegistrationId { get; set; }
        public string AadDeviceId { get; set; }
        public int CloudAssignedOobeConfig { get; set; }
        public int CloudAssignedDomainJoinMethod { get; set; }
        public int CloudAssignedForcedEnrollment { get; set; }
        public string CloudAssignedTenantDomain { get; set; }
        public string CloudAssignedTenantId { get; set; }
        public string CloudAssignedMdmId { get; set; }
        public string CloudAssignedRegion { get; set; }
        public string CloudAssignedLanguage { get; set; }
        public string DeploymentProfileName { get; set; }
        public bool IsExplicitProfileAssignment { get; set; }
        public int CloudAssignedAutopilotUpdateDisabled { get; set; }
        public int CloudAssignedPrivacyDiagnostics { get; set; }
        public int HybridJoinSkipDCConnectivityCheck { get; set; }
        public int CloudAssignedAutopilotUpdateTimeout { get; set; }
        public DateTime PolicyDownloadDate { get; set; }
        public string PolicyDownloadCorrelationVector { get; set; }

        public override string ToString()
        {
            var properties = GetType().GetProperties();
            var sb = new StringBuilder();

            foreach (var property in properties)
            {
                if (!string.IsNullOrEmpty(property.GetValue(this).ToString()))
                {
                    sb.Append($"{property.Name}: {property.GetValue(this)}\n");
                }
            }

            return sb.ToString().TrimEnd('\n');
        }
    }
}
