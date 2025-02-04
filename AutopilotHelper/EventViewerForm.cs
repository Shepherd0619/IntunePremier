using AutopilotHelper.EventViewer;
using AutopilotHelper.Models;
using AutopilotHelper.Utilities;
using DarkModeForms;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Xml.Linq;

namespace AutopilotHelper
{
    public partial class EventViewerForm : Form
    {
        public readonly List<string> EvtxFiles = new List<string>();
        public EventViewerFile CurrentFile => _CurrentFile;
        private EventViewerFile _CurrentFile;
        private int sortColumn;

        #region Filter
        public FilterInfo? Filter => _Filter;
        private FilterInfo? _Filter;

        public struct FilterInfo
        {
            public int[] Id;
            public string[] LevelDisplayName;
            public string[] Keywords;
            public bool CaseSensitive;

            public override string ToString()
            {
                return $"Id: {string.Join(",", Id)}\nLevelDisplayName: {string.Join(",", LevelDisplayName)}\nKeywords: {string.Join(",", Keywords)}\nCaseSensitive: {CaseSensitive}";
            }
        }

        public void SetFilter(FilterInfo info)
        {
            if (CurrentFile == null) return;

            _Filter = info;

            var list = CurrentFile.records.ToList();

            // Id
            if (info.Id.Length > 0)
            {
                var idExclusion = info.Id.Where(id => id < 0).Select(id => Math.Abs(id)).ToArray();

                var idInclusion = info.Id.Where(id => id >= 0).ToArray();

                if (idExclusion.Length > 0)
                {
                    list.RemoveAll(search => idExclusion.Contains(search.Id));
                }

                if (idInclusion.Length > 0)
                {
                    list.RemoveAll(search => !idInclusion.Contains(search.Id));
                }
            }

            // LevelDisplayName
            if (info.LevelDisplayName.Length > 0)
            {
                list.RemoveAll(search => !info.LevelDisplayName.Contains(search.LevelDisplayName));
            }

            // Keywords
            StringComparison comparison = info.CaseSensitive ? StringComparison.Ordinal : StringComparison.OrdinalIgnoreCase;
            if (info.Keywords.Length > 0)
            {
                list.RemoveAll(search => !info.Keywords.Any(keyword => !string.IsNullOrWhiteSpace(search.FormatDescription) && search.FormatDescription.Contains(keyword, comparison)));
            }

            RenderLogList(list);
        }

        public void ClearFilter()
        {
            if (CurrentFile == null) return;

            _Filter = null;
            FilterStatusLabel.Text = "Filter: None";

            RenderLogList();
        }
        #endregion

        public EventViewerForm()
        {
            InitializeComponent();

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };
        }

