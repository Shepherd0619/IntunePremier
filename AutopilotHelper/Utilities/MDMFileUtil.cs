using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace AutopilotHelper.Utilities
{
    public class MDMFileUtil
    {
        /// <summary>
        /// This path represents the zip file extract destination.
        /// </summary>
        public string TmpWorkplacePath = Path.Combine(Path.GetTempPath(), $"IntunePremier/TmpWorkplace/{Guid.NewGuid().ToString()}");

        public MDMFileUtil(Stream fileStream)
        {
            if (!Directory.Exists(TmpWorkplacePath))
            {
                Directory.CreateDirectory(TmpWorkplacePath);
            }

            using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Read, true))
            {
                foreach (var entry in archive.Entries)
                {
                    // Get the full path of the entry
                    var fullPath = Path.Combine(TmpWorkplacePath, entry.FullName);

                    // Create the directory if it doesn't exist
                    var dirPath = Path.GetDirectoryName(fullPath);
                    if (!string.IsNullOrEmpty(dirPath) && !Directory.Exists(dirPath))
                    {
                        Directory.CreateDirectory(dirPath);
                    }

                    // Extract the entry to the target path
                    entry.ExtractToFile(fullPath);
                }
            }

            fileStream.Close();
            fileStream.Dispose();
        }

        public MDMFileUtil(string filePath)
        {
            if (!Directory.Exists(TmpWorkplacePath))
            {
                Directory.CreateDirectory(TmpWorkplacePath);
            }

            //var cabinet = new CabInfo(cabFilePath);

            //var files = cabinet.GetFiles();

            //for (int i = 0; i < files.Count; i++)
            //{
            //    var stream = files[i].OpenRead();

            //    using(var reader = new StreamReader(stream))
            //    {
            //        using (var writer = File.CreateText(Path.Combine(TmpWorkplacePath, files[i].Name)))
            //        {
            //            writer.Write(reader.ReadToEnd());
            //        }
            //    }
            //}

            var fileNameSplit = Path.GetFileName(filePath).Split('.');
            if (fileNameSplit[fileNameSplit.Length - 1].Equals("cab", StringComparison.OrdinalIgnoreCase))
            {

                var process = new Process();
                process.StartInfo = new();
                process.StartInfo.FileName = "expand.exe";
                process.StartInfo.Arguments = $"\"{filePath}\" -F:* \"{Path.Combine(TmpWorkplacePath)}\"";
                process.Start();
                process.WaitForExit();

                if (process.ExitCode != 0)
                {
                    throw new Exception($"expand exit code is {process.ExitCode}");
                }
            }
            else
            {
                var fileStream = File.OpenRead(filePath);
                using (var archive = new ZipArchive(fileStream, ZipArchiveMode.Read, true))
                {
                    foreach (var entry in archive.Entries)
                    {
                        // Get the full path of the entry
                        var fullPath = Path.Combine(TmpWorkplacePath, entry.FullName);

                        // Create the directory if it doesn't exist
                        var dirPath = Path.GetDirectoryName(fullPath);
                        if (!string.IsNullOrEmpty(dirPath) && !Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }

                        // Extract the entry to the target path
                        entry.ExtractToFile(fullPath);
                    }
                }

                fileStream.Close();
                fileStream.Dispose();
            }
        }
    }
}
