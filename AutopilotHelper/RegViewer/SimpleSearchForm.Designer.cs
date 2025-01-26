namespace AutopilotHelper.RegViewer
{
    partial class SimpleSearchForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleSearchForm));
            textBox1 = new TextBox();
            button1 = new Button();
            button2 = new Button();
            groupBox1 = new GroupBox();
            lookAtValueCheckBox = new CheckBox();
            lookAtKeyCheckBox = new CheckBox();
            caseSensitiveCheckBox = new CheckBox();
            onlyFindInTheCurrentPathCheckBox = new CheckBox();
            label1 = new Label();
            backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // textBox1
            // 
            textBox1.Location = new Point(12, 12);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(332, 23);
            textBox1.TabIndex = 0;
            // 
            // button1
            // 
            button1.Location = new Point(356, 11);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 1;
            button1.Text = "Find";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // button2
            // 
            button2.Location = new Point(356, 40);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 2;
            button2.Text = "Cancel";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(lookAtValueCheckBox);
            groupBox1.Controls.Add(lookAtKeyCheckBox);
            groupBox1.Location = new Point(12, 41);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(88, 77);
            groupBox1.TabIndex = 3;
            groupBox1.TabStop = false;
            groupBox1.Text = "Look at";
            // 
            // lookAtValueCheckBox
            // 
            lookAtValueCheckBox.AutoSize = true;
            lookAtValueCheckBox.Checked = true;
            lookAtValueCheckBox.CheckState = CheckState.Checked;
            lookAtValueCheckBox.Location = new Point(12, 49);
            lookAtValueCheckBox.Name = "lookAtValueCheckBox";
            lookAtValueCheckBox.Size = new Size(54, 19);
            lookAtValueCheckBox.TabIndex = 1;
            lookAtValueCheckBox.Text = "Value";
            lookAtValueCheckBox.UseVisualStyleBackColor = true;
            // 
            // lookAtKeyCheckBox
            // 
            lookAtKeyCheckBox.AutoSize = true;
            lookAtKeyCheckBox.Checked = true;
            lookAtKeyCheckBox.CheckState = CheckState.Checked;
            lookAtKeyCheckBox.Location = new Point(12, 24);
            lookAtKeyCheckBox.Name = "lookAtKeyCheckBox";
            lookAtKeyCheckBox.Size = new Size(45, 19);
            lookAtKeyCheckBox.TabIndex = 0;
            lookAtKeyCheckBox.Text = "Key";
            lookAtKeyCheckBox.UseVisualStyleBackColor = true;
            // 
            // caseSensitiveCheckBox
            // 
            caseSensitiveCheckBox.AutoSize = true;
            caseSensitiveCheckBox.Location = new Point(106, 65);
            caseSensitiveCheckBox.Name = "caseSensitiveCheckBox";
            caseSensitiveCheckBox.Size = new Size(99, 19);
            caseSensitiveCheckBox.TabIndex = 4;
            caseSensitiveCheckBox.Text = "Case sensitive";
            caseSensitiveCheckBox.UseVisualStyleBackColor = true;
            // 
            // onlyFindInTheCurrentPathCheckBox
            // 
            onlyFindInTheCurrentPathCheckBox.AutoSize = true;
            onlyFindInTheCurrentPathCheckBox.Checked = true;
            onlyFindInTheCurrentPathCheckBox.CheckState = CheckState.Checked;
            onlyFindInTheCurrentPathCheckBox.Location = new Point(106, 90);
            onlyFindInTheCurrentPathCheckBox.Name = "onlyFindInTheCurrentPathCheckBox";
            onlyFindInTheCurrentPathCheckBox.Size = new Size(176, 19);
            onlyFindInTheCurrentPathCheckBox.TabIndex = 5;
            onlyFindInTheCurrentPathCheckBox.Text = "Only find in the current path";
            onlyFindInTheCurrentPathCheckBox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 125);
            label1.Name = "label1";
            label1.Size = new Size(415, 45);
            label1.TabIndex = 6;
            label1.Text = resources.GetString("label1.Text");
            // 
            // backgroundWorker1
            // 
            backgroundWorker1.DoWork += backgroundWorker1_DoWork;
            backgroundWorker1.RunWorkerCompleted += backgroundWorker1_RunWorkerCompleted;
            // 
            // SimpleSearchForm
            // 
            AcceptButton = button1;
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            CancelButton = button2;
            ClientSize = new Size(443, 179);
            Controls.Add(label1);
            Controls.Add(onlyFindInTheCurrentPathCheckBox);
            Controls.Add(caseSensitiveCheckBox);
            Controls.Add(groupBox1);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(textBox1);
            FormBorderStyle = FormBorderStyle.FixedToolWindow;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SimpleSearchForm";
            ShowInTaskbar = false;
            Text = "Find next";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox1;
        private Button button1;
        private Button button2;
        private GroupBox groupBox1;
        private CheckBox lookAtValueCheckBox;
        private CheckBox lookAtKeyCheckBox;
        private CheckBox caseSensitiveCheckBox;
        private CheckBox onlyFindInTheCurrentPathCheckBox;
        private Label label1;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
    }
}