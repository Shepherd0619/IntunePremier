using System.Diagnostics;
using Newtonsoft.Json;
using WUApiLib;
using AutopilotHelper.DiagnosticsCollection.Models;
using System.IO.Compression;

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
            Console.WriteLine($"Administrator privilege: {UacHelper.IsProcessElevated}\n");
            if(!UacHelper.IsProcessElevated)
            {
                Console.WriteLine("WARNING: The program probably does not have admin privilege. Some log collection steps may throw exception.");
            }
            Console.WriteLine();
            Console.WriteLine("PRESS ANY KEY TO CONTINUE...");
            Console.ReadKey();

            Console.WriteLine();

            if (Directory.Exists(TmpFolder))
            {
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
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(TmpFolder);
                    Console.WriteLine($"Temporary folder created! {TmpFolder}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"ERROR: Unable to create temporary folder {TmpFolder}. Abort!\n\n{ex}");
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
                CollectRegistry(@"HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\SharedAccess\Parameters\FirewallPolicy", "FirewallSettings.reg");
            }
            catch(Exception ex)
            {
                Console.WriteLine($"Failed to collect Firewall settings!\n\n{ex.ToString()}");
            }

            Console.WriteLine();
            Console.WriteLine("**********************************");
            Console.WriteLine("STEP 3 - Collect Defender Settings");
            Console.WriteLine("**********************************");
            Console.WriteLine();

            try
            {
                CollectRegistry(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender", "Defender_Policy.reg");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Failed to collect Defender Policy registry!\n\n{e}");
            }

            try
            {
                CollectRegistry(@"HKEY_LOCAL_MACHINE\SOFTWARE\Policies\Microsoft\Windows Defender\Policy Manager", "Defender_MDM.reg");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Failed to collect Defender MDM policy registry!\n\n{e}");
            }

            try
            {
                CollectRegistry(@"HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows Defender", "Defender_Local.reg");
            }
            catch(Exception e)
            {
                Console.WriteLine($"Failed to collect Defender local settings registry!\n\n{e}");
            }

            Console.WriteLine();
            Console.WriteLine("**********************************");
            Console.WriteLine("STEP 4 - Collect Update Settings");
            Console.WriteLine("**********************************");
            Console.WriteLine();

            try
            {
                CollectRegistry(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\Windows\WindowsUpdate", "WindowsUpdate.reg");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to collect Update registry!\n\n{e}");
            }

            try
            {
                CollectRegistry(@"HKEY_LOCAL_MACHINE\Software\Policies\Microsoft\WufbDS", "WufbEnrollment.reg");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to collect Update registry!\n\n{e}");
            }

            Console.WriteLine("Collecting Windows Update services information......");
            Console.WriteLine();
            WUApiLib.IUpdateServiceManager updateServiceManager = new UpdateServiceManager();
            List<UpdateService> serviceJsonList = new List<UpdateService>();

            foreach (IUpdateService2 service in updateServiceManager.Services)
            {
                Console.WriteLine("Service Name: " + service.Name);
                Console.WriteLine("AutoUpdate registered: " + service.IsRegisteredWithAU);
                Console.WriteLine("URL:" + service.ServiceUrl);
                Console.WriteLine("Is default AutoUpdate service: " + service.IsDefaultAUService);
                serviceJsonList.Add(new UpdateService
                {
                    Name = service.Name,
                    IsRegisteredWithAU = service.IsRegisteredWithAU,
                    ServiceUrl = service.ServiceUrl,
                    IsDefaultAUService = service.IsDefaultAUService
                });
                Console.WriteLine();
            }

            string jsonOutput = JsonConvert.SerializeObject(serviceJsonList, Newtonsoft.Json.Formatting.Indented);
            try
            {
                File.WriteAllText($@"{TmpFolder}\UpdateService.json", jsonOutput);
            }
            catch(Exception e)
            {
                Console.WriteLine("Failed to write UpdateService.json!\n\n" + e.ToString());
            }

            Console.WriteLine();
            Console.WriteLine("**********************************");
            Console.WriteLine("STEP 5 - Collect IntuneManagementExtension Registry");
            Console.WriteLine("**********************************");
            Console.WriteLine();

            try
            {
                CollectRegistry(@"HKEY_LOCAL_MACHINE\SOFTWARE\MicrosoftIntuneManagementExtension", "IntuneManagementExtension.reg");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Failed to collect IntuneManagementExtension registry!\n\n{e}");
            }

            Console.WriteLine();
            try
            {
                CreateAndFillZipFile(Path.Combine(TmpFolder, $"IntunePremierDiagnostics.zip"));
            }
            catch (Exception e)
            {
                Console.WriteLine("Failed to compress the log files!\n\n" + e.ToString());
            }

            CleanUpRuntimeCache();

            Console.WriteLine();
            Console.WriteLine("All operation completed successfully.\nPress any key to open output folder.");
            Console.ReadKey();
            OpenTmpFolder();
        }

        static int CollectMDMDiag()
        {
            var path = @$"{TmpFolder}\MDMDiagReport.zip";

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

        static int CollectRegistry(string regPath, string fileName)
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "reg";
            startInfo.Arguments = $"export \"{regPath}\" \"{TmpFolder}\\{fileName}\" /y";
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

            Console.WriteLine($"reg.exe has started for collecting {regPath}. Waiting for exit......");
            Console.WriteLine();

            process.WaitForExit();

            if (process.ExitCode != 0)
            {
                Console.WriteLine($"WARNING: Registry collection for {regPath} may encounter some errors! You may need to verify:\n1. Whether registry path exists.\n2. Whether you run the tool with admin privilege.\n");
            }

            return process.ExitCode;
        }

        private static void Process_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Data))
            {
                Console.WriteLine(e.Data);
            }
        }

        private static void OpenTmpFolder()
        {
            var startInfo = new ProcessStartInfo();
            startInfo.FileName = "explorer.exe";
            startInfo.Arguments = TmpFolder;
            startInfo.UseShellExecute = true;

            var process = Process.Start(startInfo);
        }

        private static void CleanUpRuntimeCache()
        {
            var files = Directory.GetFiles(TmpFolder);
            foreach(var file in files)
            {
                if(file.Equals(Path.Combine(TmpFolder, "IntunePremierDiagnostics.zip"), StringComparison.OrdinalIgnoreCase)) continue;

                try
                {
                    File.Delete(file);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to delete file {file}!\n\n{ex}");
                }
            }
        }

        #region ZIP
        static void CreateAndFillZipFile(string outputPath)
        {
            Console.WriteLine($"Compressing log into zip {outputPath}......");

            ZipArchive zip = new ZipArchive(new FileStream(outputPath, FileMode.Create), ZipArchiveMode.Update);

            // 添加文件到ZIP并添加自定义属性
            
            var files = Directory.GetFiles(TmpFolder);

            foreach(var file in files)
            {
                if (Path.GetFileName(file) == Path.GetFileName(outputPath)) continue;
                Console.WriteLine("Adding file: " + file);
                var entry = zip.CreateEntryFromFile(file, Path.GetFileName(file));
            }

            zip.Dispose();
        }
        #endregion
    }
}
