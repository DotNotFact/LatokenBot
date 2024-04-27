using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace LatokenBot.Entities;

internal class ChatHistoryEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string Message { get; set; } = string.Empty;
    public DateTime? TimeMessage { get; set; }
}
