using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapHelper.Models
{
    internal class Soap
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string NextAction { get; set; }
        public StatusEnum Status { get; set; }
        public enum StatusEnum
        {
            None,
            PendingOnCustomer,
            PendingOnSupportEngineer,
            PendingOnProductGroup,
            OnHold,
            ReadyToClose,
            Closed
        }

        public List<BaseSoapItem>? Items { get; set; }

        public class BaseSoapItem
        {
            public DateTime DateTime { get; set; }
            public string Description { get; set; }
        }

        public class Communication : BaseSoapItem
        {
            public CommunicationEnum Type { get; set; }
            public enum CommunicationEnum
            {
                None,
                Email,
                Phone,
                Meeting,
                Chat
            }
        }
    }
}
