using Microsoft.KernelMemory.AI;
using MongoDB.Bson;
using MongoDB.Driver;

public class DataRetrievalService(IMongoClient client, ITextEmbeddingGenerator textEmbeddingGenerator)
{
    //private readonly ITextEmbeddingGenerator _embeddingGenerator = textEmbeddingGenerator;
    //private readonly IMongoClient _client = client;
    //private readonly string _indexName = "your-index-name1";

    //public async Task<string> RetrieveDataAsync(string query, double minRelevance = 0.85)
    //{
    //    var collection = _client.GetDatabase("documents");
    //    var queryVector = await _embeddingGenerator.GenerateEmbeddingAsync(query);
    //    var filter = Builders<BsonDocument>.Filter.Empty; // Дополнительная логика фильтрации, если необходимо

    //    var documents = await collection.Find(filter).ToListAsync();

    //    foreach (var document in documents)
    //    {
    //        // Расчет релевантности может быть реализован здесь
    //    }

    //    return "Данные получены"; // Возврат результата
    //}

    //public async Task<bool> ArchiveInformationAsync(string data, string summary)
    //{
    //    var collection = _client.GetDatabase("documents").GetCollection<BsonDocument>("documents");
    //    var summaryVector = await _embeddingGenerator.GenerateEmbeddingAsync(summary);

    //    var newDocument = new BsonDocument
    //    {
    //        { "data", data },
    //        { "summary", summary },
    //        { "summaryVector", new BsonArray(summary) },
    //        { "dateTime", DateTime.Now }
    //    };

    //    await collection.InsertOneAsync(newDocument);
    //    Console.WriteLine($"Данные успешно сохранены: {data} - {summary}");

    //    return true;
    //}
}