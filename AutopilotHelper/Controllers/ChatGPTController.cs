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

        public static async Task<bool> RegeneratePreviousMsg(this ChatGpt api, string conversationId)
        {
            var conversation = api.GetConversation(conversationId);
            if(conversation == null)
            {
                return false;
            }

            var messages = conversation.Messages;
            if(messages == null || messages.Count <= 0)
            {
                return false;
            }

            var lastMessage = messages.Last();

            if(lastMessage == null || lastMessage.Role != ChatGPTConst.Role.System)
            {
                return false;
            }

            messages.Remove(lastMessage);



            return true;
        }
    }
}
