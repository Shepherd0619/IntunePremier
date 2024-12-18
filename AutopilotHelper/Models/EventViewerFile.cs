using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutopilotHelper.Models
{
    public class EventViewerFile
    {
        public readonly List<EventRecord> records = new();

        public EventViewerFile(string fileName)
        {
            EventLog eventLog = new EventLog();

            using (var reader = new EventLogReader(fileName, PathType.FilePath))
            {
                EventRecord record;
                while ((record = reader.ReadEvent()) != null)
                {
                    using (record)
                    {
                        records.Add(record);
                        Console.WriteLine("{0} {1}: {2}", record.TimeCreated, record.LevelDisplayName, record.FormatDescription());
                    }
                }
            }
        }
    }
}
