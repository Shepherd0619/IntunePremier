using AutopilotHelper.Utilities;
using System.Diagnostics;

namespace AutopilotHelper
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();
            if (string.IsNullOrEmpty(AdkUtil.GetAdkPath()))
            {
                var result = MessageBox.Show("It appears the Windows ADK is not installed.\n" +
                    "If you wish to decode hardware hash, ADK is required.\n\n" +
                    "Head to Microsoft Learn for download?", "Windows ADK is missing", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                if (result == DialogResult.OK)
                {
                    ProcessStartInfo startInfo = new ProcessStartInfo();
                    startInfo.FileName = "https://learn.microsoft.com/en-us/windows-hardware/get-started/adk-install"; // Õ¯“≥µÿ÷∑
                    startInfo.UseShellExecute = true;

                    Process process = Process.Start(startInfo);
                }
            }

            Application.Run(new StartUpForm());
        }
    }
}