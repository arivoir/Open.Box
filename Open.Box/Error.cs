namespace Open.Box;

public class Error
{
    [JsonPropertyName("code")]
    public string Code { get; set; }
    
    [JsonPropertyName("message")]
    public string Message { get; set; }
    
    [JsonPropertyName("type")]
    public string Type { get; set; }
    
    [JsonPropertyName("status")]
    public int Status { get; set; }
    
    [JsonPropertyName("help_url")]
    public string HelpUrl { get; set; }
    
    [JsonPropertyName("request_id")]
    public string RequestId { get; set; }
}
