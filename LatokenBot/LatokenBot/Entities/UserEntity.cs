using Microsoft.SemanticKernel.ChatCompletion;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace LatokenBot.Entities;

internal class UserEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public long TelegramId { get; set; }

    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;

    public ICollection<ChatHistoryEntity>? ChatHistory { get; set; } = [];

    public string FullName => $"{FirstName} {LastName}";
}