using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace AutopilotHelper.Models
{
    public class EventViewerFile
    {
        public readonly List<Record> records = new();

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
                        records.Add(new()
                        {
                            TimeCreated = record.TimeCreated,
                            LevelDisplayName = record.LevelDisplayName,
                            FormatDescription = record.FormatDescription()
                        });
                        //Console.WriteLine("{0} {1}: {2}", record.TimeCreated, record.LevelDisplayName, record.FormatDescription());
                    }
                }
            }
        }

        public struct Record
        {
            public DateTime? TimeCreated;
            public string LevelDisplayName;
            public string FormatDescription;
        }
    }
}
