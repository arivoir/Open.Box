namespace Open.Box;

public class User
{
    //[DataMember(Name = "type")]
    //public string Type { get; set; }
    [JsonPropertyName("id")]
    public string Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    //[DataMember(Name = "login")]
    //public string Login { get; set; }
    //[DataMember(Name = "created_at")]
    //public string CreatedAt { get; set; }
    //[DataMember(Name = "modified_at")]
    //public string ModifiedAt { get; set; }
    //[DataMember(Name = "role")]
    //public string Role { get; set; }
    //[DataMember(Name = "language")]
    //public string Language { get; set; }
    [JsonPropertyName("space_amount")]
    public long SpaceAmount { get; set; }
    [JsonPropertyName("space_used")]
    public long SpaceUsed { get; set; }
    [JsonPropertyName("max_upload_size")]
    public long MaxUploadSize { get; set; }
    ////[DataMember(Name = "tracking_codes")]
    ////public string TrackingCodes { get; set; }
    //[DataMember(Name = "can_see_managed_users")]
    //public bool CanSeeManagedUsers { get; set; }
    //[DataMember(Name = "is_sync_enabled")]
    //public bool IsSyncEnabled { get; set; }
    //[DataMember(Name = "status")]
    //public string Status { get; set; }
    //[DataMember(Name = "job_title")]
    //public string JobTitle { get; set; }
    //[DataMember(Name = "phone")]
    //public string Phone { get; set; }
    //[DataMember(Name = "address")]
    //public string Address { get; set; }
    //[DataMember(Name = "avatar_url")]
    //public string AvatarUrl { get; set; }
    //[DataMember(Name = "is_exempt_from_device_limits")]
    //public bool IsExemptFromDeviceLimits { get; set; }
    //[DataMember(Name = "is_exempt_from_login_verification")]
    //public bool IsExemptFromLoginVerification { get; set; }
    //[DataMember(Name = "enterprise")]
    //public Enterprise Enterprise { get; set; }
}

//[DataContract]
//public class Enterprise
//{
//    [DataMember(Name = "type")]
//    public string Type { get; set; }
//    [DataMember(Name = "id")]
//    public string Id { get; set; }
//    [DataMember(Name = "name")]
//    public string Name { get; set; }
//}
