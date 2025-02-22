using MsgReader.Outlook;
using HtmlAgilityPack;
using System.Windows.Forms;
using Newtonsoft.Json;
using ChatGPT.Net;
using System.Text;
using SoapHelper.Models;
using SoapHelper.Utilities;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Tab;
using DarkModeForms;
using ChatGPT.Net.DTO.ChatGPT;

namespace SoapHelper
{
    public partial class StartUpForm : Form
    {
        public const string HtmlContentType = "text/html; charset=utf-8";

        public ChatGptOptions GptOptions = new ChatGPT.Net.DTO.ChatGPT.ChatGptOptions()
        {
            BaseUrl = "http://localhost:1234",
            Model = "qwen",
            MaxTokens = 4096,
        };

        public readonly ChatGpt ChatGpt = new ChatGpt(string.Empty, new ChatGPT.Net.DTO.ChatGPT.ChatGptOptions()
        {
            BaseUrl = "http://localhost:1234",
            Model = "qwen",
            MaxTokens = 4096,
        });

        public EmailThread? CurrentEmailThread;

        public StartUpForm()
        {
            var dm = new DarkModeCS(this)
            {
                ColorMode = DarkModeCS.DisplayMode.SystemDefault
            };

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

        private async Task<EmailThread?> ParseEmailThread(List<string> msgBody)
        {
            var thread = new EmailThread();

            var exampleJson = JsonConvert.SerializeObject(new EmailMsg()
            {
                Subject = "Subject",
                //Topic = "Topic",
                Summary = "Summary",
                Sender = "Sender",
                //Body = "Body",
                To = new List<string>() { "To" },
                CC = new List<string>() { "CC" },
            });

            //var conversation = new ChatGptConversation()
            //{
            //    Id = "jsonParser",
            //    Messages = new List<ChatGptMessage>()
            //    {
            //        new ChatGptMessage()
            //        {
            //            Role = ChatGptConst.Role.System,
            //            Content = "You are JSON Parser. You should turn all my messages into plain JSON body text by the provided JSON example and no Markdown.\n" +
            //                $"{exampleJson}"
            //        }
            //    }
            //};

            //foreach (var item in msgBody)
            //{
            //    conversation.Messages.Add(new ChatGptMessage()
            //    {
            //        Role = ChatGptConst.Role.User,
            //        Content = item
            //    });
            //}

            //var response = await ChatGpt.SendMessage(new ChatGptRequest
            //{
            //    Messages = conversation.Messages,
            //    Model = GptOptions.Model,
            //    Stream = false,
            //    Temperature = GptOptions.Temperature,
            //    TopP = GptOptions.TopP,
            //    FrequencyPenalty = GptOptions.FrequencyPenalty,
            //    PresencePenalty = GptOptions.PresencePenalty,
            //    Stop = GptOptions.Stop,
            //    MaxTokens = GptOptions.MaxTokens
            //});

            ChatGpt.ResetConversation("jsonParser");
            ChatGpt.SetConversationSystemMessage("jsonParser", "You are JSON Parser. You should turn my messages into plain JSON body text by the provided JSON example and no Markdown.\n" +
                $"{exampleJson}");

            var messages = new List<EmailMsg>();
            foreach (var item in msgBody)
            {
                var msg = await ChatGpt.Ask(item, "jsonParser").ContinueWith(t => JsonConvert.DeserializeObject<EmailMsg>(t.Result));

                if (msg == null || string.IsNullOrWhiteSpace(msg.Summary)) continue;

                messages.Add(msg);
            }

            thread.Messages = messages;

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

            try
            {
                var soap = await GenerateSoap(CurrentEmailThread);

                richTextBox3.Text = JsonConvert.SerializeObject(soap);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error when generating SOAP", MessageBoxButtons.OK, MessageBoxIcon.Error);
                SoapGenerateBtn.Enabled = true;
                toolStripStatusLabel1.Text = "SOAP JSON generation failed!";
                return;
            }

            SoapGenerateBtn.Enabled = true;

            toolStripStatusLabel1.Text = "SOAP JSON generated.";
        }

        private async void EmailThreadGenerateBtn_Click(object sender, EventArgs e)
        {
            EmailThreadJsonGenerateBtn.Enabled = false;
            toolStripStatusLabel1.Text = "Generating Email Thread JSON...";

            var msgTextList = StringUtil.SplitStringIntoChunks(richTextBox1.Text, 4096);

            try
            {
                var thread = await ParseEmailThread(msgTextList);

                CurrentEmailThread = thread;
                richTextBox2.Text = JsonConvert.SerializeObject(thread);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error when generating Email Thread", MessageBoxButtons.OK, MessageBoxIcon.Error);
                EmailThreadJsonGenerateBtn.Enabled = true;
                toolStripStatusLabel1.Text = "Email Thread JSON generation failed!";
                return;
            }

            EmailThreadJsonGenerateBtn.Enabled = true;
            SoapGenerateBtn.Enabled = true;

            toolStripStatusLabel1.Text = "Email Thread JSON generated.";
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new AboutBox();
            form.ShowDialog();
        }
    }
}
