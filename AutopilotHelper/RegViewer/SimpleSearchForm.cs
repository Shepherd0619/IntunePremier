using AutopilotHelper.Utilities;
using DarkModeForms;
using System.ComponentModel;

namespace AutopilotHelper.RegViewer
{
    public partial class SimpleSearchForm : Form
    {
        private RegViewerForm _regViewerForm;

        //private Dictionary<string, List<TreeNode>> searchedNodes = new();

        private LoadingForm loadingForm;

        public SimpleSearchForm()
        {
            InitializeComponent();

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };
        }

        public SimpleSearchForm(RegViewerForm regViewerForm)
        {
            _regViewerForm = regViewerForm;

            InitializeComponent();

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Search in the key value list view.
            if (lookAtKeyCheckBox.Checked && ListViewUtil.FindNextKeyword(textBox1.Text, 0, true, false, _regViewerForm.listView1, false))
            {
                return;
            }

            if (lookAtValueCheckBox.Checked && ListViewUtil.FindNextKeyword(textBox1.Text, 1, true, false, _regViewerForm.listView1, false))
            {
                return;
            }

            if (onlyFindInTheCurrentPathCheckBox.Checked)
            {
                MessageBox.Show("No more matches found!\n\n" +
                   "If you currently selected item in list view, it means there is no match according to given direction.",
                   "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            if (_regViewerForm.SearchedNodes.TryAdd(textBox1.Text, new()))
            {
                backgroundWorker1.RunWorkerAsync();

                if (loadingForm != null)
                {
                    loadingForm.Close();
                }

                loadingForm = new LoadingForm();
                loadingForm.ShowDialog(_regViewerForm);
            }
            else
            {
                if (_regViewerForm.SearchedNodes[textBox1.Text].Count == 0)
                {
                    MessageBox.Show("No matches found in the entire registry!",
                        "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var selectedNode = _regViewerForm.treeView1.SelectedNode;

                var index = _regViewerForm.SearchedNodes[textBox1.Text].IndexOf(selectedNode);
                int nextIndex = -1;

                if (index == -1)
                {
                    nextIndex = 0;
                }
                else
                {
                    nextIndex = Math.Min(index + 1, _regViewerForm.SearchedNodes[textBox1.Text].Count - 1);

                    if(nextIndex == _regViewerForm.SearchedNodes[textBox1.Text].Count - 1)
                    {
                        MessageBox.Show("No more matches found in the entire registry!",
                            "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        nextIndex = 0;
                    }
                }

                NavigateToNode(_regViewerForm.SearchedNodes[textBox1.Text][nextIndex]);
            }
        }

        private async void InitialSearch()
        {
            // If nothing in current path, will check all other path.
            await IterateThroughChildNodesAsync(_regViewerForm.treeView1.Nodes[0]);
        }

        private void NavigateToNode(TreeNode child)
        {
            _regViewerForm.treeView1.SelectedNode = child;

            var path = child.FullPath.Substring(("Registry\\").Length);
            var keys = _regViewerForm.Reg.GetAllKeys(path);

            _regViewerForm.PopulateListView(keys);

            if (lookAtKeyCheckBox.Checked && ListViewUtil.FindNextKeyword(textBox1.Text, 0, true, false, _regViewerForm.listView1, false))
            {
                return;
            }
            if (lookAtValueCheckBox.Checked && ListViewUtil.FindNextKeyword(textBox1.Text, 1, true, false, _regViewerForm.listView1, false))
            {
                return;
            }
        }

        private void IterateThroughChildNodes(TreeNode parentNode)
        {
            StringComparison comparison;

            if (caseSensitiveCheckBox.Checked)
            {
                comparison = StringComparison.Ordinal;
            }
            else
            {
                comparison = StringComparison.OrdinalIgnoreCase;
            }

            foreach (TreeNode child in parentNode.Nodes)
            {
                // Do something with each child node, e.g., display text or modify properties.
                var path = child.FullPath.Substring(("Registry\\").Length);
                var keys = _regViewerForm.Reg.GetAllKeys(path);

                foreach (var pair in keys)
                {
                    if ((lookAtKeyCheckBox.Checked && pair.Key.Contains(textBox1.Text, comparison)) ||
                        (lookAtValueCheckBox.Checked && pair.Value != null && pair.Value.Contains(textBox1.Text, comparison)))
                    {
                        if (_regViewerForm.SearchedNodes[textBox1.Text].Contains(child)) continue;

                        _regViewerForm.SearchedNodes[textBox1.Text].Add(child);

                        return;
                    }
                }

                // Recursively call a method to iterate through all sub-child nodes of the current child node.
                IterateThroughChildNodes(child);
            }
        }

        private async Task IterateThroughChildNodesAsync(TreeNode parentNode)
        {
            StringComparison comparison;

            if (caseSensitiveCheckBox.Checked)
            {
                comparison = StringComparison.Ordinal;
            }
            else
            {
                comparison = StringComparison.OrdinalIgnoreCase;
            }

            foreach (TreeNode child in parentNode.Nodes)
            {
                // Do something with each child node, e.g., display text or modify properties.
                var path = child.FullPath.Substring(("Registry\\").Length);
                var keys = _regViewerForm.Reg.GetAllKeys(path);

                foreach (var pair in keys)
                {
                    if ((lookAtKeyCheckBox.Checked && pair.Key.Contains(textBox1.Text, comparison)) ||
                        (lookAtValueCheckBox.Checked && pair.Value != null && pair.Value.Contains(textBox1.Text, comparison)))
                    {
                        if (_regViewerForm.SearchedNodes[textBox1.Text].Contains(child)) continue;

                        _regViewerForm.SearchedNodes[textBox1.Text].Add(child);

                        return;
                    }
                }

                // Recursively call a method to iterate through all sub-child nodes of the current child node.
                await IterateThroughChildNodesAsync(child);
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            InitialSearch();
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (loadingForm != null)
            {
                loadingForm.Close();
            }

            if (_regViewerForm.SearchedNodes[textBox1.Text].Count == 0)
            {
                MessageBox.Show("No matches found in the entire registry!",
                    "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            NavigateToNode(_regViewerForm.SearchedNodes[textBox1.Text][0]);
        }
    }
}
