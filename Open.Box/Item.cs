using System.Runtime.Serialization;

namespace Open.Box
{
    [DataContract]
    public class Item
    {
        [DataMember(Name = "type", EmitDefaultValue=false)]
        public string Type { get; set; }
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public string Id { get; set; }
        [DataMember(Name = "sequence_id", EmitDefaultValue = false)]
        public string SequenceId { get; set; }
        [DataMember(Name = "etag", EmitDefaultValue = false)]
        public string Etag { get; set; }
        [DataMember(Name = "sha1", EmitDefaultValue = false)]
        public string Sha1 { get; set; }
        [DataMember(Name = "name", EmitDefaultValue = false)]
        public string Name { get; set; }
        [DataMember(Name = "created_at", EmitDefaultValue = false)]
        public string CreatedAt { get; set; }
        [DataMember(Name = "description", EmitDefaultValue = false)]
        public string Description { get; set; }
        [DataMember(Name = "size", EmitDefaultValue = false)]
        public long Size { get; set; }
        [DataMember(Name = "path_collection", EmitDefaultValue = false)]
        public PathCollection PathCollection { get; set; }
        [DataMember(Name = "created_by", EmitDefaultValue = false)]
        public User CreatedBy { get; set; }
        [DataMember(Name = "modified_by", EmitDefaultValue = false)]
        public User ModifiedBy { get; set; }
        [DataMember(Name = "owned_by", EmitDefaultValue = false)]
        public User OwnedBy { get; set; }
        [DataMember(Name = "shared_link", EmitDefaultValue = false)]
        public Link SharedLink { get; set; }
        [DataMember(Name = "folder_upload_email", EmitDefaultValue = false)]
        public Email FolderUploadEmail { get; set; }
        [DataMember(Name = "parent", EmitDefaultValue = false)]
        public Item Parent { get; set; }
        [DataMember(Name = "item_status", EmitDefaultValue = false)]
        public string ItemStatus { get; set; }
        [DataMember(Name = "item_collection", EmitDefaultValue = false)]
        public ItemCollection ItemCollection { get; set; }
    }
}
