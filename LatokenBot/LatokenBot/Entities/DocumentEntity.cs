using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LatokenBot.Entities;

public class DocumentEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}