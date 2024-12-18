using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutopilotHelper.Utilities
{
    public class MDMFileUtil
    {
        /// <summary>
        /// This path represents the zip file extract destination.
        /// </summary>
        public string TmpWorkplacePath = Path.Combine(Path.GetTempPath(), $"IntunePremier/TmpWorkplace/{Guid.NewGuid().ToString()}");

        public MDMFileUtil(Stream fileStream, bool isCab)
        {
            if(!Directory.Exists(TmpWorkplacePath))
            {
                Directory.CreateDirectory(TmpWorkplacePath);
            }

            if (!isCab)
            {
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
            }
            else
            {
                using (var package = Package.Open(fileStream))
                {
                    foreach (var resource in package.GetParts())
                    {
                        var stream = resource.GetStream();

                        // Get the full path of the entry
                        var fullPath = Path.Combine(TmpWorkplacePath, resource.Uri.ToString().Replace('/', '\\'));

                        // Create the directory if it doesn't exist
                        var dirPath = Path.GetDirectoryName(fullPath);
                        if (!string.IsNullOrEmpty(dirPath) && !Directory.Exists(dirPath))
                        {
                            Directory.CreateDirectory(dirPath);
                        }

                        // Extract the entry to the target path
                        using (var destStream = new FileStream(fullPath, FileMode.Create))
                        {
                            stream.CopyTo(destStream);
                        }
                    }
                }
            }
        }
    }
}
