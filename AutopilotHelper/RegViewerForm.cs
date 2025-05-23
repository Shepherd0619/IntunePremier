﻿using AutopilotHelper.RegViewer;
using AutopilotHelper.Utilities;
using DarkModeForms;
using System.Text;

namespace AutopilotHelper
{
    public partial class RegViewerForm : Form
    {
        public RegFileUtil Reg => _reg;
        private RegFileUtil _reg;

        public class SearchedNodeInfo
        {
            public bool CaseSensitive;
            public bool LookForKey;
            public bool LookForValue;
            public readonly List<TreeNode> Nodes = new();
        }
        public Dictionary<string, SearchedNodeInfo> SearchedNodes = new();

        public RegViewerForm()
        {
            InitializeComponent();

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };
        }

        public RegViewerForm(RegFileUtil reg)
        {
            InitializeComponent();

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };

            OpenReg(reg);
        }

        private void InitializeTreeView(List<string> paths)
        {
            treeView1.Nodes.Clear();
            var root = new TreeNode("Registry");

            foreach (var item in paths)
            {
                AddNodeToTree(root, item);
            }

            root.Expand();

            treeView1.Nodes.Add(root);
        }

        private void AddNodeToTree(TreeNode parentNode, string registryPath)
        {
            // Split the path and add nodes accordingly.
            string[] parts = registryPath.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);

            TreeNode currentNode = parentNode;

            foreach (var part in parts)
            {
                bool isLastPart = (part == parts[^1]);
                if (isLastPart && !string.IsNullOrEmpty(part))
                {
                    if (currentNode.Nodes.ContainsKey(part))
                    {
                        // If the node already exists, skip it.
                        continue;
                    }

                    // Add the last node as a leaf.
                    currentNode.Nodes.Add(part, part);
                }
                else
                {
                    // Add intermediate nodes and continue.
                    if (!currentNode.Nodes.ContainsKey(part))
                    {
                        currentNode.Nodes.Add(part, part);
                    }
                    currentNode = currentNode.Nodes[part];
                }
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (_reg == null)
            {
                return;
            }

            var selectedNode = e.Node;

            if (selectedNode == null || selectedNode == treeView1.TopNode)
            {
                return;
            }

            var path = selectedNode.FullPath.Substring(("Registry\\").Length);
            textBox1.Text = path;
            var keys = _reg.GetAllKeys(path);

            listView1.Items.Clear();
            PopulateListView(keys);
        }

        public void PopulateListView(Dictionary<string, string> keys)
        {
            listView1.Items.Clear();

            foreach (var item in keys)
            {
                var listViewItem = new ListViewItem();
                listViewItem.Text = item.Key;
                // TODO: Type
                //listViewItem.SubItems.Add(string.Empty);
                listViewItem.SubItems.Add(item.Value);

                listView1.Items.Add(listViewItem);
            }
        }

        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue != 13) return;

            var path = textBox1.Text;

            var items = path.Split("\\");

            TreeNode currentNode = treeView1.Nodes[0];

            for (int i = 0; i < items.Length; i++)
            {
                var result = currentNode.Nodes.Find(items[i], false);

                if (result == null || result.Length <= 0)
                {
                    MessageBox.Show("Path not found.", "WARN", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    if (treeView1.SelectedNode != null)
                    {
                        if (treeView1.SelectedNode.FullPath.StartsWith("Registry\\"))
                        {
                            textBox1.Text = treeView1.SelectedNode.FullPath.Substring(("Registry\\").Length);
                        }
                        else
                        {
                            textBox1.Text = string.Empty;
                        }
                    }
                    else
                    {
                        textBox1.Text = string.Empty;
                    }

                    break;
                }

                currentNode = result[0];
            }

            treeView1.SelectedNode = currentNode;
        }

        private void keyValueDetailsTab_HideBtn_Click(object sender, EventArgs e)
        {
            keyValueDetailsTab.Visible = false;
        }

        private void listView1_Click(object sender, EventArgs e)
        {
            keyValueDetailsTab.Visible = true;

            var item = listView1.Items[listView1.SelectedIndices[0]];

            var sb = new StringBuilder();
            sb.AppendLine($"Key: {item.SubItems[0].Text}");
            sb.AppendLine();
            sb.AppendLine($"Value: {item.SubItems[1].Text}");

            keyValueDetailsTextBox.Text = sb.ToString();
        }

        private void findNextToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new SimpleSearchForm(this);

            form.Show(this);
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

            var path = openFileDialog1.FileName;

            OpenReg(path);
        }

        public void OpenReg(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                return;
            }

            if (!File.Exists(path))
            {
                MessageBox.Show("Invalid file path.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            OpenReg(new RegFileUtil(path));
        }

        public void OpenReg(RegFileUtil file)
        {
            _reg = file;
            var paths = _reg.GetAllPath();
            InitializeTreeView(paths);
            SearchedNodes.Clear();
        }
    }
}
