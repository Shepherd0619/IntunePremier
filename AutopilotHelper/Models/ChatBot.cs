using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutopilotHelper.Models
{
    public struct ChatBot
    {
        public string DisplayName;
        public string ApiAddress;
        public string ApiKey;
        public string ModelName;

        public override bool Equals(object? obj)
        {
            return obj is ChatBot bot &&
                   DisplayName == bot.DisplayName &&
                   ApiAddress == bot.ApiAddress &&
                   ApiKey == bot.ApiKey &&
                   ModelName == bot.ModelName;
        }
    }
}
