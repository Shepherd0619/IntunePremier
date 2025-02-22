using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapHelper.Utilities
{
    public static class StringUtil
    {
        public static List<string> SplitStringIntoChunks(string input, int maxLength = 4096)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));
            if (maxLength <= 0) throw new ArgumentException("MaxLength must be greater than zero.", nameof(maxLength));

            input = RemoveEmptyLines(input);
            var chunks = new List<string>();
            for (int i = 0; i < input.Length; i += maxLength)
            {
                int end = Math.Min(i + maxLength, input.Length);
                string chunk = input.Substring(i, end - i);
                chunks.Add(chunk);
            }

            return chunks;
        }

        public static string RemoveEmptyLines(string input)
        {
            // Split the input into lines
            var lines = input.Split(new[] { "\r", "\n", "\r\n" }, StringSplitOptions.None);

            // Filter out empty and whitespace-only lines
            var nonEmptyLines = lines.Where(line => !string.IsNullOrWhiteSpace(line));

            // Join the remaining lines back into a single string
            return string.Join(Environment.NewLine, nonEmptyLines);
        }
    }
}
