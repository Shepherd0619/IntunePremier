namespace SoapHelper
{
    partial class StartUpForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openSolutionDialog1 = new OpenFileDialog();
            saveFileDialog1 = new SaveFileDialog();
            menuStrip1 = new MenuStrip();
            fileToolStripMenuItem = new ToolStripMenuItem();
            openSolutionToolStripMenuItem = new ToolStripMenuItem();
            saveSolutionToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            openOutlookMSGToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            openMsgDialog1 = new OpenFileDialog();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            richTextBox1 = new RichTextBox();
            tabPage2 = new TabPage();
            richTextBox2 = new RichTextBox();
            menuStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(800, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openSolutionToolStripMenuItem, saveSolutionToolStripMenuItem, toolStripSeparator1, openOutlookMSGToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
            // 
            // openSolutionToolStripMenuItem
            // 
            openSolutionToolStripMenuItem.Name = "openSolutionToolStripMenuItem";
            openSolutionToolStripMenuItem.Size = new Size(177, 22);
            openSolutionToolStripMenuItem.Text = "Open Solution";
            // 
            // saveSolutionToolStripMenuItem
            // 
            saveSolutionToolStripMenuItem.Name = "saveSolutionToolStripMenuItem";
            saveSolutionToolStripMenuItem.Size = new Size(177, 22);
            saveSolutionToolStripMenuItem.Text = "Save Solution";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(174, 6);
            // 
            // openOutlookMSGToolStripMenuItem
            // 
            openOutlookMSGToolStripMenuItem.Name = "openOutlookMSGToolStripMenuItem";
            openOutlookMSGToolStripMenuItem.Size = new Size(177, 22);
            openOutlookMSGToolStripMenuItem.Text = "Open Outlook MSG";
            openOutlookMSGToolStripMenuItem.Click += openOutlookMSGToolStripMenuItem_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(174, 6);
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.Size = new Size(177, 22);
            exitToolStripMenuItem.Text = "Exit";
            // 
            // openMsgDialog1
            // 
            openMsgDialog1.Filter = "MSG files|*.msg";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 24);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 426);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(richTextBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 398);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Email";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            richTextBox1.Dock = DockStyle.Fill;
            richTextBox1.Location = new Point(3, 3);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox1.Size = new Size(786, 392);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(richTextBox2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 398);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "JSON";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // richTextBox2
            // 
            richTextBox2.Dock = DockStyle.Fill;
            richTextBox2.Location = new Point(3, 3);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox2.Size = new Size(786, 392);
            richTextBox2.TabIndex = 0;
            richTextBox2.Text = "";
            // 
            // StartUpForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "StartUpForm";
            Text = "Form1";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openSolutionDialog1;
        private SaveFileDialog saveFileDialog1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openSolutionToolStripMenuItem;
        private ToolStripMenuItem saveSolutionToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem openOutlookMSGToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private OpenFileDialog openMsgDialog1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private RichTextBox richTextBox1;
        private TabPage tabPage2;
        private RichTextBox richTextBox2;
    }
}
