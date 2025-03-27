using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutopilotHelper.Const;
using ChatGPT.Net;
using ChatGPT.Net.DTO.ChatGPT;

namespace AutopilotHelper.Controllers
{
    public static class ChatGPTController
    {
        public static ChatGpt Api {
            get
            {
                if(_Api == null)
                {
                    InitializeApi();
                }

                return _Api;
            }
        }
        private static ChatGpt _Api;
        public static ChatGptOptions Options { get; private set; }

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

        public static void InitializeApi()
        {
            // TODO: 需要换成用户设置

            Options = new ChatGptOptions()
            {
                BaseUrl = "http://localhost:1234",
                Model = "qwen"
            };

            _Api = new ChatGpt(string.Empty, Options);
        }
    }
}
