namespace Open.Box;

public class Item
{
    [JsonPropertyName("type")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Type { get; set; }
    
    [JsonPropertyName("id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Id { get; set; }
    
    [JsonPropertyName("sequence_id")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string SequenceId { get; set; }
    
    [JsonPropertyName("etag")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Etag { get; set; }
    
    [JsonPropertyName("sha1")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Sha1 { get; set; }
    
    [JsonPropertyName("name")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Name { get; set; }
    
    [JsonPropertyName("created_at")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string CreatedAt { get; set; }
    
    [JsonPropertyName("description")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string Description { get; set; }
    
    [JsonPropertyName("size")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public long Size { get; set; }
    
    [JsonPropertyName("path_collection")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public PathCollection PathCollection { get; set; }
    
    [JsonPropertyName("created_by")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public User CreatedBy { get; set; }
    
    [JsonPropertyName("modified_by")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public User ModifiedBy { get; set; }
    
    [JsonPropertyName("owned_by")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public User OwnedBy { get; set; }
    
    [JsonPropertyName("shared_link")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Link SharedLink { get; set; }
    
    [JsonPropertyName("folder_upload_email")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Email FolderUploadEmail { get; set; }
    
    [JsonPropertyName("parent")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public Item Parent { get; set; }
    
    [JsonPropertyName("item_status")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string ItemStatus { get; set; }
    
    [JsonPropertyName("item_collection")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public ItemCollection ItemCollection { get; set; }
}
