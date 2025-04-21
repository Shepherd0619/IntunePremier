using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutopilotHelper.DiagnosticsCollection.Models
{
    internal struct UpdateService
    {
        public string Name { get; set; }
        public bool IsRegisteredWithAU { get; set; }
        public string ServiceUrl { get; set; }
        public bool IsDefaultAUService { get; set; }
    }
}
