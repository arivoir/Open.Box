using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Open.Box
{
    [DataContract]
    public class PathCollection
    {
        [DataMember(Name = "total_count")]
        public int TotalCount { get; set; }
        [DataMember(Name = "entries")]
        public List<PathEntry> Entries { get; set; }
    }

    [DataContract]
    public class PathEntry
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "sequence_id")]
        public string SequenceId { get; set; }
        [DataMember(Name = "etag")]
        public string Etag { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
