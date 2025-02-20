using MsgReader.Outlook;
using HtmlAgilityPack;
using System.Windows.Forms;
using Newtonsoft.Json;
using ChatGPT.Net;
using System.Text;
using SoapHelper.Models;

namespace SoapHelper
{
    public partial class StartUpForm : Form
    {
        public const string HtmlContentType = "text/html; charset=utf-8";
        
        public readonly ChatGpt ChatGpt = new ChatGpt(string.Empty, new ChatGPT.Net.DTO.ChatGPT.ChatGptOptions()
        {
            BaseUrl = "http://localhost:1234",
            Model = "qwen",
            MaxTokens = 4096,
        });

        public StartUpForm()
        {
            InitializeComponent();
        }

        private async void openOutlookMSGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openMsgDialog1.ShowDialog();
            var fileName = openMsgDialog1.FileName;

            if(string.IsNullOrEmpty(fileName))
            {
                return;
            }

            var reader = new MsgReader.Reader();
            var msg = new MsgReader.Outlook.Storage.Message(fileName);
            //var tmpPath = Path.Combine(Path.GetTempPath(), $"IntunePremier/SoapHelper/{msg.ConversationTopic}");
            //if(Directory.Exists(tmpPath) == false)
            //{
            //    Directory.CreateDirectory(tmpPath);
            //}

            //reader.ExtractToFolder(fileName, tmpPath);
           
            var html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(reader.ExtractMsgEmailBody(msg, MsgReader.ReaderHyperLinks.Both, HtmlContentType));
            var msgBody = html.DocumentNode.InnerText.Trim();

            richTextBox1.Text = msgBody;

            var msgSubject = msg.Subject;

            var msgSender = msg.Sender;

            var msgTO = msg.GetEmailRecipients(RecipientType.To, false, false);

            var msgCC = msg.GetEmailRecipients(RecipientType.Cc, false, false);

            var body = await ParseEmailThreadIntoJson(msgBody);

            richTextBox2.Text = JsonConvert.SerializeObject(body);
        }

        private async Task<EmailThread?> ParseEmailThreadIntoJson(string msgBody)
        {
            var thread = new EmailThread()
            {
                Messages = new List<EmailMsg>()
                {
                    new EmailMsg()
                    {
                        Subject = "Subject",
                        Topic = "Topic",
                        Sender = "Sender",
                        Body = "Body",
                        To = new List<string>() { "To" },
                        CC = new List<string>() { "CC" },
                    }
                }
            };

            var exampleJson = JsonConvert.SerializeObject(thread);

            ChatGpt.SetConversationSystemMessage("jsonParser", "You are JSON Parser. You should turn my plain text message into plain JSON body text by the provided JSON example and no Markdown.\n" +
                $"{exampleJson}");

            var json = await ChatGpt.Ask(msgBody, "jsonParser");

            thread = JsonConvert.DeserializeObject<EmailThread>(json);

            return thread;
        }
    }
}
