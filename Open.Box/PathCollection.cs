namespace Open.Box;

public class PathCollection
{
    [JsonPropertyName("total_count")]
    public int TotalCount { get; set; }
    [JsonPropertyName("entries")]
    public List<PathEntry> Entries { get; set; }
}

public class PathEntry
{
    [JsonPropertyName("type")]
    public string Type { get; set; }
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("sequence_id")]
    public string SequenceId { get; set; }
    [JsonPropertyName("etag")]
    public string Etag { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
