using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutopilotHelper.Models;
using Newtonsoft.Json;

namespace AutopilotHelper.Utilities
{
    public class AutopilotUtil
    {
        public MDMFileUtil MDMDiag => _MDMDiag;
        private MDMFileUtil _MDMDiag;

        public AutopilotUtil(MDMFileUtil file)
        {
            _MDMDiag = file;

            GetLocalAutopilotProfileStatus();
        }

        public AutopilotDDSZTD? GetLocalAutopilotProfileStatus()
        {
            return JsonConvert.DeserializeObject<AutopilotDDSZTD>(File.ReadAllText(Path.Combine(MDMDiag.TmpWorkplacePath, "AutopilotDDSZTDFile.json")));
        }

        public List<EventViewerFile.Record> GetCloudSessionHostRecords()
        {
            EventViewerFile file = new(Path.Combine(MDMDiag.TmpWorkplacePath, 
                "microsoft-windows-shell-core-operational.evtx"));

            return file.records
                .Where(search => !string.IsNullOrEmpty(search.FormatDescription) 
                && search.FormatDescription.StartsWith("CloudExperienceHost"))
                .OrderByDescending(search => search.TimeCreated)
                .ToList();
        }
    }
}
