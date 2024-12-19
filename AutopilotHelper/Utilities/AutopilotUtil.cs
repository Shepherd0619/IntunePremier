using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutopilotHelper.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AutopilotHelper.Utilities
{
    public class AutopilotUtil
    {
        public MDMFileUtil MDMDiag => _MDMDiag;
        private MDMFileUtil _MDMDiag;

        public AutopilotUtil(MDMFileUtil file)
        {
            _MDMDiag = file;

            GetLocalAutopilotProfileStatus();
        }

        public AutopilotDDSZTD? GetLocalAutopilotProfileStatus()
        {
            var path = Path.Combine(MDMDiag.TmpWorkplacePath, "AutopilotDDSZTDFile.json");
            if (!File.Exists(path)) return null;
            return JsonConvert.DeserializeObject<AutopilotDDSZTD>(File.ReadAllText(path));
        }

        public List<EventViewerFile.Record> GetCloudSessionHostRecords()
        {
            EventViewerFile file = new(Path.Combine(MDMDiag.TmpWorkplacePath, 
                "microsoft-windows-shell-core-operational.evtx"));

            return file.records
                .Where(search => !string.IsNullOrEmpty(search.FormatDescription) 
                && search.FormatDescription.StartsWith("CloudExperienceHost"))
                .OrderByDescending(search => search.TimeCreated)
                .ToList();
        }

        /// <summary>
        /// Old school diagnostics report like https://github.com/silvermarkg/Autopilot/tree/main.
        /// </summary>
        /// <returns></returns>
        public string GetGeneralDiagnosticsReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("AUTOPILOT DIAGNOSTICS");
            sb.AppendLine();

            var autopilotLocalProfile = GetLocalAutopilotProfileStatus();
            if (autopilotLocalProfile == null)
            {
                sb.AppendLine("This is not an Autopilot device or autopilot profile download failed!");
                return sb.ToString();
            }

            //// Profile Information
            //sb.Append("Profile:                  ").AppendLine(autopilotLocalProfile.DeploymentProfileName);
            //sb.Append("TenantDomain:             ").AppendLine(autopilotLocalProfile.CloudAssignedTenantDomain);
            //sb.Append("TenantID:                 ").AppendLine(autopilotLocalProfile.CloudAssignedTenantId);

            //// Correlation Information
            //sb.Append("ZTDID:                    ").AppendLine(autopilotLocalProfile.ZtdRegistrationId);
            ////sb.Append("EntDMID:                  ").AppendLine(autopilotLocalProfile.EntDMID);

            sb.AppendLine(autopilotLocalProfile.ToString());

            sb.AppendLine();

            #region Read Registry file
            object ConvertValue(string value)
            {
                try
                {
                    return int.Parse(value);
                }
                catch (FormatException)
                {
                    return value;
                }
            }
            var reg = new RegFileUtil(MDMDiag);
            #endregion

            #region OobeConfig
            
            int? configValue = Convert.ToInt32
                (reg.GetValue("HKEY_LOCAL_MACHINE\\software\\microsoft\\provisioning\\Diagnostics\\AutoPilot", 
                "\"CloudAssignedOobeConfig\"").Split(new char[] { ':' }, 2)[1].Trim(), 16);

            if (configValue == null) return sb.ToString();

            sb.AppendLine("OobeConfig:               " + configValue);

            string[] skipKeyboardMask = new string[] { "1", "0" };
            int skipKeyboardBit = 10;
            sb.AppendLine(" Skip keyboard:           " + ((configValue & (1 << skipKeyboardBit)) != 0 ? "Yes   " : "No    ") + skipKeyboardMask[0] + " - - - - - - - - - -");

            string[] enablePatchDownloadMask = new string[] { "-1", "-0" };
            int enablePatchDownloadBit = 9;
            sb.AppendLine(" Enable patch download:   " + ((configValue & (1 << enablePatchDownloadBit)) != 0 ? "Yes   - " : "No    - ") + enablePatchDownloadMask[0] + " - - - - - - - -");

            string[] skipWindowsUpgradeUXMask = new string[] { "-1", "-0" };
            int skipWindowsUpgradeUXBit = 8;
            sb.AppendLine(" Skip Windows upgrade UX: " + ((configValue & (1 << skipWindowsUpgradeUXBit)) != 0 ? "Yes   - - " : "No    - - ") + skipWindowsUpgradeUXMask[0] + " - - - -");

            string[] aadTpmRequiredMask = new string[] { "-1", "-0" };
            int aadTpmRequiredBit = 7;
            sb.AppendLine(" AAD TPM Required:        " + ((configValue & (1 << aadTpmRequiredBit)) != 0 ? "Yes   - - - " : "No    - - - ") + aadTpmRequiredMask[0] + " - - -");

            string[] aadDeviceAuthMask = new string[] { "-1", "-0" };
            int aadDeviceAuthBit = 6;
            sb.AppendLine(" AAD device auth:         " + ((configValue & (1 << aadDeviceAuthBit)) != 0 ? "Yes   - - - - " : "No    - - - - ") + aadDeviceAuthMask[0] + " -");

            string[] tpmAttestationMask = new string[] { "-1", "-0" };
            int tpmAttestationBit = 5;
            sb.AppendLine(" TPM attestation:         " + ((configValue & (1 << tpmAttestationBit)) != 0 ? "Yes   - - - - - " : "No    - - - - - ") + tpmAttestationMask[0] + " -");

            string[] skipEulaMask = new string[] { "-1", "-0" };
            int skipEulaBit = 4;
            sb.AppendLine(" Skip EULA:               " + ((configValue & (1 << skipEulaBit)) != 0 ? "Yes   - - - - - - " : "No    - - - - - - ") + skipEulaMask[0]);

            string[] skipOemRegistrationMask = new string[] { "-1", "-0" };
            int skipOemRegistrationBit = 3;
            sb.AppendLine(" Skip OEM registration:   " + ((configValue & (1 << skipOemRegistrationBit)) != 0 ? "Yes   - - - - - - - " : "No    - - - - - - - ") + skipOemRegistrationMask[0]);

            string[] skipExpressSettingsMask = new string[] { "-1", "-0" };
            int skipExpressSettingsBit = 2;
            sb.AppendLine(" Skip express settings:   " + ((configValue & (1 << skipExpressSettingsBit)) != 0 ? "Yes   - - - - - - - - " : "No    - - - - - - - - ") + skipExpressSettingsMask[0]);

            string[] disallowAdminMask = new string[] { "-1", "-0" };
            int disallowAdminBit = 1;
            sb.AppendLine(" Disallow admin:          " + ((configValue & (1 << disallowAdminBit)) != 0 ? "Yes   - - - - - - - - - " : "No    - - - - - - - - - ") + disallowAdminMask[0]);
            #endregion

            #region Autopilot Profile
            if(autopilotLocalProfile.CloudAssignedDomainJoinMethod == 1)
            {
                sb.AppendLine("Scenario: Hybrid Entra Join");
                sb.AppendLine("Skip Local AD Connectivity Check: " + (autopilotLocalProfile.HybridJoinSkipDCConnectivityCheck == 1 ? "YES":"NO"));
            }
            else
            {
                sb.AppendLine("Scenario: Entra Join");
            }
            #endregion

            return sb.ToString();
        }
    }
}
