using MsgReader.Outlook;
using HtmlAgilityPack;
using System.Windows.Forms;

namespace SoapHelper
{
    public partial class StartUpForm : Form
    {
        public StartUpForm()
        {
            InitializeComponent();
        }

        private void openOutlookMSGToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openMsgDialog1.ShowDialog();
            var fileName = openMsgDialog1.FileName;

            if(string.IsNullOrEmpty(fileName))
            {
                return;
            }

            var msg = new MsgReader.Outlook.Storage.Message(fileName);
            var html = new HtmlAgilityPack.HtmlDocument();
            html.LoadHtml(msg.BodyHtml);
            var msgBody = html.DocumentNode.InnerText.Trim();

            richTextBox1.Text = msgBody;

            var msgSubject = msg.Subject;

            var msgSender = msg.Sender;

            var msgTO = msg.GetEmailRecipients(RecipientType.To, false, false);

            var msgCC = msg.GetEmailRecipients(RecipientType.Cc, false, false);
        }
    }
}
