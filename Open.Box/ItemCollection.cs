using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Open.Box
{
    [DataContract]
    public class ItemCollection
    {
        [DataMember(Name = "total_count")]
        public int TotalCount { get; set; }
        [DataMember(Name = "entries")]
        public List<Item> Entries { get; set; }
        [DataMember(Name = "offset")]
        public int Offset { get; set; }
        [DataMember(Name = "limit")]
        public int Limit { get; set; }
    }
}
