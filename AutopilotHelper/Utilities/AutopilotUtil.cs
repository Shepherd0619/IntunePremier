using AutopilotHelper.Models;
using Newtonsoft.Json;
using System.Data;
using System.Diagnostics;
using System.Text;
using System.Xml;

namespace AutopilotHelper.Utilities
{
    public class AutopilotUtil
    {
        public MDMFileUtil MDMDiag
        {
            get => _MDMDiag;

            set
            {
                _MDMDiag = value;
                try
                {
                    _Reg = new RegFileUtil(MDMDiag);
                }
                catch
                {
                    Console.WriteLine("ERROR: Autopilot registry is missing!");
                }
            }
        }
        private MDMFileUtil _MDMDiag;
        private RegFileUtil _Reg;

        public List<NodeCache> NodeCaches => _NodeCaches;
        private List<NodeCache> _NodeCaches;

        public AutopilotUtil(MDMFileUtil file)
        {
            MDMDiag = file;

            GetLocalAutopilotProfileStatus();
        }

        public AutopilotDDSZTD? GetLocalAutopilotProfileStatus()
        {
            var path = Path.Combine(MDMDiag.TmpWorkspacePath, "AutopilotDDSZTDFile.json");
            if (!File.Exists(path)) return null;
            return JsonConvert.DeserializeObject<AutopilotDDSZTD>(File.ReadAllText(path));
        }

