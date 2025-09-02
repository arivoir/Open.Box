namespace Open.Box;

public class Comment
{
    [JsonPropertyName("id")]
    public string Id { get; set; }
    
    [JsonPropertyName("is_reply_comment")]
    public bool IsReplyComment { get; set; }
    
    [JsonPropertyName("message")]
    public string Message { get; set; }
    
    [JsonPropertyName("created_by")]
    public User CreatedBy { get; set; }
    
    [JsonPropertyName("created_at")]
    public string CreatedAt { get; set; }
    
    [JsonPropertyName("item")]
    public Item Item { get; set; }
    
    [JsonPropertyName("modified_at")]
    public string ModifiedAt { get; set; }
}

public class CommentsCollection
{
    [JsonPropertyName("total_count")]
    public string TotalCount { get; set; }
    
    [JsonPropertyName("entries")]
    public List<Comment> Entries { get; set; }
    
    [JsonPropertyName("offset")]
    public string Offset { get; set; }
    
    [JsonPropertyName("limit")]
    public string Limit { get; set; }
}
