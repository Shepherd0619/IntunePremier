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
            var template = File.ReadAllLines("HtmlTemplates/table_with_search.html");

            // Replace title and description 
            for (int i = 0; i < template.Length; i++)
            {
                var line = template[i];
                line = line.Replace(TitlePlaceHolder, title);
                line = line.Replace(DescriptionPlaceHolder, description);
                template[i] = line;
            }

            // Add header
            for (int i = 0; i < template.Length; i++)
            {
                var line = template[i];
                if (line.Contains(TableHeaderPlaceHolder))
                {
                    var startIndex = line.IndexOf(TableHeaderPlaceHolder);
                    line = line.Remove(startIndex, TableHeaderPlaceHolder.Length);

                    var sb = new StringBuilder();
                    for (int j = 0; j < headers.Length; j++)
                    {
                        sb.Append($"<th>{headers[j]}</th>");
                    }

                    line = line.Insert(startIndex, sb.ToString());
                    template[i] = line;
                    break;
                }
            }

            // Add content
            for (int i = 0; i < template.Length; i++)
            {
                var line = template[i];
                if (line.Contains(TableContentPlaceHolder))
                {
                    var startIndex = line.IndexOf(TableContentPlaceHolder);
                    line = line.Remove(startIndex, TableContentPlaceHolder.Length);

                    var sb = new StringBuilder();
                    for (int j = 0; j < content.Length; j++)
                    {
                        if ((j + 1) % headers.Length == 1 && j != 0)
                        {
                            sb.Append("</tr><tr>");
                        }
                        sb.Append($"<td>{content[j]}</td>");
                    }
                    if (content.Length > 0)
                    {
                        sb.Append("</tr>");
                    }

                    line = line.Insert(startIndex, sb.ToString());
                    template[i] = line;
                    break;
                }
            }

            for (int i = 0; i < template.Length; i++)
            {
                var line = template[i];
                line = line.Replace(SearchCol, searchCol.ToString());
                template[i] = line;
            }

            var output = new StringBuilder();

            for (int i = 0; i < template.Length; i++)
            {
                output.AppendLine(template[i]);
            }

            return output.ToString();
        }
    }
}
