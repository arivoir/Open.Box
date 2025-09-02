namespace Open.Box;

public class Email
{
    [JsonPropertyName("access")]
    public string Access { get; set; }
    
    [JsonPropertyName("email")]
    public string Value { get; set; }
}
