using System.Runtime.Serialization;

namespace Open.Box
{
    [DataContract]
    public class Error
    {
        [DataMember(Name = "code")]
        public string Code { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "status")]
        public int Status { get; set; }
        [DataMember(Name = "help_url")]
        public string HelpUrl { get; set; }
        [DataMember(Name = "request_id")]
        public string RequestId { get; set; }
    }
}
