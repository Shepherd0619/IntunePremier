namespace AutopilotHelper
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
            openFileDialog1 = new OpenFileDialog();
            label1 = new Label();
            panel1 = new Panel();
            AboutMeButton = new Button();
            CollectMDMDiagButton = new Button();
            OpenMDMDiagButton = new Button();
            saveFileDialog1 = new SaveFileDialog();
            RecentMDMDiagList = new ListBox();
            label2 = new Label();
            panel1.SuspendLayout();
            SuspendLayout();
            // 
            // openFileDialog1
            // 
            openFileDialog1.Filter = "CAB file|*.cab|ZIP file|*.zip";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(280, 42);
            label1.TabIndex = 0;
            label1.Text = "Welcome to Autopilot Helper.\nMade by Shepherd Zhu (v-ziruizhu)";
            // 
            // panel1
            // 
            panel1.Controls.Add(AboutMeButton);
            panel1.Controls.Add(CollectMDMDiagButton);
            panel1.Controls.Add(OpenMDMDiagButton);
            panel1.Location = new Point(12, 59);
            panel1.Name = "panel1";
            panel1.Size = new Size(214, 379);
            panel1.TabIndex = 1;
            // 
            // AboutMeButton
            // 
            AboutMeButton.Location = new Point(16, 84);
            AboutMeButton.Name = "AboutMeButton";
            AboutMeButton.Size = new Size(177, 29);
            AboutMeButton.TabIndex = 2;
            AboutMeButton.Text = "About";
            AboutMeButton.UseVisualStyleBackColor = true;
            AboutMeButton.Click += AboutMeButton_Click;
            // 
            // CollectMDMDiagButton
            // 
            CollectMDMDiagButton.Location = new Point(16, 49);
            CollectMDMDiagButton.Name = "CollectMDMDiagButton";
            CollectMDMDiagButton.Size = new Size(177, 29);
            CollectMDMDiagButton.TabIndex = 1;
            CollectMDMDiagButton.Text = "Collect MDM Diag";
            CollectMDMDiagButton.UseVisualStyleBackColor = true;
            // 
            // OpenMDMDiagButton
            // 
            OpenMDMDiagButton.Location = new Point(16, 14);
            OpenMDMDiagButton.Name = "OpenMDMDiagButton";
            OpenMDMDiagButton.Size = new Size(177, 29);
            OpenMDMDiagButton.TabIndex = 0;
            OpenMDMDiagButton.Text = "Open MDM Diag";
            OpenMDMDiagButton.UseVisualStyleBackColor = true;
            OpenMDMDiagButton.Click += OpenMDMDiagButton_Click;
            // 
            // saveFileDialog1
            // 
            saveFileDialog1.FileName = "MDMDiag.zip";
            saveFileDialog1.Filter = "ZIP file|*.zip|CAB file|*.cab";
            // 
            // RecentMDMDiagList
            // 
            RecentMDMDiagList.FormattingEnabled = true;
            RecentMDMDiagList.ItemHeight = 15;
            RecentMDMDiagList.Items.AddRange(new object[] { "ExampleFile.cab", "ExmapleFile.zip" });
            RecentMDMDiagList.Location = new Point(232, 89);
            RecentMDMDiagList.Name = "RecentMDMDiagList";
            RecentMDMDiagList.Size = new Size(556, 349);
            RecentMDMDiagList.TabIndex = 2;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(232, 59);
            label2.Name = "label2";
            label2.Size = new Size(128, 15);
            label2.TabIndex = 3;
            label2.Text = "Recent diagnostic files:";
            // 
            // StartUpForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(RecentMDMDiagList);
            Controls.Add(panel1);
            Controls.Add(label1);
            MaximizeBox = false;
            Name = "StartUpForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Autopilot Helper";
            FormClosed += StartUpForm_FormClosed;
            Load += Form1_Load;
            panel1.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialog1;
        private Label label1;
        private Panel panel1;
        private Button AboutMeButton;
        private Button CollectMDMDiagButton;
        private Button OpenMDMDiagButton;
        private SaveFileDialog saveFileDialog1;
        private ListBox RecentMDMDiagList;
        private Label label2;
    }
}
