namespace AutopilotHelper
{
    partial class EventViewerForm
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
            splitContainer1 = new SplitContainer();
            EvtxListBox = new ListBox();
            label1 = new Label();
            splitContainer2 = new SplitContainer();
            LogListView = new ListView();
            IndexColumn = new ColumnHeader();
            IdColumn = new ColumnHeader();
            LevelColumn = new ColumnHeader();
            DescriptionColumn = new ColumnHeader();
            DateTimeColumn = new ColumnHeader();
            label2 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            LogLineDetailsTextBox = new RichTextBox();
            tabPage2 = new TabPage();
            XmlTextBox = new RichTextBox();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openExternalEvtxToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            openInSystemEventViewerToolStripMenuItem = new ToolStripMenuItem();
            saveCurrentViewIntoCSVToolStripMenuItem = new ToolStripMenuItem();
            saveAllEventsIntoCSVToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            searchToolStripMenuItem = new ToolStripMenuItem();
            filtersToolStripMenuItem = new ToolStripMenuItem();
            searchDescriptionToolStripMenuItem = new ToolStripMenuItem();
            saveFileDialog1 = new SaveFileDialog();
            openFileDialog1 = new OpenFileDialog();
            statusStrip1 = new StatusStrip();
            IOOperationProgressLabel = new ToolStripStatusLabel();
            FilterStatusLabel = new ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            menuStrip1.SuspendLayout();
            statusStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(EvtxListBox);
            splitContainer1.Panel1.Controls.Add(label1);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(splitContainer2);
            splitContainer1.Size = new Size(800, 461);
            splitContainer1.SplitterDistance = 262;
            splitContainer1.TabIndex = 0;
            // 
            // EvtxListBox
            // 
            EvtxListBox.Dock = DockStyle.Fill;
            EvtxListBox.FormattingEnabled = true;
            EvtxListBox.HorizontalScrollbar = true;
            EvtxListBox.ItemHeight = 15;
            EvtxListBox.Location = new Point(0, 15);
            EvtxListBox.Name = "EvtxListBox";
            EvtxListBox.Size = new Size(262, 446);
            EvtxListBox.TabIndex = 1;
            EvtxListBox.DoubleClick += EvtxListBox_DoubleClick;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Dock = DockStyle.Top;
            label1.Location = new Point(0, 0);
            label1.Name = "label1";
            label1.Size = new Size(55, 15);
            label1.TabIndex = 0;
            label1.Text = "EVTX list:";
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(LogListView);
            splitContainer2.Panel1.Controls.Add(label2);
            splitContainer2.Panel1.RightToLeft = RightToLeft.No;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(tabControl1);
            splitContainer2.Panel2.RightToLeft = RightToLeft.No;
            splitContainer2.Size = new Size(534, 461);
            splitContainer2.SplitterDistance = 227;
            splitContainer2.TabIndex = 0;
            // 
            // LogListView
            // 
            LogListView.Columns.AddRange(new ColumnHeader[] { IndexColumn, IdColumn, LevelColumn, DescriptionColumn, DateTimeColumn });
            LogListView.Dock = DockStyle.Fill;
            LogListView.Location = new Point(0, 15);
            LogListView.MultiSelect = false;
            LogListView.Name = "LogListView";
            LogListView.Size = new Size(534, 212);
            LogListView.TabIndex = 1;
            LogListView.UseCompatibleStateImageBehavior = false;
            LogListView.View = View.Details;
            LogListView.ColumnClick += LogListView_ColumnClick;
            LogListView.Click += LogListView_Click;
            // 
            // IndexColumn
            // 
            IndexColumn.Text = "Index";
            // 
            // IdColumn
            // 
            IdColumn.Text = "ID";
            // 
            // LevelColumn
            // 
            LevelColumn.Text = "Level";
            // 
            // DescriptionColumn
            // 
            DescriptionColumn.Text = "Description";
            DescriptionColumn.Width = 330;
            // 
            // DateTimeColumn
            // 
            DateTimeColumn.Text = "DateTime";
            DateTimeColumn.Width = 120;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Dock = DockStyle.Top;
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(44, 15);
            label2.TabIndex = 0;
            label2.Text = "Events:";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(534, 230);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(LogLineDetailsTextBox);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(526, 202);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Details";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // LogLineDetailsTextBox
            // 
            LogLineDetailsTextBox.Dock = DockStyle.Fill;
            LogLineDetailsTextBox.Location = new Point(3, 3);
            LogLineDetailsTextBox.Name = "LogLineDetailsTextBox";
            LogLineDetailsTextBox.ReadOnly = true;
            LogLineDetailsTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            LogLineDetailsTextBox.Size = new Size(520, 196);
            LogLineDetailsTextBox.TabIndex = 0;
            LogLineDetailsTextBox.Text = "";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(XmlTextBox);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(526, 202);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "XML View";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // XmlTextBox
            // 
            XmlTextBox.Dock = DockStyle.Fill;
            XmlTextBox.Location = new Point(3, 3);
            XmlTextBox.Name = "XmlTextBox";
            XmlTextBox.ReadOnly = true;
            XmlTextBox.ScrollBars = RichTextBoxScrollBars.Vertical;
            XmlTextBox.Size = new Size(520, 196);
            XmlTextBox.TabIndex = 1;
            XmlTextBox.Text = "";
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
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openExternalEvtxToolStripMenuItem, toolStripSeparator1, openInSystemEventViewerToolStripMenuItem, saveCurrentViewIntoCSVToolStripMenuItem, saveAllEventsIntoCSVToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openExternalEvtxToolStripMenuItem
            // 
            openExternalEvtxToolStripMenuItem.Name = "openExternalEvtxToolStripMenuItem";
            openExternalEvtxToolStripMenuItem.Size = new Size(290, 22);
            openExternalEvtxToolStripMenuItem.Text = "Open external evtx";
            openExternalEvtxToolStripMenuItem.Click += openExternalEvtxToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(287, 6);
            // 
            // openInSystemEventViewerToolStripMenuItem
            // 
            openInSystemEventViewerToolStripMenuItem.Name = "openInSystemEventViewerToolStripMenuItem";
            openInSystemEventViewerToolStripMenuItem.Size = new Size(290, 22);
            openInSystemEventViewerToolStripMenuItem.Text = "Open current evtx in system event viewer";
            openInSystemEventViewerToolStripMenuItem.Click += openInSystemEventViewerToolStripMenuItem_Click;
            // 
            // saveCurrentViewIntoCSVToolStripMenuItem
            // 
            saveCurrentViewIntoCSVToolStripMenuItem.Name = "saveCurrentViewIntoCSVToolStripMenuItem";
            saveCurrentViewIntoCSVToolStripMenuItem.Size = new Size(290, 22);
            saveCurrentViewIntoCSVToolStripMenuItem.Text = "Save current view into CSV";
            saveCurrentViewIntoCSVToolStripMenuItem.Click += saveCurrentViewIntoCSVToolStripMenuItem_Click;
            // 
            // saveAllEventsIntoCSVToolStripMenuItem
            // 
            saveAllEventsIntoCSVToolStripMenuItem.Name = "saveAllEventsIntoCSVToolStripMenuItem";
            saveAllEventsIntoCSVToolStripMenuItem.Size = new Size(290, 22);
            saveAllEventsIntoCSVToolStripMenuItem.Text = "Save all events into CSV";
            saveAllEventsIntoCSVToolStripMenuItem.Click += saveAllEventsIntoCSVToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(287, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(290, 22);
            exitToolStripMenuItem.Text = "Exit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // searchToolStripMenuItem
            // 
            searchToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { filtersToolStripMenuItem, searchDescriptionToolStripMenuItem });
            searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            searchToolStripMenuItem.Size = new Size(54, 20);
            searchToolStripMenuItem.Text = "Search";
            // 
            // filtersToolStripMenuItem
            // 
            filtersToolStripMenuItem.Name = "filtersToolStripMenuItem";
            filtersToolStripMenuItem.Size = new Size(162, 22);
            filtersToolStripMenuItem.Text = "Filters";
            filtersToolStripMenuItem.Click += filtersToolStripMenuItem_Click;
            // 
            // searchDescriptionToolStripMenuItem
            // 
            searchDescriptionToolStripMenuItem.Name = "searchDescriptionToolStripMenuItem";
            searchDescriptionToolStripMenuItem.ShortcutKeys = Keys.Control | Keys.F;
            searchDescriptionToolStripMenuItem.Size = new Size(162, 22);
            searchDescriptionToolStripMenuItem.Text = "Find next";
            searchDescriptionToolStripMenuItem.Click += searchDescriptionToolStripMenuItem_Click;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.Filter = "CSV file|*.csv";
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "Event Viewer File|*.evtx";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { IOOperationProgressLabel, FilterStatusLabel });
            statusStrip1.Location = new Point(0, 483);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 24);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // IOOperationProgressLabel
            // 
            IOOperationProgressLabel.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            IOOperationProgressLabel.Name = "IOOperationProgressLabel";
            IOOperationProgressLabel.Size = new Size(43, 19);
            IOOperationProgressLabel.Text = "Ready";
            IOOperationProgressLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // FilterStatusLabel
            // 
            FilterStatusLabel.BorderSides = ToolStripStatusLabelBorderSides.Left | ToolStripStatusLabelBorderSides.Right;
            FilterStatusLabel.Name = "FilterStatusLabel";
            FilterStatusLabel.Size = new Size(76, 19);
            FilterStatusLabel.Text = "Filter: NONE";
            FilterStatusLabel.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // EventViewerForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 507);
            Controls.Add(statusStrip1);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "EventViewerForm";
            Text = "EventViewerForm";
            Load += EventViewerForm_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private SplitContainer splitContainer1;
        private ListBox EvtxListBox;
        private Label label1;
        private SplitContainer splitContainer2;
        private Label label2;
        private ListView LogListView;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private TabPage tabPage2;
        private ColumnHeader IdColumn;
        private ColumnHeader LevelColumn;
        private ColumnHeader DescriptionColumn;
        private ColumnHeader DateTimeColumn;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem searchToolStripMenuItem;
        private ToolStripMenuItem filtersToolStripMenuItem;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openInSystemEventViewerToolStripMenuItem;
        private RichTextBox LogLineDetailsTextBox;
        private RichTextBox XmlTextBox;
        private ToolStripMenuItem searchDescriptionToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ColumnHeader IndexColumn;
        private ToolStripMenuItem saveAllEventsIntoCSVToolStripMenuItem;
        private SaveFileDialog saveFileDialog1;
        private ToolStripMenuItem saveCurrentViewIntoCSVToolStripMenuItem;
        private ToolStripMenuItem openExternalEvtxToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private OpenFileDialog openFileDialog1;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel IOOperationProgressLabel;
        public ToolStripStatusLabel FilterStatusLabel;
    }
}