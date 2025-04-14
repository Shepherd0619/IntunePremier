using System.Diagnostics;
using Microsoft.Win32;

namespace AutopilotHelper.DiagnosticsCollection
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            Console.WriteLine("AutopilotHelper Custom Diagnostics Collection Tool");
            Console.WriteLine("--------------------------------------------------");

            Console.WriteLine("This program will collect diagnostics information from the system, not only the traditional MDM diagnostics,but also some registry keys etc.");
            Console.WriteLine();
            Console.WriteLine("Before continue, please make sure you have synced the device for the accuracy.");
            Console.WriteLine();
            Console.WriteLine("Kindly note that the diagnostics can be only opened by AutopilotHelper.");
            Console.WriteLine();
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadKey();
            Console.WriteLine("You can cancel the process by pressing Ctrl+C at any time.");
            Console.WriteLine();
            Console.WriteLine("********************************");
            Console.WriteLine("STEP 1 - Collect MDM diagnostics");
            Console.WriteLine("********************************");
            Console.WriteLine();

            var exitCodeForMdm = CollectMDMDiag();
            if (exitCodeForMdm == 0)
            {
                Console.WriteLine();
                Console.WriteLine("MDM diagnostics collection completed successfully.");
                Console.WriteLine("You can find the report in c:\\users\\public\\documents\\MDMDiagReport.zip");
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"MDM diagnostics collection failed with exit code {exitCodeForMdm}.");
                Environment.Exit(exitCodeForMdm);
            }

            Console.WriteLine();
            Console.WriteLine("*******************************");
            Console.WriteLine("STEP 2 - Collect Firewall Rules");
            Console.WriteLine("*******************************");
            Console.WriteLine();

        }

        static int CollectMDMDiag()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "mdmdiagnosticstool.exe";
            startInfo.Arguments = "-area \"DeviceEnrollment;DeviceProvisioning;Autopilot\" -zip \"c:\\users\\public\\documents\\MDMDiagReport.zip\"";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
            startInfo.StandardErrorEncoding = System.Text.Encoding.UTF8;
            startInfo.UseShellExecute = false;

            Process process = new();
            process.OutputDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    Console.WriteLine(e.Data);
                }
            };
            process.ErrorDataReceived += (sender, e) =>
            {
                if (!string.IsNullOrEmpty(e.Data))
                {
                    Console.WriteLine(e.Data);
                }
            };
            process.StartInfo = startInfo;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            Console.WriteLine("mdmdiagnosticstool.exe has started. Waiting for exit......");
            Console.WriteLine();

            process.WaitForExit();

            return process.ExitCode;
        }

        static bool CollectFirewallRule()
        {
            return true;
        }
    }
}
