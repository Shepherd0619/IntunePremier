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

            regContent = File.ReadAllText(Path.Combine(MDMDiag.TmpWorkspacePath, "MdmDiagReport_RegistryDump.reg"));
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
                        if (line.Substring(1, line.Length - 2) == path)
                        {
                            pathLocated = true;
                        }
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

                string part = parts[1];
                if (part.StartsWith("\""))
                {
                    part = part.Substring(1); // 去掉开始的第一个引号
                }
                if (part.EndsWith("\""))
                {
                    part = part.Substring(0, part.Length - 1); // 去掉结尾的最后一个引号
                }
                parts[1] = part;
                return parts[1];
            }

            throw new KeyNotFoundException();
        }

        public bool PathExist(string path)
        {
            bool pathLocated = false;
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
                        if (line.Substring(1, line.Length - 2) == path)
                        {
                            pathLocated = true;
                        }
                        continue;
                    }
                }
            }

            return pathLocated;
        }

        public List<string> GetAllPath()
        {
            List<string> paths = new();
            foreach (string line in lines)
            {
                if (line.StartsWith("["))
                {
                    paths.Add(line.Substring(1, line.Length - 2));
                }
            }
            return paths;
        }

        public struct Record
        {
            public string Key;
            public string Type;
            public string Value;
        }

        public Dictionary<string, string> GetAllKeys(string path)
        {
            var keyValues = new Dictionary<string, string>();

            bool pathLocated = false;

            foreach (string line in lines)
            {
                if (!pathLocated)
                {
                    if (!line.StartsWith($"[{path}", StringComparison.OrdinalIgnoreCase))
                    {
                        continue;
                    }
                    else
                    {
                        if (line.Substring(1, line.Length - 2) == path)
                        {
                            pathLocated = true;
                        }
                        continue;
                    }
                }

                if (line.StartsWith("["))
                {
                    break; // Exit the loop when a new section is encountered
                }

                string[] parts = line.Split(new char[] { '=' }, 2);
                string _key = parts[0].Trim();
                if (!string.IsNullOrWhiteSpace(_key))
                {
                    string value = parts.Length > 1 ? parts[1].Trim() : null;
                    keyValues[_key] = value;
                }
            }

            return keyValues;
        }
    }
}
