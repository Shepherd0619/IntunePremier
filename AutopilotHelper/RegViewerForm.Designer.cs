namespace AutopilotHelper
{
    partial class RegViewerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            TreeNode treeNode1 = new TreeNode("Node2");
            TreeNode treeNode2 = new TreeNode("HKEY_LOCAL_MACHINE", new TreeNode[] { treeNode1 });
            TreeNode treeNode3 = new TreeNode("Registry", new TreeNode[] { treeNode2 });
            splitContainer1 = new SplitContainer();
            treeView1 = new TreeView();
            keyValueDetailsTab = new TabControl();
            tabPage1 = new TabPage();
            keyValueDetailsTab_HideBtn = new Button();
            keyValueDetailsTextBox = new TextBox();
            listView1 = new ListView();
            KeyCol = new ColumnHeader();
            ValueCol = new ColumnHeader();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            searchToolStripMenuItem = new ToolStripMenuItem();
            findNextToolStripMenuItem = new ToolStripMenuItem();
            textBox1 = new TextBox();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            keyValueDetailsTab.SuspendLayout();
            tabPage1.SuspendLayout();
            menuStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 48);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(treeView1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(keyValueDetailsTab);
            splitContainer1.Panel2.Controls.Add(listView1);
            splitContainer1.Size = new Size(800, 402);
            splitContainer1.SplitterDistance = 266;
            splitContainer1.TabIndex = 0;
            // 
            // treeView1
            // 
            treeView1.Dock = DockStyle.Fill;
            treeView1.Location = new Point(0, 0);
            treeView1.Name = "treeView1";
            treeNode1.Name = "Node2";
            treeNode1.Text = "Node2";
            treeNode2.Name = "Node1";
            treeNode2.Text = "HKEY_LOCAL_MACHINE";
            treeNode3.Name = "Node0";
            treeNode3.Text = "Registry";
            treeView1.Nodes.AddRange(new TreeNode[] { treeNode3 });
            treeView1.Size = new Size(266, 402);
            treeView1.TabIndex = 0;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            // 
            // keyValueDetailsTab
            // 
            keyValueDetailsTab.Alignment = TabAlignment.Bottom;
            keyValueDetailsTab.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            keyValueDetailsTab.Controls.Add(tabPage1);
            keyValueDetailsTab.Location = new Point(3, 290);
            keyValueDetailsTab.Name = "keyValueDetailsTab";
            keyValueDetailsTab.SelectedIndex = 0;
            keyValueDetailsTab.Size = new Size(515, 100);
            keyValueDetailsTab.TabIndex = 1;
            keyValueDetailsTab.Visible = false;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(keyValueDetailsTab_HideBtn);
            tabPage1.Controls.Add(keyValueDetailsTextBox);
            tabPage1.Location = new Point(4, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(507, 72);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Details";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // keyValueDetailsTab_HideBtn
            // 
            keyValueDetailsTab_HideBtn.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            keyValueDetailsTab_HideBtn.Location = new Point(437, 2);
            keyValueDetailsTab_HideBtn.Name = "keyValueDetailsTab_HideBtn";
            keyValueDetailsTab_HideBtn.Size = new Size(46, 23);
            keyValueDetailsTab_HideBtn.TabIndex = 1;
            keyValueDetailsTab_HideBtn.Text = "Hide";
            keyValueDetailsTab_HideBtn.UseVisualStyleBackColor = true;
            keyValueDetailsTab_HideBtn.Click += keyValueDetailsTab_HideBtn_Click;
            // 
            // keyValueDetailsTextBox
            // 
            keyValueDetailsTextBox.Dock = DockStyle.Fill;
            keyValueDetailsTextBox.Location = new Point(3, 3);
            keyValueDetailsTextBox.Multiline = true;
            keyValueDetailsTextBox.Name = "keyValueDetailsTextBox";
            keyValueDetailsTextBox.ReadOnly = true;
            keyValueDetailsTextBox.ScrollBars = ScrollBars.Vertical;
            keyValueDetailsTextBox.Size = new Size(501, 66);
            keyValueDetailsTextBox.TabIndex = 0;
            // 
            // listView1
            // 
            listView1.Columns.AddRange(new ColumnHeader[] { KeyCol, ValueCol });
            listView1.Dock = DockStyle.Fill;
            listView1.Location = new Point(0, 0);
            listView1.MultiSelect = false;
            listView1.Name = "listView1";
            listView1.Size = new Size(530, 402);
            listView1.TabIndex = 0;
            listView1.UseCompatibleStateImageBehavior = false;
            listView1.View = View.Details;
            listView1.Click += listView1_Click;
            // 
            // KeyCol
            // 
            KeyCol.Text = "Key";
            KeyCol.Width = 120;
            // 
            // ValueCol
            // 
            ValueCol.Text = "Value";
            ValueCol.Width = 400;
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, searchToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { closeToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(103, 22);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // searchToolStripMenuItem
            // 
            searchToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { findNextToolStripMenuItem });
            searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            searchToolStripMenuItem.Size = new Size(54, 20);
            searchToolStripMenuItem.Text = "Search";
            // 
            // findNextToolStripMenuItem
            // 
            findNextToolStripMenuItem.Name = "findNextToolStripMenuItem";
            findNextToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F;
            findNextToolStripMenuItem.Size = new Size(180, 22);
            findNextToolStripMenuItem.Text = "Find next";
            findNextToolStripMenuItem.Click += findNextToolStripMenuItem_Click;
            // 
            // textBox1
            // 
            textBox1.Dock = DockStyle.Top;
            textBox1.Location = new Point(0, 24);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(800, 23);
            textBox1.TabIndex = 2;
            textBox1.KeyDown += textBox1_KeyDown;
            // 
            // RegViewerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(textBox1);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "RegViewerForm";
            Text = "RegViewerForm";
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            keyValueDetailsTab.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainer1;
        public TreeView treeView1;
        public ListView listView1;
        private ColumnHeader KeyCol;
        private ColumnHeader ValueCol;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ToolStripMenuItem searchToolStripMenuItem;
        private ToolStripMenuItem findNextToolStripMenuItem;
        private TextBox textBox1;
        private TabControl keyValueDetailsTab;
        private TabPage tabPage1;
        private TextBox keyValueDetailsTextBox;
        private Button keyValueDetailsTab_HideBtn;
    }
}