using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutopilotHelper.Const;
using ChatGPT.Net;

namespace AutopilotHelper.Controllers
{
    public static class ChatGPTController
    {
        public static ChatGpt Api { get; set; }

        public static async Task<string> RegeneratePreviousMsg(this ChatGpt api, string conversationId)
        {
            var conversation = api.GetConversation(conversationId);
            if(conversation == null)
            {
                return string.Empty;
            }

            var messages = conversation.Messages;
            if(messages == null || messages.Count <= 0)
            {
                return string.Empty;
            }

            var lastMessage = messages.Last();

            if(lastMessage == null || lastMessage.Role != ChatGPTConst.Role.System)
            {
                return string.Empty;
            }

            messages.Remove(lastMessage);

            api.SetConversation(conversationId, conversation);

            return await api.Ask(conversation.Messages[0].Content, conversationId);
        }
    }
}
