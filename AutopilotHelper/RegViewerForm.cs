using AutopilotHelper.Utilities;
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

namespace AutopilotHelper
{
    public partial class RegViewerForm : Form
    {
        private RegFileUtil _reg;

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
            _reg = reg;

            var paths = _reg.GetAllPath();

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };

            InitializeTreeView(paths);
        }

        private void InitializeTreeView(List<string> paths)
        {
            treeView1.Nodes.Clear();
            var root = new TreeNode("Registry");

            foreach (var item in paths)
            {
                AddNodeToTree(root, item);
            }

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
            var selectedNode = e.Node;

            if (selectedNode == null || selectedNode == treeView1.TopNode)
            {
                return;
            }

            var path = selectedNode.FullPath.Substring(("Registry\\").Length);

            var keys = _reg.GetAllKeys(path);

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
    }
}
