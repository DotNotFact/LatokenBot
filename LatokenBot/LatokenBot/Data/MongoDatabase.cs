using MongoDB.Driver;

namespace LatokenBot.Data;

internal class MongoDatabase(IMongoClient client)
{
    private readonly IMongoClient _client = client;
    private const string DataBaseName = "latoken_db";

    public IMongoDatabase GetDatabase() => _client.GetDatabase(DataBaseName);
}
