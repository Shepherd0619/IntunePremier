﻿using DarkModeForms;
using System.Data;
using System.Text.RegularExpressions;

namespace AutopilotHelper.EventViewer
{
    public partial class FilterForm : Form
    {
        private ListView logListView;
        private EventViewerForm form;

        public FilterForm()
        {
            InitializeComponent();

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };
        }

        public FilterForm(EventViewerForm form, ListView logListView)
        {
            this.logListView = logListView;
            this.form = form;

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };

            InitializeComponent();

            if (form.Filter != null)
            {
                // Load current filter and output to textbox etc.
                EventViewerForm.FilterInfo filter = (EventViewerForm.FilterInfo)form.Filter;

                if (filter.Id != null && filter.Id.Length > 0)
                {
                    textBox1.Text = string.Join(",", filter.Id);
                }

                if (filter.LevelDisplayName != null)
                {
                    foreach (var item in filter.LevelDisplayName)
                    {
                        checkedListBox1.SetItemChecked(checkedListBox1.Items.IndexOf(item), true);
                    }
                }

                if (filter.Keywords != null && filter.Keywords.Length > 0)
                {
                    textBox2.Text = string.Join(",", filter.Keywords);
                }

                checkBox1.Checked = filter.CaseSensitive;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var checkedItems = checkedListBox1.CheckedItems;
            var level = new List<string>();
            foreach (var item in checkedItems)
            {
                level.Add(item.ToString());
            }

            try
            {
                // Initialize filter variables with default values if textbox is empty
                var id = string.IsNullOrEmpty(textBox1.Text) ? new int[] { } : textBox1.Text.Split(',').Select(x => int.Parse(x)).ToArray();
                var keywords = string.IsNullOrEmpty(textBox2.Text) ? new string[] { } : textBox2.Text.Split(',');

                // Create filter object
                var filter = new EventViewerForm.FilterInfo
                {
                    Id = id,
                    LevelDisplayName = level.ToArray(),
                    Keywords = keywords,
                    CaseSensitive = checkBox1.Checked
                };

                form.SetFilter(filter);
                form.FilterStatusLabel.Text = $"Filter: {Regex.Replace(filter.ToString(), @"\r?\n", " ")}";
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Unable to apply your filter.\nPlease check whether there is a syntax error.\n\n{ex}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            form.ClearFilter();
            this.Close();
        }
    }
}
