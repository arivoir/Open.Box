using System.Runtime.Serialization;

namespace Open.Box
{
    [DataContract]
    public class Link
    {
        [DataMember(Name = "url")]
        public string Url { get; set; }
        [DataMember(Name = "download_url")]
        public string DownloadUrl { get; set; }
        //[DataMember(Name = "vanity_url")]
        //public string VanityUrl { get; set; }
        //[DataMember(Name = "is_password_enabled")]
        //public bool IsPasswordEnabled { get; set; }
        //[DataMember(Name = "unshared_at")]
        //public string UnsharedAt { get; set; }
        //[DataMember(Name = "download_count")]
        //public int DownloadCount { get; set; }
        //[DataMember(Name = "preview_count")]
        //public int PreviewCount { get; set; }
        //[DataMember(Name = "access")]
        //public string Access { get; set; }
        //[DataMember(Name = "permissions")]
        //public List<Permission> Permissions { get; set; }
    }

    //public class Permission
    //{
    //    [DataMember(Name = "can_download")]
    //    public bool CanDownload { get; set; }
    //    [DataMember(Name = "can_preview")]
    //    public bool CanPreview { get; set; }

    //}
}
