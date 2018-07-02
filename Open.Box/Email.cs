using System.Runtime.Serialization;

namespace Open.Box
{
    [DataContract]
    public class Email
    {
        [DataMember(Name = "access")]
        public string Access { get; set; }
        [DataMember(Name = "email")]
        public string Value { get; set; }
    }
}
