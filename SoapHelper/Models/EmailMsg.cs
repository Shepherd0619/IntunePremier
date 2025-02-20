using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoapHelper.Models
{
    public class EmailMsg
    {
        public string Subject { get; set; }
        public string Summary { get; set; }
        public string Sender { get; set; }
        //public string Body { get; set; }
        public List<string> To { get; set; }
        public List<string> CC { get; set; }
        public DateTime DateTime { get; set; }
    }
}
