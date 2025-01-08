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
            toolStripSeparator1 = new ToolStripSeparator();
            openWorkspaceFolderToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            tabControl1 = new TabControl();
            AutopilotDiagTab = new TabPage();
            autopilotDiagTextBox1 = new RichTextBox();
            ProfileTab = new TabPage();
            label5 = new Label();
            AutopilotProfileStatusTextBox = new TextBox();
            label4 = new Label();
            label1 = new Label();
            progressBar1 = new ProgressBar();
            EspPage = new TabPage();
            panel3 = new Panel();
            label7 = new Label();
            accountSetupCheckedListBox = new CheckedListBox();
            panel2 = new Panel();
            label6 = new Label();
            deviceSetupCheckedListBox = new CheckedListBox();
            panel1 = new Panel();
            label3 = new Label();
            devicePreparationCheckedListBox = new CheckedListBox();
            label8 = new Label();
            label2 = new Label();
            OOBETab = new TabPage();
            ProcessedPoliciesTab = new TabPage();
            ProcessedPoliciesWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            ProcessedAppsTab = new TabPage();
            ProcessedAppsWebView = new Microsoft.Web.WebView2.WinForms.WebView2();
            menuStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            AutopilotDiagTab.SuspendLayout();
            ProfileTab.SuspendLayout();
            EspPage.SuspendLayout();
            panel3.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ProcessedPoliciesTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ProcessedPoliciesWebView).BeginInit();
            ProcessedAppsTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)ProcessedAppsWebView).BeginInit();
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
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openToolStripMenuItem, closeToolStripMenuItem, toolStripSeparator1, openWorkspaceFolderToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openToolStripMenuItem
            // 
            openToolStripMenuItem.Name = "openToolStripMenuItem";
            openToolStripMenuItem.Size = new Size(196, 22);
            openToolStripMenuItem.Text = "Open";
            openToolStripMenuItem.Click += openToolStripMenuItem_Click;
            // 
            // closeToolStripMenuItem
            // 
            closeToolStripMenuItem.Name = "closeToolStripMenuItem";
            closeToolStripMenuItem.Size = new Size(196, 22);
            closeToolStripMenuItem.Text = "Close";
            closeToolStripMenuItem.Click += closeToolStripMenuItem_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(193, 6);
            // 
            // openWorkspaceFolderToolStripMenuItem
            // 
            openWorkspaceFolderToolStripMenuItem.Name = "openWorkspaceFolderToolStripMenuItem";
            openWorkspaceFolderToolStripMenuItem.Size = new Size(196, 22);
            openWorkspaceFolderToolStripMenuItem.Text = "Open workspace folder";
            openWorkspaceFolderToolStripMenuItem.Click += openWorkspaceFolderToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(107, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(AutopilotDiagTab);
            tabControl1.Controls.Add(ProfileTab);
            tabControl1.Controls.Add(EspPage);
            tabControl1.Controls.Add(OOBETab);
            tabControl1.Controls.Add(ProcessedPoliciesTab);
            tabControl1.Controls.Add(ProcessedAppsTab);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 24);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 510);
            tabControl1.TabIndex = 1;
            // 
            // AutopilotDiagTab
            // 
            AutopilotDiagTab.Controls.Add(autopilotDiagTextBox1);
            AutopilotDiagTab.Location = new Point(4, 24);
            AutopilotDiagTab.Name = "AutopilotDiagTab";
            AutopilotDiagTab.Size = new Size(792, 482);
            AutopilotDiagTab.TabIndex = 2;
            AutopilotDiagTab.Text = "Diagnostics";
            AutopilotDiagTab.UseVisualStyleBackColor = true;
            // 
            // autopilotDiagTextBox1
            // 
            autopilotDiagTextBox1.Dock = DockStyle.Fill;
            autopilotDiagTextBox1.Location = new Point(0, 0);
            autopilotDiagTextBox1.Name = "autopilotDiagTextBox1";
            autopilotDiagTextBox1.ReadOnly = true;
            autopilotDiagTextBox1.ScrollBars = RichTextBoxScrollBars.ForcedVertical;
            autopilotDiagTextBox1.Size = new Size(792, 482);
            autopilotDiagTextBox1.TabIndex = 0;
            autopilotDiagTextBox1.Text = "";
            // 
            // ProfileTab
            // 
            ProfileTab.Controls.Add(label5);
            ProfileTab.Controls.Add(AutopilotProfileStatusTextBox);
            ProfileTab.Controls.Add(label4);
            ProfileTab.Controls.Add(label1);
            ProfileTab.Controls.Add(progressBar1);
            ProfileTab.Location = new Point(4, 24);
            ProfileTab.Name = "ProfileTab";
            ProfileTab.Padding = new Padding(3);
            ProfileTab.Size = new Size(792, 482);
            ProfileTab.TabIndex = 0;
            ProfileTab.Text = "Profile";
            ProfileTab.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point);
            label5.Location = new Point(8, 12);
            label5.Name = "label5";
            label5.Size = new Size(191, 15);
            label5.TabIndex = 6;
            label5.Text = "* Based on end user's pespective.";
            // 
            // AutopilotProfileStatusTextBox
            // 
            AutopilotProfileStatusTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            AutopilotProfileStatusTextBox.Location = new Point(8, 169);
            AutopilotProfileStatusTextBox.Multiline = true;
            AutopilotProfileStatusTextBox.Name = "AutopilotProfileStatusTextBox";
            AutopilotProfileStatusTextBox.ReadOnly = true;
            AutopilotProfileStatusTextBox.ScrollBars = ScrollBars.Vertical;
            AutopilotProfileStatusTextBox.ShortcutsEnabled = false;
            AutopilotProfileStatusTextBox.Size = new Size(774, 221);
            AutopilotProfileStatusTextBox.TabIndex = 5;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(6, 151);
            label4.Name = "label4";
            label4.Size = new Size(129, 15);
            label4.TabIndex = 4;
            label4.Text = "Autopilot Profile Status";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 38);
            label1.Name = "label1";
            label1.Size = new Size(92, 15);
            label1.TabIndex = 1;
            label1.Text = "Overall Progress";
            // 
            // progressBar1
            // 
            progressBar1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            progressBar1.Location = new Point(6, 56);
            progressBar1.Name = "progressBar1";
            progressBar1.Size = new Size(776, 23);
            progressBar1.TabIndex = 0;
            // 
            // EspPage
            // 
            EspPage.Controls.Add(panel3);
            EspPage.Controls.Add(panel2);
            EspPage.Controls.Add(panel1);
            EspPage.Controls.Add(label8);
            EspPage.Controls.Add(label2);
            EspPage.Location = new Point(4, 24);
            EspPage.Name = "EspPage";
            EspPage.Size = new Size(792, 482);
            EspPage.TabIndex = 5;
            EspPage.Text = "ESP";
            EspPage.UseVisualStyleBackColor = true;
            // 
            // panel3
            // 
            panel3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel3.Controls.Add(label7);
            panel3.Controls.Add(accountSetupCheckedListBox);
            panel3.Location = new Point(8, 339);
            panel3.Name = "panel3";
            panel3.Size = new Size(776, 128);
            panel3.TabIndex = 10;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label7.Location = new Point(0, 0);
            label7.Name = "label7";
            label7.Size = new Size(110, 20);
            label7.TabIndex = 6;
            label7.Text = "Account setup";
            // 
            // accountSetupCheckedListBox
            // 
            accountSetupCheckedListBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            accountSetupCheckedListBox.FormattingEnabled = true;
            accountSetupCheckedListBox.Location = new Point(0, 34);
            accountSetupCheckedListBox.Name = "accountSetupCheckedListBox";
            accountSetupCheckedListBox.Size = new Size(776, 94);
            accountSetupCheckedListBox.TabIndex = 5;
            // 
            // panel2
            // 
            panel2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel2.Controls.Add(label6);
            panel2.Controls.Add(deviceSetupCheckedListBox);
            panel2.Location = new Point(8, 205);
            panel2.Name = "panel2";
            panel2.Size = new Size(776, 131);
            panel2.TabIndex = 9;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label6.Location = new Point(0, 0);
            label6.Name = "label6";
            label6.Size = new Size(98, 20);
            label6.TabIndex = 4;
            label6.Text = "Device setup";
            // 
            // deviceSetupCheckedListBox
            // 
            deviceSetupCheckedListBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            deviceSetupCheckedListBox.FormattingEnabled = true;
            deviceSetupCheckedListBox.Location = new Point(0, 37);
            deviceSetupCheckedListBox.Name = "deviceSetupCheckedListBox";
            deviceSetupCheckedListBox.Size = new Size(776, 94);
            deviceSetupCheckedListBox.TabIndex = 3;
            // 
            // panel1
            // 
            panel1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panel1.Controls.Add(label3);
            panel1.Controls.Add(devicePreparationCheckedListBox);
            panel1.Location = new Point(8, 71);
            panel1.Name = "panel1";
            panel1.Size = new Size(776, 128);
            panel1.TabIndex = 8;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11.25F, FontStyle.Bold, GraphicsUnit.Point);
            label3.Location = new Point(0, 0);
            label3.Name = "label3";
            label3.Size = new Size(141, 20);
            label3.TabIndex = 2;
            label3.Text = "Device preparation";
            // 
            // devicePreparationCheckedListBox
            // 
            devicePreparationCheckedListBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            devicePreparationCheckedListBox.FormattingEnabled = true;
            devicePreparationCheckedListBox.Location = new Point(0, 34);
            devicePreparationCheckedListBox.Name = "devicePreparationCheckedListBox";
            devicePreparationCheckedListBox.Size = new Size(776, 94);
            devicePreparationCheckedListBox.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(8, 36);
            label8.Name = "label8";
            label8.Size = new Size(310, 15);
            label8.TabIndex = 7;
            label8.Text = "* for detailed information, please head to Diagnostics tab.";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 14.25F, FontStyle.Bold, GraphicsUnit.Point);
            label2.Location = new Point(8, 11);
            label2.Name = "label2";
            label2.Size = new Size(275, 25);
            label2.TabIndex = 0;
            label2.Text = "Setting up for work or school";
            // 
            // OOBETab
            // 
            OOBETab.Location = new Point(4, 24);
            OOBETab.Name = "OOBETab";
            OOBETab.Padding = new Padding(3);
            OOBETab.Size = new Size(792, 482);
            OOBETab.TabIndex = 1;
            OOBETab.Text = "OOBE";
            OOBETab.UseVisualStyleBackColor = true;
            // 
            // ProcessedPoliciesTab
            // 
            ProcessedPoliciesTab.Controls.Add(ProcessedPoliciesWebView);
            ProcessedPoliciesTab.Location = new Point(4, 24);
            ProcessedPoliciesTab.Name = "ProcessedPoliciesTab";
            ProcessedPoliciesTab.Size = new Size(792, 482);
            ProcessedPoliciesTab.TabIndex = 3;
            ProcessedPoliciesTab.Text = "Processed Policies";
            ProcessedPoliciesTab.UseVisualStyleBackColor = true;
            // 
            // ProcessedPoliciesWebView
            // 
            ProcessedPoliciesWebView.AllowExternalDrop = true;
            ProcessedPoliciesWebView.CreationProperties = null;
            ProcessedPoliciesWebView.DefaultBackgroundColor = Color.White;
            ProcessedPoliciesWebView.Dock = DockStyle.Fill;
            ProcessedPoliciesWebView.Location = new Point(0, 0);
            ProcessedPoliciesWebView.Name = "ProcessedPoliciesWebView";
            ProcessedPoliciesWebView.Size = new Size(792, 482);
            ProcessedPoliciesWebView.TabIndex = 0;
            ProcessedPoliciesWebView.ZoomFactor = 1D;
            // 
            // ProcessedAppsTab
            // 
            ProcessedAppsTab.Controls.Add(ProcessedAppsWebView);
            ProcessedAppsTab.Location = new Point(4, 24);
            ProcessedAppsTab.Name = "ProcessedAppsTab";
            ProcessedAppsTab.Size = new Size(792, 482);
            ProcessedAppsTab.TabIndex = 4;
            ProcessedAppsTab.Text = "Processed Apps";
            ProcessedAppsTab.UseVisualStyleBackColor = true;
            // 
            // ProcessedAppsWebView
            // 
            ProcessedAppsWebView.AllowExternalDrop = true;
            ProcessedAppsWebView.CreationProperties = null;
            ProcessedAppsWebView.DefaultBackgroundColor = Color.White;
            ProcessedAppsWebView.Dock = DockStyle.Fill;
            ProcessedAppsWebView.Location = new Point(0, 0);
            ProcessedAppsWebView.Name = "ProcessedAppsWebView";
            ProcessedAppsWebView.Size = new Size(792, 482);
            ProcessedAppsWebView.TabIndex = 0;
            ProcessedAppsWebView.ZoomFactor = 1D;
            // 
            // MDMAnalysisWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(800, 534);
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
            AutopilotDiagTab.ResumeLayout(false);
            ProfileTab.ResumeLayout(false);
            ProfileTab.PerformLayout();
            EspPage.ResumeLayout(false);
            EspPage.PerformLayout();
            panel3.ResumeLayout(false);
            panel3.PerformLayout();
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ProcessedPoliciesTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ProcessedPoliciesWebView).EndInit();
            ProcessedAppsTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)ProcessedAppsWebView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private TabControl tabControl1;
        private TabPage ProfileTab;
        private TabPage OOBETab;
        private ToolStripMenuItem openToolStripMenuItem;
        private ToolStripMenuItem closeToolStripMenuItem;
        private ProgressBar progressBar1;
        private Label label1;
        private TextBox AutopilotProfileStatusTextBox;
        private Label label4;
        private Label label5;
        private TabPage AutopilotDiagTab;
        private RichTextBox autopilotDiagTextBox1;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private TabPage ProcessedPoliciesTab;
        private Microsoft.Web.WebView2.WinForms.WebView2 ProcessedPoliciesWebView;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem openWorkspaceFolderToolStripMenuItem;
        private TabPage ProcessedAppsTab;
        private Microsoft.Web.WebView2.WinForms.WebView2 ProcessedAppsWebView;
        private TabPage EspPage;
        private Label label2;
        private Label label7;
        private CheckedListBox accountSetupCheckedListBox;
        private Label label6;
        private CheckedListBox deviceSetupCheckedListBox;
        private Label label3;
        private CheckedListBox devicePreparationCheckedListBox;
        private Label label8;
        private Panel panel1;
        private Panel panel3;
        private Panel panel2;
    }
}