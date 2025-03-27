using AutopilotHelper.Models;
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

namespace AutopilotHelper.Chat
{
    public partial class ChatBotSettingsWindow : Form
    {
        private readonly List<ChatBot> chatBots;

        public ChatBotSettingsWindow()
        {
            InitializeComponent();
            chatBots = Program.Settings.ChatBots;
            if (chatBots != null && chatBots.Count > 0)
            {
                comboBox1.Items.AddRange(chatBots.Select(x => x.DisplayName).ToArray());
                var index = chatBots.FindIndex(x => x.Equals(Program.Settings.LastUsedChatBot));

                if (index != -1)
                {
                    comboBox1.SelectedIndex = index;
                }
            }

            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChatBotSettingsWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(MessageBox.Show("Do you want to save the changes?", "Save Changes", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Program.Settings.LastUsedChatBot = chatBots[comboBox1.SelectedIndex];
                Program.Settings.ChatBots = chatBots;
            }
        }
    }
}
