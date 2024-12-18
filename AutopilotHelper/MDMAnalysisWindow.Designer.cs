namespace AutopilotHelper
{
    partial class MDMAnalysisWindow
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
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openToolStripMenuItem = new ToolStripMenuItem();
            closeToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            tabControl1 = new TabControl();
            GeneralTab = new TabPage();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            progressBar1 = new ProgressBar();
            OOBETab = new TabPage();
            label4 = new Label();
            AutopilotProfileStatusTextBox = new TextBox();
            menuStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            GeneralTab.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, closeToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(103, 22);
            openToolStripMenuItem.Text = "Open";
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(103, 22);
            closeToolStripMenuItem.Text = "Close";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(GeneralTab);
            tabControl1.Controls.Add(OOBETab);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 24);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 426);
            tabControl1.TabIndex = 1;
            // 
            // GeneralTab
            // 
            GeneralTab.Controls.Add(AutopilotProfileStatusTextBox);
            GeneralTab.Controls.Add(label4);
            GeneralTab.Controls.Add(label3);
            GeneralTab.Controls.Add(label2);
            GeneralTab.Controls.Add(label1);
            GeneralTab.Controls.Add(progressBar1);
            GeneralTab.Location = new Point(4, 24);
            GeneralTab.Name = "GeneralTab";
            GeneralTab.Padding = new Padding(3);
            GeneralTab.Size = new Size(792, 398);
            GeneralTab.TabIndex = 0;
            GeneralTab.Text = "General";
            GeneralTab.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label3.AutoSize = true;
            label3.Location = new Point(98, 62);
            label3.Name = "label3";
            label3.Size = new Size(59, 15);
            label3.TabIndex = 3;
            label3.Text = "Language";
            // 
            // label2
            // 
            label2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            label2.AutoSize = true;
            label2.Location = new Point(8, 62);
            label2.Name = "label2";
            label2.Size = new Size(38, 30);
            label2.TabIndex = 2;
            label2.Text = "OOBE\r\nBegin";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 7);
            label1.Name = "label1";
            label1.Size = new Size(92, 15);
            label1.TabIndex = 1;
            label1.Text = "Overall Progress";
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(6, 25);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(776, 23);
            progressBar1.TabIndex = 0;
            // 
            // OOBETab
            // 
            OOBETab.Location = new Point(4, 24);
            OOBETab.Name = "OOBETab";
            OOBETab.Padding = new Padding(3);
            OOBETab.Size = new Size(792, 398);
            OOBETab.TabIndex = 1;
            OOBETab.Text = "OOBE";
            OOBETab.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 120);
            label4.Name = "label4";
            label4.Size = new Size(129, 15);
            label4.TabIndex = 4;
            label4.Text = "Autopilot Profile Status";
            // 
            // AutopilotProfileStatusTextBox
            // 
            AutopilotProfileStatusTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AutopilotProfileStatusTextBox.Location = new Point(8, 138);
            AutopilotProfileStatusTextBox.Multiline = true;
            AutopilotProfileStatusTextBox.Name = "AutopilotProfileStatusTextBox";
            AutopilotProfileStatusTextBox.ReadOnly = true;
            AutopilotProfileStatusTextBox.ScrollBars = ScrollBars.Vertical;
            AutopilotProfileStatusTextBox.ShortcutsEnabled = false;
            AutopilotProfileStatusTextBox.Size = new Size(774, 123);
            AutopilotProfileStatusTextBox.TabIndex = 5;
            // 
            // MDMAnalysisWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "MDMAnalysisWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "MDMAnalysisWindow";
            FormClosed += MDMAnalysisWindow_FormClosed;
            Load += MDMAnalysisWindow_Load;
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            GeneralTab.ResumeLayout(false);
            GeneralTab.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage GeneralTab;
        private TabPage OOBETab;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ProgressBar progressBar1;
        private Label label3;
        private Label label2;
        private Label label1;
        private TextBox AutopilotProfileStatusTextBox;
        private Label label4;
    }
}