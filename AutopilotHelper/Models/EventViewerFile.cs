using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;

namespace AutopilotHelper.Models
{
    public class EventViewerFile
    {
        public string FileName => _FileName;
        private string _FileName;
        public readonly List<Record> records = new();

        public EventViewerFile(string fileName)
        {
            EventLog eventLog = new EventLog();

            using (var reader = new EventLogReader(fileName, PathType.FilePath))
            {
                EventRecord record;
                var index = 0;
                while ((record = reader.ReadEvent()) != null)
                {
                    using (record)
                    {
                        records.Add(new()
                        {
                            Index = index++,
                            Id = record.Id,
                            TimeCreated = record.TimeCreated,
                            LevelDisplayName = record.LevelDisplayName,
                            FormatDescription = record.FormatDescription(),
                            OpcodeDisplayName = record.OpcodeDisplayName,
                            MachineName = record.MachineName,
                            LogName = record.LogName,
                            ProviderName = record.ProviderName,
                            XmlContent = record.ToXml()
                        });
                        //Console.WriteLine("{0} {1}: {2}", record.TimeCreated, record.LevelDisplayName, record.FormatDescription());
                    }
                }
            }

            _FileName = fileName;
        }

        public struct Record
        {
            public int Index;
            public int Id;
            public DateTime? TimeCreated;
            public string LevelDisplayName;
            public string FormatDescription;
            public string OpcodeDisplayName;
            public string MachineName;
            public string LogName;
            public string ProviderName;

            public string XmlContent;

            public override string ToString()
            {
                return $@"Id: {Id},
TimeCreated: {TimeCreated.ToString()},
LevelDisplayName: {LevelDisplayName},
FormatDescription: {FormatDescription},
OpcodeDisplayName: {OpcodeDisplayName},
MachineName: {MachineName},
LogName: {LogName},
ProviderName: {ProviderName}";
            }
        }
    }
}
