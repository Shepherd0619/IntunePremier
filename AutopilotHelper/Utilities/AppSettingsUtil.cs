using AutopilotHelper.Models;

namespace AutopilotHelper.Utilities
{
    public class AppSettingsUtil
    {
        public List<string> RecentDiagFiles { get; set; }
        public List<ChatBot> ChatBots { get; set; }
        public ChatBot LastUsedChatBot { get; set; }

        public void Initialize()
        {
            RecentDiagFiles = new List<string>();
            ChatBots = new List<AutopilotHelper.Models.ChatBot>();
        }
    }
}
