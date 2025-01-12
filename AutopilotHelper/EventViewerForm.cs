using AutopilotHelper.Models;
using AutopilotHelper.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutopilotHelper
{
    public partial class EventViewerForm : Form
    {
        public readonly List<string> EvtxFiles = new List<string>();
        public EventViewerFile CurrentFile => _CurrentFile;
        private EventViewerFile _CurrentFile;

        /// <summary>
        /// 每列对应LogListView中的列index。
        /// 0和1分别代表正序和倒序排列标识符。
        /// </summary>
        public int[,] ColumnSortStatus;
        private int[,] _ColumnSortStatus;

        public EventViewerForm()
        {
            InitializeComponent();
        }

        public EventViewerForm(MDMFileUtil diag)
        {
            InitializeComponent();

            SearchEvtx(diag.TmpWorkspacePath);

            // 初始化排序标识符
            _ColumnSortStatus = new int[2, LogListView.Columns.Count];
            for (int i = 0; i < _ColumnSortStatus.GetLength(1); i++)
            {
                _ColumnSortStatus[0, i] = i;
                _ColumnSortStatus[1, i] = 0;
            }
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
                    EvtxFiles.Add(file);
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

            if (string.IsNullOrEmpty(path)) return;

            _CurrentFile = new EventViewerFile(path);

            RenderLogList();
        }

        private void RenderLogList()
        {
            LogListView.Items.Clear();

            // 排序
            var list = CurrentFile.records.OrderByDescending(obj => obj.TimeCreated).ToList();

            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem item = new(list[i].Id.ToString());
                item.SubItems.Add(list[i].LevelDisplayName);
                item.SubItems.Add(list[i].FormatDescription);
                item.SubItems.Add(list[i].TimeCreated.ToString());

                LogListView.Items.Add(item);
            }
        }

        private void LogListView_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            var colIndex = e.Column;
            
            int status = _ColumnSortStatus[1, colIndex];

            switch(status)
            {
                case 0:
                    _ColumnSortStatus[1, colIndex] = status = 1;
                    break;

                case 1:
                    _ColumnSortStatus[1, colIndex] = status = 0;
                    break;
            }

        }
    }
}