        public EventViewerForm(MDMFileUtil diag)
        {
            InitializeComponent();

            SearchEvtx(diag.TmpWorkspacePath);

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };
        }

        private void EventViewerForm_Load(object sender, EventArgs e)
        {

        }

        private void SearchEvtx(string path)
        {
            try
            {
                foreach (string file in Directory.EnumerateFiles(path, "*.evtx", SearchOption.AllDirectories))
                {
                    EvtxFiles.Add(PathUtil.FixSeparator(file));
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error when searching evtx inside tmp workspace dir!\n\n{ex.Message}", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            EvtxListBox.Items.Clear();

            for (int i = 0; i < EvtxFiles.Count; i++)
            {
                var file = EvtxFiles[i];

                EvtxListBox.Items.Add(Path.GetFileName(file));
            }
        }

        private void EvtxListBox_DoubleClick(object sender, EventArgs e)
        {
            var index = EvtxListBox.SelectedIndex;

            var path = EvtxFiles[index];

            OpenEvtx(path);
        }

        public bool OpenEvtx(string path)
        {
            if (string.IsNullOrEmpty(path) || !File.Exists(path))
            {
                IOOperationProgressLabel.Text = "File not found!";
                return false;
            }

            _CurrentFile = new EventViewerFile(path);

            RenderLogList();

            var fixedPath = PathUtil.FixSeparator(path);

            if (!EvtxFiles.Contains(fixedPath))
            {
                EvtxFiles.Add(fixedPath);
                EvtxListBox.Items.Add($"[EXT] {Path.GetFileName(fixedPath)} ({fixedPath})");
            }

            IOOperationProgressLabel.Text = "Open successfully!";

            ClearFilter();

            return true;
        }

        private void RenderLogList()
        {
            RenderLogList(CurrentFile.records);
        }

        private void RenderLogList(List<EventViewerFile.Record> list)
        {
            LogListView.ListViewItemSorter = null;
            LogListView.Items.Clear();

            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem item = new(list[i].Index.ToString());
                item.SubItems.Add(list[i].Id.ToString());
                item.SubItems.Add(list[i].LevelDisplayName);
                item.SubItems.Add(list[i].FormatDescription);
                item.SubItems.Add(list[i].TimeCreated.ToString());

                LogListView.Items.Add(item);
            }

            LogListView.Sorting = SortOrder.Ascending;
            var comparer = new ListViewItemIntComparer(0);
            comparer.SetSortDirection(LogListView.Sorting == SortOrder.Ascending);
            LogListView.ListViewItemSorter = comparer;

            LogListView.Sort();

            IOOperationProgressLabel.Text = $"Total {list.Count} records.";
        }

        private void LogListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            // Determine whether the column is the same as the last column clicked.
            if (e.Column != sortColumn)
            {
                // Set the sort column to the new column.
                sortColumn = e.Column;
            }

            // Determine what the last sort order was and change it.
            if (LogListView.Sorting == SortOrder.Ascending)
                LogListView.Sorting = SortOrder.Descending;
            else
                LogListView.Sorting = SortOrder.Ascending;

            // Set the ListViewItemSorter property to a new ListViewItemComparer
            // object.
            ListViewItemComparerBase comparer;
            switch (e.Column)
            {
                case 4:
                    comparer = new ListViewItemDateTimeComparer(e.Column, 0);
                    comparer.SetSortDirection(LogListView.Sorting == SortOrder.Ascending);

                    LogListView.ListViewItemSorter = comparer;
                    break;

                case 0:
                    comparer = new ListViewItemIntComparer(e.Column);
                    comparer.SetSortDirection(LogListView.Sorting == SortOrder.Ascending);

                    LogListView.ListViewItemSorter = comparer;
                    break;

                default:
                    comparer = new ListViewItemStringComparer(e.Column);
                    comparer.SetSortDirection(LogListView.Sorting == SortOrder.Ascending);

                    LogListView.ListViewItemSorter = comparer;
                    break;
            }

            // Call the sort method to manually sort.
            LogListView.Sort();
        }

        private void openInSystemEventViewerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentFile == null) return;

            Process process = new();
            var startInfo = new ProcessStartInfo();
            startInfo.UseShellExecute = true;
            startInfo.FileName = CurrentFile.FileName;
            process.StartInfo = startInfo;
            process.Start();
        }

        private void LogListView_Click(object sender, EventArgs e)
        {
            var item = CurrentFile.records.Find(search => search.Index == int.Parse(LogListView.Items[LogListView.SelectedIndices[0]].Text));

            LogLineDetailsTextBox.Text = item.ToString();

            // 将XML字符串加载到XDocument中
            XDocument xdoc = XDocument.Parse(item.XmlContent);

            // 使用XDocument.ToString方法进行格式化输出
            XmlTextBox.Text = xdoc.ToString();

            IOOperationProgressLabel.Text = $"Selected: {item.Index}";
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void searchDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SimpleSearchForm(LogListView);

            form.Show(this);
        }

        private void saveAllEventsIntoCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentFile == null) return;

            saveFileDialog1.ShowDialog();
            var fileName = saveFileDialog1.FileName;

            if (string.IsNullOrEmpty(fileName)) return;

            JsonToCsv.jsonToCSV(JsonConvert.SerializeObject(CurrentFile.records), fileName);

            MessageBox.Show("Save successfully!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (MessageBox.Show("Open the csv now?", "QUESTION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var process = new Process();
                process.StartInfo.FileName = fileName;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
        }

        private void saveCurrentViewIntoCSVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (CurrentFile == null) return;

            var filteredRecords = new List<EventViewerFile.Record>();

            for (int i = 0; i < LogListView.Items.Count; i++)
            {
                var item = LogListView.Items[i];

                var index = int.Parse(item.Text);

                var record = CurrentFile.records.Find(search => search.Index == index);

                filteredRecords.Add(record);
            }

            saveFileDialog1.ShowDialog();
            var fileName = saveFileDialog1.FileName;

            if (string.IsNullOrEmpty(fileName)) return;

            JsonToCsv.jsonToCSV(JsonConvert.SerializeObject(filteredRecords), fileName);

            MessageBox.Show("Save successfully!", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (MessageBox.Show("Open the csv now?", "QUESTION", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                var process = new Process();
                process.StartInfo.FileName = fileName;
                process.StartInfo.UseShellExecute = true;
                process.Start();
            }
        }

        private void openExternalEvtxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            var fileName = openFileDialog1.FileName;

            if (string.IsNullOrEmpty(fileName)) return;

            OpenEvtx(fileName);
        }

        private void filtersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FilterForm(this, LogListView);

            form.ShowDialog(this);
        }
    }
}
