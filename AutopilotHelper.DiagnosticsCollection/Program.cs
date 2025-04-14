using System.Diagnostics;
using System.Xml;
using Microsoft.Win32;
using Newtonsoft.Json;

namespace AutopilotHelper.DiagnosticsCollection
{
    internal class Program
    {
        public const string TmpFolder = @"C:\Users\Public\Documents\IntunePremier\DiagnosticsCollection";
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello, World!");
            Console.WriteLine("\r\n  ___       _                      ____                     _           \r\n |_ _|_ __ | |_ _   _ _ __   ___  |  _ \\ _ __ ___ _ __ ___ (_) ___ _ __ \r\n  | || '_ \\| __| | | | '_ \\ / _ \\ | |_) | '__/ _ \\ '_ ` _ \\| |/ _ \\ '__|\r\n  | || | | | |_| |_| | | | |  __/ |  __/| | |  __/ | | | | | |  __/ |   \r\n |___|_| |_|\\__|\\__,_|_| |_|\\___| |_|   |_|  \\___|_| |_| |_|_|\\___|_|   \r\n                                                                        \r\n");
            Console.WriteLine("AutopilotHelper Custom Diagnostics Collection Tool");
            Console.WriteLine("==================================================");

            Console.WriteLine("This program will collect diagnostics information from the system, not only the traditional MDM diagnostics,but also some registry keys etc.");
            Console.WriteLine();
            Console.WriteLine("Before continue, please make sure you have synced the device for the accuracy.");
            Console.WriteLine();
            Console.WriteLine($"** All logs will be collected under {TmpFolder}. **");
            Console.WriteLine();
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadKey();

            Console.WriteLine();

            if (Directory.GetFiles(TmpFolder).Length > 0)
            {
                Console.WriteLine($"WARNING: There are files exist in the cache folder {TmpFolder}. Please make sure you have backup of them before continue as we will clean up the folder.");
                Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
                Console.ReadKey();
                try
                {
                    Directory.Delete(TmpFolder, true);
                    Directory.CreateDirectory(TmpFolder);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to clean up the cache folder {TmpFolder}. Abort!\n\n{ex}");
                    Environment.Exit(-1);
                }
            }

            Console.WriteLine();

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
            }
            else
            {
                Console.WriteLine();
                Console.WriteLine($"MDM diagnostics collection failed with exit code {exitCodeForMdm}.");
                Environment.Exit(exitCodeForMdm);
            }

            Console.WriteLine();
            Console.WriteLine("**********************************");
            Console.WriteLine("STEP 2 - Collect Firewall Settings");
            Console.WriteLine("**********************************");
            Console.WriteLine();

            try
            {
                CollectRegistry(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SharedAccess\Parameters\FirewallPolicy");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to collect Firewall settings!\n\n{ex.ToString()}");
            }
        }

        static int CollectMDMDiag()
        {
            var path = @$"{TmpFolder}\\MDMDiagReport.zip";

            if (File.Exists(path))
            {
                Console.WriteLine("WARNING: There is a MDMDiagReport exist in the cache folder. Will delete it in case mdmdiagnosticstool.exe throw error.");

                try
                {
                    File.Delete(path);
                    Console.WriteLine("Delete successful!");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete the MDMDiagReport cache. Abort!\n\n{ex}");
                    return -1;
                }
            }

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "mdmdiagnosticstool.exe";
            startInfo.Arguments = $"-area \"DeviceEnrollment;DeviceProvisioning;Autopilot\" -zip \"{path}\"";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
            startInfo.StandardErrorEncoding = System.Text.Encoding.UTF8;
            startInfo.UseShellExecute = false;

            Process process = new();
            process.OutputDataReceived += Process_OutputDataReceived;
            process.ErrorDataReceived += Process_OutputDataReceived;
            process.StartInfo = startInfo;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            Console.WriteLine("mdmdiagnosticstool.exe has started. Waiting for exit......");
            Console.WriteLine();

            process.WaitForExit();

            return process.ExitCode;
        }

        static int CollectRegistry(string regPath)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "reg";
            startInfo.Arguments = $"export \"{regPath}\" \"{TmpFolder}\\FirewallSettings.reg\" /y";
            startInfo.RedirectStandardOutput = true;
            startInfo.RedirectStandardError = true;
            startInfo.StandardOutputEncoding = System.Text.Encoding.UTF8;
            startInfo.StandardErrorEncoding = System.Text.Encoding.UTF8;
            startInfo.UseShellExecute = false;
            startInfo.CreateNoWindow = true;

            var process = new Process();
            process.StartInfo = startInfo;
            process.OutputDataReceived += Process_OutputDataReceived;
            process.ErrorDataReceived += Process_OutputDataReceived;
            process.Start();
            process.BeginOutputReadLine();
            process.BeginErrorReadLine();

            Console.WriteLine("reg.exe has started. Waiting for exit......");
            Console.WriteLine();

            process.WaitForExit();

            return process.ExitCode;
        }

        private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine(e.Data);
            }
        }
    }
}