        public List<EventViewerFile.Record> GetCloudSessionHostRecords()
        {
            EventViewerFile file = new(Path.Combine(MDMDiag.TmpWorkspacePath,
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

            #region Read Registry file
            if (_Reg == null)
            {
                sb.AppendLine("ERROR: Autopilot registry is missing!");
                return sb.ToString();
            }

            var autopilotRegPath = "HKEY_LOCAL_MACHINE\\software\\microsoft\\provisioning\\Diagnostics\\AutoPilot";
            var correlationsPath = "HKEY_LOCAL_MACHINE\\software\\microsoft\\provisioning\\Diagnostics\\AutoPilot\\EstablishedCorrelations";
            #endregion

            var autopilotLocalProfile = GetLocalAutopilotProfileStatus();
            if (autopilotLocalProfile == null)
            {
                sb.AppendLine("WARNING: Possibly Autopilot profile download failed!");
                sb.AppendLine();
            }

            #region Autopilot Registry
            try
            {
                // Profile Information
                sb.Append("Profile:                  ").AppendLine(_Reg.GetValue(autopilotRegPath, "DeploymentProfileName"));
                sb.Append("TenantDomain:             ").AppendLine(_Reg.GetValue(autopilotRegPath, "CloudAssignedTenantDomain"));
                sb.Append("TenantID:                 ").AppendLine(_Reg.GetValue(autopilotRegPath, "CloudAssignedTenantId"));
            }
            catch (Exception ex)
            {
                sb.AppendLine($"Unable to fetch profile info from registry!\n\n{ex}");
            }

            try
            {
                // Correlation Information
                sb.Append("ZTDID:                    ").AppendLine(_Reg.GetValue(correlationsPath, "ZtdRegistrationId"));
                sb.Append("EntDMID:                  ").AppendLine(_Reg.GetValue(correlationsPath, "EntDMID"));
            }
            catch (Exception ex)
            {
                sb.AppendLine($"Unable to fetch correlation info from registry!\n\n{ex}");
            }
            //sb.AppendLine(autopilotLocalProfile.ToString());

            sb.AppendLine();
            #endregion

            // TODO: HKEY_LOCAL_MACHINE\software\microsoft\provisioning\AutopilotSettings
            #region ESP
            try
            {
                //[HKEY_LOCAL_MACHINE\software\microsoft\provisioning\OMADM\Logger]
                //"CurrentEnrollmentId" = "4F823456-CCB7-4F35-9162-6860AEB328FC"
                var espPath =
                    $"HKEY_LOCAL_MACHINE\\software\\microsoft\\enrollments\\{_Reg.GetValue("HKEY_LOCAL_MACHINE\\software\\microsoft\\provisioning\\OMADM\\Logger", "CurrentEnrollmentId")}";
                var firstSync = $"{espPath}\\FirstSync";
                sb.AppendLine("Enrollment status page:");
                var skipDeviceStatusPage = _Reg.GetValue(firstSync, "SkipDeviceStatusPage").Split(new char[] { ':' }, 2)[1].Trim();
                sb.AppendLine("Device ESP enabled: " + (Convert.ToInt32(skipDeviceStatusPage, 16) == 0 ? "True" : "False"));
                var skipUserStatusPage = _Reg.GetValue(firstSync, "SkipUserStatusPage").Split(new char[] { ':' }, 2)[1].Trim();
                sb.AppendLine("User ESP enabled: " + (Convert.ToInt32(skipUserStatusPage, 16) == 0 ? "True" : "False"));
                var timeout = _Reg.GetValue(firstSync, "SyncFailureTimeout").Split(new char[] { ':' }, 2)[1].Trim();
                sb.AppendLine($"ESP Timeout: {Convert.ToInt32(timeout, 16)}");

                var espBlockingValue = _Reg.GetValue(firstSync, "BlockInStatusPage").Split(new char[] { ':' }, 2)[1].Trim();
                var espBlockingFlag = Convert.ToInt32(espBlockingValue, 16);
                var espBlocking = espBlockingFlag == 0;

                sb.AppendLine("ESP Blocking: " + (espBlocking ? "True" : "False"));

                if (espBlocking)
                {
                    if ((espBlockingFlag & 1) != 0)
                    {
                        sb.AppendLine("ESP allow reset:         Yes");
                    }

                    if ((espBlockingFlag & 2) != 0)
                    {
                        sb.AppendLine("ESP allow try again:     Yes");
                    }

                    if ((espBlockingFlag & 4) != 0)
                    {
                        sb.AppendLine("ESP continue anyway:     Yes");
                    }
                }
            }
            catch (Exception ex)
            {
                sb.AppendLine($"ESP Diagnostics is unavailable this time!\n\n{ex}");
            }

            sb.AppendLine();
            #endregion

            #region OobeConfig

            int? configValue = Convert.ToInt32
                (_Reg.GetValue(autopilotRegPath,
                "CloudAssignedOobeConfig").Split(new char[] { ':' }, 2)[1].Trim(), 16);

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
            if (autopilotLocalProfile.CloudAssignedDomainJoinMethod == 1)
            {
                sb.AppendLine("Scenario: Hybrid Entra Join");
                sb.AppendLine("Skip Local AD Connectivity Check: " + (autopilotLocalProfile.HybridJoinSkipDCConnectivityCheck == 1 ? "YES" : "NO"));
            }
            else
            {
                sb.AppendLine("Scenario: Entra Join");
            }

            sb.AppendLine();
            #endregion

            #region Hardware Hash
            try
            {
                sb.AppendLine(DecodeHardwareHash());
            }
            catch (Exception ex)
            {
                sb.AppendLine("Failed to decode hardware hash. Please check whether ADK is installed and AutopilotHash CSV file exist.");
                sb.AppendLine("To install ADK, please visit https://learn.microsoft.com/en-us/windows-hardware/get-started/adk-install");
                sb.AppendLine();
                sb.AppendLine(ex.ToString());
            }
            #endregion

            //sb.AppendLine(GetProcessedPolicies());
            sb.AppendLine("For processed policies, please check the \"Processed Policies\" tab for further details.");
            sb.AppendLine();

            sb.AppendLine(GetProcessedApps());

            sb.AppendLine();

            sb.AppendLine("New ESP report:");
            try
            {
                var esp = GetAutopilotSettingsFromRegistry();
                var accountSetup = JsonConvert.DeserializeObject<AccountSetupCategory>(esp.AccountSetup.Status);
                var devicePreparation = JsonConvert.DeserializeObject<DevicePreparationCategory>(esp.DevicePreparation.Status);
                var deviceSetup = JsonConvert.DeserializeObject<DeviceSetupCategory>(esp.DeviceSetup.Status);
                sb.AppendLine();
                sb.AppendLine("Device Preparation:");
                sb.AppendLine(devicePreparation.ToString());
                sb.AppendLine();
                sb.AppendLine("Device Setup:");
                sb.AppendLine(deviceSetup.ToString());
                sb.AppendLine();
                sb.AppendLine("Account Setup:");
                sb.AppendLine(accountSetup.ToString());
            }
            catch (Exception ex)
            {
                sb.AppendLine($"Not available this time.\n\n{ex}");
            }

            return sb.ToString();
        }

        public string DecodeHardwareHash()
        {
            // Check if ADK is installed and OA3 Tool exists
            string adkPath = AdkUtil.GetAdkPath();
            string oa3ToolPath = AdkUtil.GetOA3ToolPath(adkPath);
            var hash = string.Empty;

            if (oa3ToolPath != null && !string.IsNullOrEmpty(oa3ToolPath))
            {
                // Grab hash from csv
                var fileList = Directory.GetFiles(_MDMDiag.TmpWorkspacePath);
                for (int i = 0; i < fileList.Length; i++)
                {
                    var fileName = Path.GetFileName(fileList[i]);
                    var extension = Path.GetExtension(fileList[i]);
                    if (fileName.StartsWith("DeviceHash_") && extension == ".csv")
                    {
                        using (StreamReader sr = new StreamReader(fileList[i]))
                        {
                            // 跳过标题行
                            sr.ReadLine();

                            while (!sr.EndOfStream)
                            {
                                string line = sr.ReadLine();
                                string[] columns = line.Split(',');

                                hash = columns[2];
                            }

                            break;
                        }
                    }
                }

                // Decode hardware hash
                string commandLineArgs = $"/DecodeHwHash:{hash}";
                Process process = new Process();
                process.StartInfo.FileName = oa3ToolPath;
                process.StartInfo.Arguments = commandLineArgs;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.RedirectStandardOutput = true;
                process.Start();

                // Read output
                string output = "";
                while (!process.StandardOutput.EndOfStream)
                {
                    output += process.StandardOutput.ReadLine();
                }

                // Close the process
                process.WaitForExit();

                // 处理掉刚开始的工具版本信息。
                int startIndex = output.IndexOf("Decoded Hardware Hash:") + "Decoded Hardware Hash:".Length;
                output = output.Substring(startIndex);

                // 处理掉结尾的信息
                startIndex = output.IndexOf("</HardwareInventory></HardwareReport>") + "</HardwareInventory></HardwareReport>".Length;
                output = output.Remove(startIndex);

                // Parse XML output
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.LoadXml(output);

                // Display hardware information
                StringBuilder sb = new StringBuilder("Hardware Information:\n");

                XmlNodeList nodeList = xmlDoc.GetElementsByTagName("p");
                foreach (XmlNode node in nodeList)
                {
                    if (node.Attributes["n"] != null && node.Attributes["v"] != null)
                    {
                        string key = node.Attributes["n"].Value;
                        string value = node.Attributes["v"].Value;

                        switch (key)
                        {
                            case "OsBuild":
                                sb.Append($"Operating System Build: {value}\n");
                                break;
                            case "SmbiosSystemManufacturer":
                                sb.Append($"Manufacturer: {value}\n");
                                break;
                            case "SmbiosSystemProductName":
                                sb.Append($"Model: {value}\n");
                                break;
                            case "SmbiosSystemSerialNumber":
                                sb.Append($"Serial Number: {value}\n");
                                break;
                            case "TPMVersion":
                                sb.Append($"TPM Version: {value}\n");
                                break;
                        }
                    }
                }

                return sb.ToString();
            }

            throw new VersionNotFoundException();
        }

        public string GetProcessedPolicies()
        {
            StringBuilder sb = new StringBuilder();

            if (_Reg == null)
            {
                sb.AppendLine("Due to unable to load the MDM registry file, we will skip the ProcessedPolicies list.");
            }

            var nodes = new List<NodeCache>();

            for (int i = 0; i < _Reg.Lines.Length; i++)
            {
                if (!_Reg.Lines[i].StartsWith
                    ("[HKEY_LOCAL_MACHINE\\software\\microsoft\\provisioning\\NodeCache\\CSP\\Device\\MS DM Server\\Nodes\\"))
                    continue;

                var node = new NodeCache();
                var path = _Reg.Lines[i];
                path = path.Substring(1, path.Length - 2);
                node.id = int.Parse(
                    path.Substring("HKEY_LOCAL_MACHINE\\software\\microsoft\\provisioning\\NodeCache\\CSP\\Device\\MS DM Server\\Nodes\\".Length,
                    path.Length - "HKEY_LOCAL_MACHINE\\software\\microsoft\\provisioning\\NodeCache\\CSP\\Device\\MS DM Server\\Nodes\\".Length));

                try
                {
                    node.NodeUri = _Reg.GetValue(path, "NodeUri");
                }
                catch
                {
                    // Do nothing
                }

                try
                {
                    node.ExpectedValue = _Reg.GetValue(path, "ExpectedValue");
                }
                catch
                {
                    // Do nothing
                }

                nodes.Add(node);
            }

            sb.AppendLine("POLICIES PROCESSED");

            for (int i = 0; i < nodes.Count; i++)
            {
                sb.AppendLine($"CSP [{nodes[i].id}]:\nURI: {nodes[i].NodeUri}\nExpected Value: {nodes[i].ExpectedValue}");
                if (i < nodes.Count - 1)
                    sb.AppendLine();
            }

            _NodeCaches = nodes;

            return sb.ToString();
        }

        public string GetHtmlFormattedProcessedPolicies()
        {
            if (_NodeCaches == null)
                GetProcessedPolicies();

            if (_NodeCaches == null)
                return string.Empty;

            /*
            var sb = new StringBuilder();
            
            sb.AppendLine("<h1>Processed Policies</h1");
            sb.AppendLine("<p>*Based on <i>MdmDiagReport_RegistryDump.reg</i> .</p>");

            sb.AppendLine("<style>");

            // grid 布局
            sb.AppendLine("table { display: grid; grid-template-columns: repeat(3, 1fr); gap: 10px; }");
            sb.AppendLine("th, td { padding: 10px; border: 0.5px solid #ddd; text-align: left; }");
            sb.AppendLine("th { background-color: #04AA6D; color: white;}");
            sb.AppendLine("tr:hover {background-color: coral;}");

            // 添加媒体查询
            sb.AppendLine("@media (max-width: 768px) { table { grid-template-columns: repeat(2, 1fr); } }");
            sb.AppendLine("@media (max-width: 480px) { table { grid-template-columns: repeat(1, 1fr); } }");

            sb.AppendLine("</style>");

            for (int i = 0; i < _NodeCaches.Count; i++)
            {
                if (i == 0)
                {
                    sb.AppendLine("<table>");
                    sb.AppendLine("<tr><th>ID</th><th>Node Uri</th><th>Expected Value</th></tr>");
                }
                sb.AppendLine($"<tr><td>{_NodeCaches[i].id}</td><td>{_NodeCaches[i].NodeUri}</td><td>{_NodeCaches[i].ExpectedValue}</td></tr>");
                if (i < _NodeCaches.Count - 1)
                    sb.AppendLine();
            }
            sb.AppendLine("</table>");
            */

            var value = new List<string>();
            for (int i = 0; i < _NodeCaches.Count; i++)
            {
                value.Add(_NodeCaches[i].id.ToString());
                value.Add(_NodeCaches[i].NodeUri);
                value.Add(_NodeCaches[i].ExpectedValue);
            }

            return HtmlReportUtil.GenerateHtmlReport("Processed Policies", "*Based on MdmDiagReport_RegistryDump.reg",
                new string[] { "ID", "NodeUri", "ExpectedValue" }, value.ToArray(), 1);
        }

        public string GetProcessedApps()
        {
            var sb = new StringBuilder();

            #region MSI
            // Get the MSI ID from ./Device/Vendor/MSFT/EnterpriseDesktopAppManagement/MSI/
            // Current user should be S-0-0-00-0000000000-0000000000-000000000-000
            // eg: HKEY_LOCAL_MACHINE\software\microsoft\enterprisedesktopappmanagement\S-0-0-00-0000000000-0000000000-000000000-000\MSI\{fd14a52a-dced-4e7c-a682-fd1f442fe059}

            if (_NodeCaches == null)
            {
                GetProcessedPolicies();
            }

            if (_NodeCaches == null) return string.Empty;

            var possibleMsiList = _NodeCaches.FindAll(search => search.NodeUri != null
                && search.NodeUri.StartsWith("./Device/Vendor/MSFT/EnterpriseDesktopAppManagement/MSI/"));

            var msiList = new List<string>();
            for (int i = 0; i < possibleMsiList.Count; i++)
            {
                var splitString = possibleMsiList[i].NodeUri.Split('/');
                if (splitString.Length > 7) continue;
                // remove %7B and %7D
                if (possibleMsiList[i].NodeUri.Contains("/MSI/") && splitString[6].Contains("%7B") && splitString[6].Contains("%7D"))
                {
                    msiList.Add(splitString[6].Replace("%7B", string.Empty).Replace("%7D", string.Empty));
                }
            }

            var user = "S-0-0-00-0000000000-0000000000-000000000-000";
            var path = "HKEY_LOCAL_MACHINE\\software\\microsoft\\enterprisedesktopappmanagement\\S-0-0-00-0000000000-0000000000-000000000-000\\MSI\\";
            for (int i = 0; i < msiList.Count; i++)
            {
                var msiPath = path + "{" + msiList[i] + "}";

                if (!_Reg.PathExist(msiPath)) continue;

                var downloadUrl = _Reg.GetValue(msiPath, "CurrentDownloadUrl");
                string msiKey;
                if (downloadUrl.Contains("IntuneWindowsAgent.msi"))
                {
                    msiKey = $"Intune Management Extensions ({msiList[i]})";
                }
                else
                {
                    msiKey = msiList[i];
                }

                string statusText = string.Empty;
                try
                {
                    var status = Convert.ToInt32(_Reg.GetValue(msiPath, "Status").Split(new char[] { ':' }, 2)[1].Trim(), 10);
                    switch (status)
                    {
                        case 0:
                            statusText = "SUCCESS";
                            break;

                        case 60:
                            statusText = "FAILED";
                            break;

                        case 70:
                            statusText = "INSTALLED";
                            break;

                        default:
                            statusText = "PROCESSING";
                            break;
                    }
                }
                catch
                {
                    statusText = "UNKNOWN";
                }

                sb.AppendLine($"MSI: {msiKey}\nStatus: {statusText} ({_Reg.GetValue(msiPath, "DownloadInstall")})");
            }

            #endregion

            possibleMsiList = _NodeCaches.FindAll(search => search.NodeUri != null
                && search.NodeUri.StartsWith("./Device/Vendor/MSFT/Office/Installation"));

            return sb.ToString();
        }

        public string GetLegacyObservedTimeline()
        {
            var sb = new StringBuilder();
            sb.AppendLine("OBSERVED TIMELINE:");
            sb.AppendLine();



            return sb.ToString();
        }

        public AutopilotSettings GetAutopilotSettingsFromRegistry()
        {
            var settings = new AutopilotSettings();
            var regPath = "HKEY_LOCAL_MACHINE\\software\\microsoft\\provisioning\\AutopilotSettings";
            settings.AgilityProductName = _Reg.GetValue(regPath, "AgilityProductName");
            settings.AccountSetup = new()
            {
                Status = _Reg.GetValue(regPath, "AccountSetupCategory.Status")
            };

            settings.DevicePreparation = new()
            {
                Status = _Reg.GetValue(regPath, "DevicePreparationCategory.Status")
            };

            settings.DeviceSetup = new()
            {
                Status = _Reg.GetValue(regPath, "DeviceSetupCategory.Status")
            };

            return settings;
        }
    }
}
