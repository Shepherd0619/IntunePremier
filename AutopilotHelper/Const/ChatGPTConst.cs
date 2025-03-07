using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutopilotHelper.Const
{
    public static class ChatGPTConst
    {
        public static class ConversationId
        {
            public const string Undefined = "Undefined";
            public const string EventViewer = "EventViewer";
            public const string RegViewer = "RegViewer";
        }

        public static class Role
        {
            public const string System = "system";
            public const string User = "user";
        }

        public static class Prompt
        {
            public const string SupportEngineer = "You are Microsoft Intune Support Engineer.";
            public const string EventVIewer = "You are Microsoft Intune Support Engineer. " +
                "I will provide a Event Viewer log list. You should answer my questions based on Event Viewer log and Microsoft Learn documentation.";
            public const string RegViewer = "You are Microsoft Intune Support Engineer. " +
                "I will provide a registry body. You should answer my questions based on registry and Microsoft Learn documentation.";
        }
    }
}
