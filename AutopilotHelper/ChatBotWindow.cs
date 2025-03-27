using AutopilotHelper.Chat;
using AutopilotHelper.Const;
using AutopilotHelper.Controllers;
using ChatGPT.Net.DTO.ChatGPT;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutopilotHelper
{
    public partial class ChatBotWindow : Form
    {
        public readonly string Log;
        public readonly string ConversationId;
        public readonly string Prompt;
        public ChatGptRequest Request { get; private set; }

        public bool isProcessing
        {
            get => _isProcessing;

            set
            {
                if (value)
                {
                    Cursor = Cursors.WaitCursor;
                }
                else
                {
                    Cursor = Cursors.Default;
                }

                _isProcessing = value;

                SetInteractiveUIState(value);
            }
        }
        private bool _isProcessing = false;

        public ChatBotWindow()
        {
            InitializeComponent();
        }

        public ChatBotWindow(string log, string conversationId)
        {
            Log = log;
            ConversationId = conversationId;
            InitializeComponent();
        }

        private void SetInteractiveUIState(bool isProcessing)
        {
            button1.Enabled = !isProcessing;
            richTextBox2.ReadOnly = isProcessing;
        }

        public void DisplayChatMessage(string role, string content)
        {
            StringBuilder sb = new();
            sb.AppendLine($"{role} {DateTime.Now} :");
            sb.AppendLine(content);
            richTextBox1.AppendText(sb.ToString());
            richTextBox1.AppendText(Environment.NewLine);
        }

        public void InitializeConversation()
        {
            if (ChatGPTController.Api.GetConversation(ConversationId) != null)
            {
                var choice = MessageBox.Show("Initiailize will discard the current active conversation.\n\nProceed?", "WARN", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (choice == DialogResult.No)
                {
                    return;
                }

                ChatGPTController.Api.RemoveConversation(ConversationId);
            }

            var conversation = new ChatGptConversation
            {
                Messages = new List<ChatGptMessage>
                {
                    new ChatGptMessage
                    {
                        Role = ChatGPTConst.Role.System,
                        Content = ChatGPTConst.Prompt.SupportEngineer
                    }
                }
            };

            ChatGPTController.Api.SetConversation(ConversationId, conversation);
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ChatBotSettingsWindow();
            form.ShowDialog(this);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(richTextBox2.Text))
            {
                return;
            }

            DisplayChatMessage(ChatGPTConst.Role.User, richTextBox2.Text);

            isProcessing = true;
        }

        private void OnReceiveResponse(string message)
        {
            DisplayChatMessage(ChatGPTConst.Role.System, message);
            isProcessing = false;
        }

        private async void SendMessage(ChatGptMessage message)
        {
            var conversation = ChatGPTController.Api.GetConversation(ConversationId);

            if(conversation == null)
            {
                InitializeConversation();
                conversation = ChatGPTController.Api.GetConversation(ConversationId);
            }



            var response = await ChatGPTController.Api.Ask(message.Content, ConversationId);

            OnReceiveResponse(response);
        }
    }
}
