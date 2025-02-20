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
            openOutlookMSGToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            exitToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            openMsgDialog1 = new OpenFileDialog();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            label3 = new Label();
            richTextBox1 = new RichTextBox();
            tabPage2 = new TabPage();
            EmailThreadJsonGenerateBtn = new Button();
            label2 = new Label();
            richTextBox2 = new RichTextBox();
            tabPage3 = new TabPage();
            textBox1 = new TextBox();
            SoapGenerateBtn = new Button();
            label1 = new Label();
            richTextBox3 = new RichTextBox();
            statusStrip1 = new StatusStrip();
            toolStripStatusLabel1 = new ToolStripStatusLabel();
            menuStrip1.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            tabPage2.SuspendLayout();
            tabPage3.SuspendLayout();
            statusStrip1.SuspendLayout();
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
            fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { openOutlookMSGToolStripMenuItem, toolStripSeparator2, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new Size(37, 20);
            fileToolStripMenuItem.Text = "File";
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
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
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
            aboutToolStripMenuItem.Size = new Size(180, 22);
            aboutToolStripMenuItem.Text = "About";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // openMsgDialog1
            // 
            openMsgDialog1.Filter = "MSG files|*.msg";
            // 
            // tabControl1
            // 
            tabControl1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Location = new Point(0, 24);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(800, 401);
            tabControl1.TabIndex = 1;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(label3);
            tabPage1.Controls.Add(richTextBox1);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(792, 373);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "Email";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(3, 3);
            label3.Name = "label3";
            label3.Size = new Size(607, 30);
            label3.TabIndex = 3;
            label3.Text = "* Load a email thread from MSG file. Check whether the text looks good before letting LLM understand the thread.\r\nYou can get a MSG file by exporting the latest email from Outlook.";
            // 
            // richTextBox1
            // 
            richTextBox1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox1.Location = new Point(3, 36);
            richTextBox1.Name = "richTextBox1";
            richTextBox1.ReadOnly = true;
            richTextBox1.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox1.Size = new Size(786, 334);
            richTextBox1.TabIndex = 2;
            richTextBox1.Text = "";
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(EmailThreadJsonGenerateBtn);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(richTextBox2);
            tabPage2.Location = new Point(4, 24);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(792, 373);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Thread";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // EmailThreadJsonGenerateBtn
            // 
            EmailThreadJsonGenerateBtn.Enabled = false;
            EmailThreadJsonGenerateBtn.Location = new Point(589, 3);
            EmailThreadJsonGenerateBtn.Name = "EmailThreadJsonGenerateBtn";
            EmailThreadJsonGenerateBtn.Size = new Size(75, 23);
            EmailThreadJsonGenerateBtn.TabIndex = 2;
            EmailThreadJsonGenerateBtn.Text = "Generate";
            EmailThreadJsonGenerateBtn.UseVisualStyleBackColor = true;
            EmailThreadJsonGenerateBtn.Click += EmailThreadGenerateBtn_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(6, 7);
            label2.Name = "label2";
            label2.Size = new Size(577, 15);
            label2.TabIndex = 1;
            label2.Text = "* Carefully check the email thread understood by LLM. Make sure this is correct before generating the SOAP.";
            // 
            // richTextBox2
            // 
            richTextBox2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox2.Location = new Point(3, 32);
            richTextBox2.Name = "richTextBox2";
            richTextBox2.ReadOnly = true;
            richTextBox2.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox2.Size = new Size(786, 363);
            richTextBox2.TabIndex = 0;
            richTextBox2.Text = "";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(textBox1);
            tabPage3.Controls.Add(SoapGenerateBtn);
            tabPage3.Controls.Add(label1);
            tabPage3.Controls.Add(richTextBox3);
            tabPage3.Location = new Point(4, 24);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new Size(792, 373);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "SOAP";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(81, 3);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(255, 23);
            textBox1.TabIndex = 4;
            // 
            // SoapGenerateBtn
            // 
            SoapGenerateBtn.Enabled = false;
            SoapGenerateBtn.Location = new Point(342, 3);
            SoapGenerateBtn.Name = "SoapGenerateBtn";
            SoapGenerateBtn.Size = new Size(75, 23);
            SoapGenerateBtn.TabIndex = 3;
            SoapGenerateBtn.Text = "Generate";
            SoapGenerateBtn.UseVisualStyleBackColor = true;
            SoapGenerateBtn.Click += SoapGenerateBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 6);
            label1.Name = "label1";
            label1.Size = new Size(67, 15);
            label1.TabIndex = 1;
            label1.Text = "Your name:";
            // 
            // richTextBox3
            // 
            richTextBox3.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            richTextBox3.Location = new Point(0, 29);
            richTextBox3.Name = "richTextBox3";
            richTextBox3.ReadOnly = true;
            richTextBox3.ScrollBars = RichTextBoxScrollBars.Vertical;
            richTextBox3.Size = new Size(792, 369);
            richTextBox3.TabIndex = 0;
            richTextBox3.Text = "";
            // 
            // statusStrip1
            // 
            statusStrip1.Items.AddRange(new ToolStripItem[] { toolStripStatusLabel1 });
            statusStrip1.Location = new Point(0, 428);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.Size = new Size(800, 22);
            statusStrip1.TabIndex = 2;
            statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            toolStripStatusLabel1.Size = new Size(126, 17);
            toolStripStatusLabel1.Tag = "";
            toolStripStatusLabel1.Text = "Waitting for MSG file...";
            // 
            // StartUpForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(statusStrip1);
            Controls.Add(tabControl1);
            Controls.Add(menuStrip1);
            MainMenuStrip = menuStrip1;
            Name = "StartUpForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SoapHelper";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openSolutionDialog1;
        private SaveFileDialog saveFileDialog1;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem openOutlookMSGToolStripMenuItem;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem exitToolStripMenuItem;
        private OpenFileDialog openMsgDialog1;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private RichTextBox richTextBox1;
        private TabPage tabPage2;
        private RichTextBox richTextBox2;
        private TabPage tabPage3;
        private RichTextBox richTextBox3;
        private Button SoapGenerateBtn;
        private Label label1;
        private TextBox textBox1;
        private Button EmailThreadJsonGenerateBtn;
        private Label label2;
        private Label label3;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel toolStripStatusLabel1;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem aboutToolStripMenuItem;
    }
}
