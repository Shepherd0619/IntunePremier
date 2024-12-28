namespace AutopilotHelper.Utilities
{
    public class AppSettingsUtil
    {
        public List<string> RecentDiagFiles { get; set; }

        public void Initialize()
        {
            RecentDiagFiles = new List<string>();
        }
    }
}
