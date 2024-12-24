namespace AutopilotHelper.Utilities
{
    public static class AdkUtil
    {
        public static string GetAdkPath()
        {
            try
            {
                using (Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine.OpenSubKey(@"SOFTWARE\Microsoft\Windows Kits\Installed Roots"))
                {
                    return key.GetValue("KitsRoot10").ToString();
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static string GetOA3ToolPath(string adkPath)
        {
            if (!string.IsNullOrEmpty(adkPath))
            {
                string oa3ToolPath = Path.Combine(adkPath,
                    $"Assessment and Deployment Kit\\Deployment Tools\\{Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE").ToLower()}\\Licensing\\OA30\\oa3tool.exe");

                return oa3ToolPath;
            }

            return null;
        }

    }
}
