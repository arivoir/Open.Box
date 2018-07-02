using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Open.Box
{
    [DataContract]
    public class Comment
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "is_reply_comment")]
        public bool IsReplyComment { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
        [DataMember(Name = "created_by")]
        public User CreatedBy { get; set; }
        [DataMember(Name = "created_at")]
        public string CreatedAt { get; set; }
        [DataMember(Name = "item")]
        public Item Item { get; set; }
        [DataMember(Name = "modified_at")]
        public string ModifiedAt { get; set; }
    }

    [DataContract]
    public class CommentsCollection
    {
        [DataMember(Name = "total_count")]
        public string TotalCount { get; set; }
        [DataMember(Name = "entries")]
        public List<Comment> Entries { get; set; }
        [DataMember(Name = "offset")]
        public string Offset { get; set; }
        [DataMember(Name = "limit")]
        public string Limit { get; set; }
    }
}
