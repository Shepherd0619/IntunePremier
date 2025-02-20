using MsgReader.Outlook;
using HtmlAgilityPack;
using System.Windows.Forms;
using Newtonsoft.Json;
using ChatGPT.Net;
using System.Text;
using SoapHelper.Models;
using SoapHelper.Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;

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

        public EmailThread? CurrentEmailThread;

        public StartUpForm()
        {
            InitializeComponent();
        }

        private void openOutlookMSGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openMsgDialog1.ShowDialog();
            var fileName = openMsgDialog1.FileName;

            if (string.IsNullOrEmpty(fileName))
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

            EmailThreadJsonGenerateBtn.Enabled = true;

            toolStripStatusLabel1.Text = "MSG file loaded.";
        }

        private async Task<EmailThread?> ParseEmailThread(string msgBody)
        {
            var thread = new EmailThread()
            {
                Messages = new List<EmailMsg>()
                {
                    new EmailMsg()
                    {
                        Subject = "Subject",
                        //Topic = "Topic",
                        Summary = "Summary",
                        Sender = "Sender",
                        //Body = "Body",
                        To = new List<string>() { "To" },
                        CC = new List<string>() { "CC" },
                    }
                }
            };

            var exampleJson = JsonConvert.SerializeObject(thread);
            ChatGpt.ResetConversation("jsonParser");
            ChatGpt.SetConversationSystemMessage("jsonParser", "You are JSON Parser. You should turn my email thread into plain JSON body text by the provided JSON example and no Markdown.\n" +
                $"{exampleJson}");

            var json = await ChatGpt.Ask(msgBody, "jsonParser");

            thread = JsonConvert.DeserializeObject<EmailThread>(json);

            return thread;
        }

        private async Task<Soap?> GenerateSoap(EmailThread thread)
        {
            var soap = new Soap()
            {
                Title = "Title",
                Description = "Description",
                NextAction = "NextAction",
                Status = Soap.StatusEnum.None,
                Items = new List<Soap.BaseSoapItem>()
                {
                    new Soap.Communication()
                    {
                        DateTime = DateTime.Now,
                        Description = "Description",
                        Type = Soap.Communication.CommunicationEnum.None
                    }
                }
            };

            var exampleJson = JsonConvert.SerializeObject(soap);
            ChatGpt.ResetConversation("soapGenerator");
            ChatGpt.SetConversationSystemMessage("soapGenerator", "You are JSON Parser.\n" +
                "1. You should turn my email thread JSON body into SOAP JSON body by the provided JSON example and no Markdown.\n" +
                $"{exampleJson}\n" +
                $"2. The Type attribute inside Communication has {EnumUtil.GetEnumValuesAsString(typeof(Soap.Communication.CommunicationEnum))}\n" +
                $"3. The Status attribute inside Soap has {EnumUtil.GetEnumValuesAsString(typeof(Soap.StatusEnum))}\n" +
                $"4. You should write SOAP from {textBox1.Text}'s perspective.");

            var json = await ChatGpt.Ask(JsonConvert.SerializeObject(thread), "soapGenerator");

            soap = JsonConvert.DeserializeObject<Soap>(json);

            return soap;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private async void SoapGenerateBtn_Click(object sender, EventArgs e)
        {
            if (CurrentEmailThread == null)
            {
                MessageBox.Show("Please generate Email Thread first.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; 
            }

            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                MessageBox.Show("Please provide the name of the person who is writing the SOAP.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SoapGenerateBtn.Enabled = false;
            toolStripStatusLabel1.Text = "Generating SOAP JSON...";

            var soap = await GenerateSoap(CurrentEmailThread);

            richTextBox3.Text = JsonConvert.SerializeObject(soap);

            SoapGenerateBtn.Enabled = true;

            toolStripStatusLabel1.Text = "SOAP JSON generated.";
        }

        private async void EmailThreadGenerateBtn_Click(object sender, EventArgs e)
        {
            EmailThreadJsonGenerateBtn.Enabled = false;
            toolStripStatusLabel1.Text = "Generating Email Thread JSON...";

            var thread = await ParseEmailThread(richTextBox1.Text);

            CurrentEmailThread = thread;
            richTextBox2.Text = JsonConvert.SerializeObject(thread);

            EmailThreadJsonGenerateBtn.Enabled = true;
            SoapGenerateBtn.Enabled = true;

            toolStripStatusLabel1.Text = "Email Thread JSON generated.";
        }
    }
}
