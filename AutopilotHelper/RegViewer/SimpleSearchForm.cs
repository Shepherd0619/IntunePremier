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
            if(_regViewerForm.Reg == null)
            {
                MessageBox.Show("Registry is not loaded yet!", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Search in the key value list view.
            if (lookAtKeyCheckBox.Checked && ListViewUtil.FindNextKeyword(textBox1.Text, 0, true, caseSensitiveCheckBox.Checked, _regViewerForm.listView1, false))
            {
                return;
            }

            if (lookAtValueCheckBox.Checked && ListViewUtil.FindNextKeyword(textBox1.Text, 1, true, caseSensitiveCheckBox.Checked, _regViewerForm.listView1, false))
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

            var searchInfo = new RegViewerForm.SearchedNodeInfo()
            {
                CaseSensitive = caseSensitiveCheckBox.Checked,
            };

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
                if(searchInfo.CaseSensitive != _regViewerForm.SearchedNodes[textBox1.Text].CaseSensitive)
                {
                    _regViewerForm.SearchedNodes[textBox1.Text] = searchInfo;
                    backgroundWorker1.RunWorkerAsync();

                    if (loadingForm != null)
                    {
                        loadingForm.Close();
                    }

                    loadingForm = new LoadingForm();
                    loadingForm.ShowDialog(_regViewerForm);

                    return;
                }

                if (_regViewerForm.SearchedNodes[textBox1.Text].Nodes.Count == 0)
                {
                    MessageBox.Show("No matches found in the entire registry!",
                        "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var selectedNode = _regViewerForm.treeView1.SelectedNode;

                var index = _regViewerForm.SearchedNodes[textBox1.Text].Nodes.IndexOf(selectedNode);
                int nextIndex = -1;

                if (index == -1)
                {
                    nextIndex = 0;
                }
                else
                {
                    nextIndex = Math.Min(index + 1, _regViewerForm.SearchedNodes[textBox1.Text].Nodes.Count - 1);

                    if(nextIndex == _regViewerForm.SearchedNodes[textBox1.Text].Nodes.Count - 1)
                    {
                        MessageBox.Show("No more matches found in the entire registry!",
                            "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        nextIndex = 0;
                    }
                }

                NavigateToNode(_regViewerForm.SearchedNodes[textBox1.Text].Nodes[nextIndex]);
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

            if (lookAtKeyCheckBox.Checked && ListViewUtil.FindNextKeyword(textBox1.Text, 0, true, caseSensitiveCheckBox.Checked, _regViewerForm.listView1, false))
            {
                return;
            }
            if (lookAtValueCheckBox.Checked && ListViewUtil.FindNextKeyword(textBox1.Text, 1, true, caseSensitiveCheckBox.Checked, _regViewerForm.listView1, false))
            {
                return;
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
                        if (_regViewerForm.SearchedNodes[textBox1.Text].Nodes.Contains(child)) continue;

                        _regViewerForm.SearchedNodes[textBox1.Text].Nodes.Add(child);

                        return;
                    }
                    else if((!lookAtKeyCheckBox.Checked && !lookAtValueCheckBox.Checked) && child.Text.Contains(textBox1.Text, comparison))
                    {
                        if (_regViewerForm.SearchedNodes[textBox1.Text].Nodes.Contains(child)) continue;

                        _regViewerForm.SearchedNodes[textBox1.Text].Nodes.Add(child);

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

            if (_regViewerForm.SearchedNodes[textBox1.Text].Nodes.Count == 0)
            {
                MessageBox.Show("No matches found in the entire registry!",
                    "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            NavigateToNode(_regViewerForm.SearchedNodes[textBox1.Text].Nodes[0]);
        }
    }
}
