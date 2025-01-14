﻿using AutopilotHelper.Models;
using AutopilotHelper.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using AutopilotHelper.EventViewer;

namespace AutopilotHelper
{
    public partial class EventViewerForm : Form
    {
        public readonly List<string> EvtxFiles = new List<string>();
        public EventViewerFile CurrentFile => _CurrentFile;
        private EventViewerFile _CurrentFile;
        private int sortColumn;

        public EventViewerForm()
        {
            InitializeComponent();
        }

        public EventViewerForm(MDMFileUtil diag)
        {
            InitializeComponent();

            SearchEvtx(diag.TmpWorkspacePath);
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
            LogListView.ListViewItemSorter = null;
            LogListView.Sorting = SortOrder.Ascending;

            var list = CurrentFile.records;

            for (int i = 0; i < list.Count; i++)
            {
                ListViewItem item = new(list[i].Index.ToString());
                item.SubItems.Add(list[i].Id.ToString());
                item.SubItems.Add(list[i].LevelDisplayName);
                item.SubItems.Add(list[i].FormatDescription);
                item.SubItems.Add(list[i].TimeCreated.ToString());

                LogListView.Items.Add(item);
            }
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
                    comparer = new ListViewItemDateTimeComparer(e.Column);
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
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void searchDescriptionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SimpleSearchForm();

            form.ShowDialog();
        }
    }
}
