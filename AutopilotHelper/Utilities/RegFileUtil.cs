using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutopilotHelper.Utilities
{
    public class RegFileUtil
    {
        public MDMFileUtil MDMDiag => _MDMDiag;
        private MDMFileUtil _MDMDiag;

        public string RegContent => regContent;
        private string regContent;

        public string[] Lines => lines;
        private string[] lines;

        public RegFileUtil(MDMFileUtil file)
        {
            _MDMDiag = file;

            regContent = File.ReadAllText(Path.Combine(MDMDiag.TmpWorkplacePath, "MdmDiagReport_RegistryDump.reg"));
            lines = regContent.Split(new char[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        }

        /// <summary>
        /// Read registry key value pair.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        /// <exception cref="KeyNotFoundException"></exception>
        public string GetValue(string path, string key)
        {
            bool pathLocated = false;
            key = "\"" + key + "\"";

            foreach (string line in lines)
            {
                if (!pathLocated)
                {
                    if (!line.StartsWith($"[{path}", StringComparison.CurrentCultureIgnoreCase))
                    {
                        continue;
                    }
                    else
                    {
                        pathLocated = true;
                        continue;
                    }
                }

                if (line.StartsWith("["))
                {
                    break;
                }

                string[] parts = line.Split(new char[] { '=' }, 2);
                string _key = parts[0].Trim();
                if (_key != key)
                {
                    continue;
                }

                return parts[1].Replace("\"", string.Empty);
            }

            throw new KeyNotFoundException();
        }
    }
}
