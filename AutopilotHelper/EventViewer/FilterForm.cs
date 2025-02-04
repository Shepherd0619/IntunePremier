using AutopilotHelper.Models;
using DarkModeForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

            // Load current filter and output to textbox etc.

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };

            InitializeComponent();
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
