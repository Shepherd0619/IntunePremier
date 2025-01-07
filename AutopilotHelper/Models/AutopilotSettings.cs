using Newtonsoft.Json;

namespace AutopilotHelper.Models
{
    public class AutopilotSettings
    {
        public string AgilityProductName { get; set; }
        public int AllowedTimeDriftDeltaMinutes { get; set; }
        public string AutopilotDiagnosticsCurrentVersion { get; set; }
        public bool AutopilotDiagnosticsOutputMocked { get; set; }
        public string ConciergeMsaTicketUri { get; set; }
        public string ConciergeUri { get; set; }
        public string DdsZtdMsaTicketUri { get; set; }
        public string DdsZtdUri { get; set; }
        public int TpmAikTaskMaxTimeoutMilliseconds { get; set; }
        public int TpmNgcWaitDelayMilliseconds { get; set; }
        public bool UseRefactoredEsp { get; set; }
        public bool DisableAutopilotAgilityProductVersionTelemetry { get; set; }
        public string GlobalRunProvisioning { get; set; }
        public string GlobalRestoreMdmTasks { get; set; }
        public string GlobalMdmEnrollmentStatus { get; set; }
        public DevicePreparationCategory DevicePreparation { get; set; }
        public AccountSetupCategory AccountSetup { get; set; }
        public bool ShowContinueAnywayButton { get; set; }
        public int DppHeartbeatMaxFailures { get; set; }
        public int DppHeartbeatMilliseconds { get; set; }

        public class DevicePreparationCategory
        {
            public string Status { get; set; }
        }

        public class AccountSetupCategory
        {
            public string Status { get; set; }
        }
    }

    public class DevicePreparationCategory
    {
        [JsonProperty("categoryState")]
        public string CategoryState { get; set; }

        [JsonProperty("DevicePreparation.TpmAttestationSubcategory")]
        public Subcategory TpmAttestationSubcategory { get; set; }

        [JsonProperty("DevicePreparation.AadjSubcategory")]
        public Subcategory AadjSubcategory { get; set; }

        [JsonProperty("DevicePreparation.MdmEnrollmentSubcategory")]
        public Subcategory MdmEnrollmentSubcategory { get; set; }

        [JsonProperty("DevicePreparation.EspProviderInstallationSubcategory")]
        public Subcategory EspProviderInstallationSubcategory { get; set; }

        [JsonProperty("DevicePreparation.InitiateSyncSessions")]
        public Subcategory InitiateSyncSessions { get; set; }

        [JsonProperty("DevicePreparation.SetContinueAnywayButtonVisibility")]
        public Subcategory SetContinueAnywayButtonVisibility { get; set; }

        [JsonProperty("categoryStatusText")]
        public string CategoryStatusText { get; set; }

        public override string ToString()
        {
            return $"CategoryState: {CategoryState}\n" +
                   $"TpmAttestationSubcategory: State={TpmAttestationSubcategory?.SubcategoryState}, StatusText={TpmAttestationSubcategory?.SubcategoryStatusText}\n" +
                   $"AadjSubcategory: State={AadjSubcategory?.SubcategoryState}, StatusText={AadjSubcategory?.SubcategoryStatusText}\n" +
                   $"MdmEnrollmentSubcategory: State={MdmEnrollmentSubcategory?.SubcategoryState}, StatusText={MdmEnrollmentSubcategory?.SubcategoryStatusText}\n" +
                   $"EspProviderInstallationSubcategory: State={EspProviderInstallationSubcategory?.SubcategoryState}, StatusText={EspProviderInstallationSubcategory?.SubcategoryStatusText}\n" +
                   $"InitiateSyncSessions: State={InitiateSyncSessions?.SubcategoryState}, StatusText={InitiateSyncSessions?.SubcategoryStatusText}\n" +
                   $"SetContinueAnywayButtonVisibility: State={SetContinueAnywayButtonVisibility?.SubcategoryState}, StatusText={SetContinueAnywayButtonVisibility?.SubcategoryStatusText}\n" +
                   $"CategoryStatusText: {CategoryStatusText}";
        }
    }

    public class AccountSetupCategory
    {
        [JsonProperty("categoryState")]
        public string CategoryState { get; set; }

        [JsonProperty("AccountSetup.WaitingForAadRegistrationSubcategory")]
        public Subcategory WaitingForAadRegistrationSubcategory { get; set; }

        [JsonProperty("AccountSetup.PrepareMultifactorAuth")]
        public Subcategory PrepareMultifactorAuth { get; set; }

        [JsonProperty("AccountSetup.SecurityPoliciesSubcategory")]
        public Subcategory SecurityPoliciesSubcategory { get; set; }

        [JsonProperty("AccountSetup.CertificatesSubcategory")]
        public Subcategory CertificatesSubcategory { get; set; }

        [JsonProperty("AccountSetup.NetworkConnectionsSubcategory")]
        public Subcategory NetworkConnectionsSubcategory { get; set; }

        [JsonProperty("AccountSetup.AppsSubcategory")]
        public Subcategory AppsSubcategory { get; set; }

        [JsonProperty("AccountSetup.SendResultsToMdmServer")]
        public Subcategory SendResultsToMdmServer { get; set; }

        [JsonProperty("categoryStatusText")]
        public string CategoryStatusText { get; set; }

        public override string ToString()
        {
            return $"CategoryState: {CategoryState}\n" +
                   $"WaitingForAadRegistrationSubcategory: State={WaitingForAadRegistrationSubcategory?.SubcategoryState}, StatusText={WaitingForAadRegistrationSubcategory?.SubcategoryStatusText}\n" +
                   $"PrepareMultifactorAuth: State={PrepareMultifactorAuth?.SubcategoryState}, StatusText={PrepareMultifactorAuth?.SubcategoryStatusText}\n" +
                   $"SecurityPoliciesSubcategory: State={SecurityPoliciesSubcategory?.SubcategoryState}, StatusText={SecurityPoliciesSubcategory?.SubcategoryStatusText}\n" +
                   $"CertificatesSubcategory: State={CertificatesSubcategory?.SubcategoryState}, StatusText={CertificatesSubcategory?.SubcategoryStatusText}\n" +
                   $"NetworkConnectionsSubcategory: State={NetworkConnectionsSubcategory?.SubcategoryState}, StatusText={NetworkConnectionsSubcategory?.SubcategoryStatusText}\n" +
                   $"AppsSubcategory: State={AppsSubcategory?.SubcategoryState}, StatusText={AppsSubcategory?.SubcategoryStatusText}\n" +
                   $"SendResultsToMdmServer: State={SendResultsToMdmServer?.SubcategoryState}, StatusText={SendResultsToMdmServer?.SubcategoryStatusText}\n" +
                   $"CategoryStatusText: {CategoryStatusText}";
        }
    }

    public class Subcategory
    {
        [JsonProperty("subcategoryState")]
        public string SubcategoryState { get; set; }

        [JsonProperty("subcategoryStatusText")]
        public string SubcategoryStatusText { get; set; }

        public override string ToString()
        {
            return $"Subcategory State: {SubcategoryState}, Status Text: {SubcategoryStatusText}";
        }
    }
}
