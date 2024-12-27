using System.Text;

namespace AutopilotHelper.Utilities
{
    public static class HtmlReportUtil
    {
        public const string TitlePlaceHolder = "{PAGE_TITLE}";
        public const string DescriptionPlaceHolder = "{DESCRIPTION}";
        public const string TableHeaderPlaceHolder = "<!-- HEADER -->";
        public const string TableContentPlaceHolder = "<!-- CONTENT -->";
        public const string SearchCol = "{ searchCol }";

        public static string GenerateHtmlReport(string title, string description, string[] headers, string[] content, int searchCol)
        {
            // 加载模板文件
            var template = File.ReadAllLines("HtmlTemplates/table_with_search.html");

            // 替换标题和描述
            Replace(template, TitlePlaceHolder, title);
            Replace(template, DescriptionPlaceHolder, description);

            // 添加表头
            AddHeader(template, headers);

            // 添加内容
            AddContent(template, content, headers.Length);

            // 替换搜索列
            Replace(template, SearchCol, searchCol.ToString());

            // 合并 HTML 内容
            var output = string.Join(Environment.NewLine, template);

            return output;
        }

        private static void Replace(string[] lines, string placeholder, string value)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains(placeholder))
                {
                    lines[i] = lines[i].Replace(placeholder, value);
                }
            }
        }

        private static void AddHeader(string[] lines, string[] headers)
        {
            var startIndex = Array.IndexOf(lines, lines.FirstOrDefault(line => line.Contains(TableHeaderPlaceHolder)));
            if (startIndex != -1)
            {
                var sb = new StringBuilder();
                for (int j = 0; j < headers.Length; j++)
                {
                    sb.Append($"<th>{headers[j]}</th>");
                }
                lines[startIndex] = lines[startIndex].Insert(lines[startIndex].IndexOf(TableHeaderPlaceHolder), sb.ToString());
            }
        }

        private static void AddContent(string[] lines, string[] content, int headerCount)
        {
            var startIndex = Array.IndexOf(lines, lines.FirstOrDefault(line => line.Contains(TableContentPlaceHolder)));
            if (startIndex != -1)
            {
                var sb = new StringBuilder();
                for (int j = 0; j < content.Length; j++)
                {
                    if ((j + 1) % headerCount == 1 && j != 0)
                    {
                        sb.Append("</tr><tr>");
                    }
                    sb.Append($"<td>{content[j]}</td>");
                }
                lines[startIndex] = lines[startIndex].Insert(lines[startIndex].IndexOf(TableContentPlaceHolder), sb.ToString());
            }
        }

    }
}
