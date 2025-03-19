namespace AutopilotHelper.Chat
{
    partial class ChatBotSettingsWindow
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
            textBox3 = new TextBox();
            label3 = new Label();
            textBox2 = new TextBox();
            label2 = new Label();
            textBox1 = new TextBox();
            label1 = new Label();
            button1 = new Button();
            comboBox1 = new ComboBox();
            label4 = new Label();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            textBox4 = new TextBox();
            label5 = new Label();
            SuspendLayout();
            // 
            // textBox3
            // 
            textBox3.Location = new Point(70, 208);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(179, 23);
            textBox3.TabIndex = 11;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(14, 211);
            label3.Name = "label3";
            label3.Size = new Size(44, 15);
            label3.TabIndex = 10;
            label3.Text = "Model:";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(70, 161);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(179, 23);
            textBox2.TabIndex = 9;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(14, 164);
            label2.Name = "label2";
            label2.Size = new Size(42, 15);
            label2.TabIndex = 8;
            label2.Text = "Secret:";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(70, 118);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(179, 23);
            textBox1.TabIndex = 7;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 121);
            label1.Name = "label1";
            label1.Size = new Size(52, 15);
            label1.TabIndex = 6;
            label1.Text = "Address:";
            // 
            // button1
            // 
            button1.Location = new Point(12, 252);
            button1.Name = "button1";
            button1.Size = new Size(101, 23);
            button1.TabIndex = 12;
            button1.Text = "Save and quit";
            button1.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(91, 12);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(158, 23);
            comboBox1.TabIndex = 13;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(14, 15);
            label4.Name = "label4";
            label4.Size = new Size(71, 15);
            label4.TabIndex = 14;
            label4.Text = "Current Bot:";
            // 
            // button2
            // 
            button2.Location = new Point(119, 252);
            button2.Name = "button2";
            button2.Size = new Size(130, 23);
            button2.TabIndex = 15;
            button2.Text = "Quit without saving";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Location = new Point(93, 43);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 16;
            button3.Text = "Add";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(174, 43);
            button4.Name = "button4";
            button4.Size = new Size(75, 23);
            button4.TabIndex = 17;
            button4.Text = "Remove";
            button4.UseVisualStyleBackColor = true;
            // 
            // textBox4
            // 
            textBox4.Location = new Point(101, 77);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(148, 23);
            textBox4.TabIndex = 19;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(12, 80);
            label5.Name = "label5";
            label5.Size = new Size(83, 15);
            label5.TabIndex = 18;
            label5.Text = "Display Name:";
            // 
            // ChatBotSettingsWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(265, 287);
            Controls.Add(textBox4);
            Controls.Add(label5);
            Controls.Add(button4);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(label4);
            Controls.Add(comboBox1);
            Controls.Add(button1);
            Controls.Add(textBox3);
            Controls.Add(label3);
            Controls.Add(textBox2);
            Controls.Add(label2);
            Controls.Add(textBox1);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedDialog;
            Name = "ChatBotSettingsWindow";
            Text = "ChatBotSettingsWindow";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBox3;
        private Label label3;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox1;
        private Label label1;
        private Button button1;
        private ComboBox comboBox1;
        private Label label4;
        private Button button2;
        private Button button3;
        private Button button4;
        private TextBox textBox4;
        private Label label5;
    }
}