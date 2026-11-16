using MongoDB.Bson.Serialization.Attributes;

namespace EventHub.Models;

[BsonIgnoreExtraElements]
public class EventGroupDocument
{
    [BsonId]
    public int Id { get; set; }

    [BsonElement("owner")]
    public int Owner { get; set; }

    [BsonElement("event_id")]
    public int EventId { get; set; }

    [BsonElement("name")]
    public string Name { get; set; } = string.Empty;

    [BsonElement("description")]
    public string Description { get; set; } = string.Empty;

    [BsonElement("created_at")]
    public string CreatedAt { get; set; } = string.Empty;

    [BsonElement("max_members")]
    public int MaxMembers { get; set; }
}