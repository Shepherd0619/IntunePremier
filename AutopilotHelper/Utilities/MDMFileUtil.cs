using System.Diagnostics;
using System.IO.Compression;

namespace AutopilotHelper.Utilities
{
    public class MDMFileUtil
    {
        /// <summary>
        /// This path represents the zip file extract destination.
        /// </summary>
        public string TmpWorkspacePath = Path.Combine(Path.GetTempPath(), $"IntunePremier/TmpWorkspace/{Guid.NewGuid().ToString()}");
        public readonly string FilePath;

        public MDMFileUtil(string filePath)
        {
            if (!Directory.Exists(TmpWorkspacePath))
            {
                Directory.CreateDirectory(TmpWorkspacePath);
            }

            FilePath = filePath;
        }

        public async Task Extract()
        {
            var fileNameSplit = Path.GetFileName(FilePath).Split('.');
            if (fileNameSplit[fileNameSplit.Length - 1].Equals("cab", StringComparison.OrdinalIgnoreCase))
            {

                var process = new Process();
                process.StartInfo = new();
                process.StartInfo.FileName = "expand.exe";
                process.StartInfo.Arguments = $"\"{FilePath}\" -F:* \"{Path.Combine(TmpWorkspacePath)}\"";
                process.Start();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"expand exit code is {process.ExitCode}");
                }
            }
            else
            {
                var fileStream = File.OpenRead(FilePath);
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Read, true))
                {
                    foreach (var entry in archive.Entries)
                    {
                        // Get the full path of the entry
                        var fullPath = Path.Combine(TmpWorkspacePath, entry.FullName);

                        // Create the directory if it doesn't exist
                        var dirPath = Path.GetDirectoryName(fullPath);
                        if (!string.IsNullOrEmpty(dirPath) && !Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }

                        // Extract the entry to the target path
                        await Task.Run(() => entry.ExtractToFile(fullPath));
                    }
                }

                fileStream.Close();
                fileStream.Dispose();
            }
        }
    }
}
