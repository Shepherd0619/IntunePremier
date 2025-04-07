namespace AutopilotHelper.Utilities
{
    internal static class PathUtil
    {
        public static string FixSeparator(string path)
        {
            string pathWithBackslashes = Path.DirectorySeparatorChar == '/' ?
                        path.Replace('/', '\\') :
                        path.Replace('\\', '/');

            return pathWithBackslashes;
        }
    }
}
