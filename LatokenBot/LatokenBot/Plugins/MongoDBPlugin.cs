using LatokenBot.Services.Actions;
using Microsoft.SemanticKernel;
using MongoDB.Bson;
using MongoDB.Driver;
using System.ComponentModel;

namespace LatokenBot.Plugins;

internal class MongoDBPlugin(RagAgentService _dataRetrievalService)
{
    //[KernelFunction]
    //// Need write promt Retrieves information beyond the AI's immediate knowledge by synthesizing data into precise summaries. Excelling at interpreting natural language queries and extracting fundamental insights from multiple sources.
    ////[Description("Retrieves information beyond the AI's immediate knowledge by synthesizing data into precise summaries. Excelling at interpreting natural language queries and extracting fundamental insights from multiple sources.")]
    //[Description("Retrieves information beyond the AI's immediate knowledge by synthesizing data into precise summaries. Excelling at interpreting natural language queries and extracting fundamental insights from multiple sources.")]
    //public async Task<string> RetrieveDataFromQDrantAsync(string query)
    //{
    //    return await _dataRetrievalService.RetrieveDataAsync(query);
    //}

    //[KernelFunction]
    //[Description("Archives specified information into the knowledge database. This method expects pre-processed input where date information, if present in the content, is identified using a Large Language Model (LLM) before invocation. If no date is extracted, the current date is used.")]
    //public async Task<bool> ArchiveInformationAsync(
    //    [Description("Indicates the date and time the information is deemed current or relevant, extracted from the data using an LLM. If no date is provided or extracted, the current system date and time are used.")] DateTime dateTime,
    //    [Description("The primary content or information to be archived. This should be pre-processed to extract date information using an LLM if relevant for the context.")] string data,
    //    [Description("A concise summary of the data, utilized to create an embedding for similarity checks to prevent storing similar or duplicate information, thus maintaining database integrity.")] string summary)
    //{
    //    return await _dataRetrievalService.ArchiveInformationAsync(dateTime, data, summary);
    //}

    //[KernelFunction]
    //[Description("Retrieves data and generates response using RAG")]
    //public async Task<string> RetrieveAndGenerateAsync(string query)
    //{
    //    var collection = _database.GetCollection<BsonDocument>("your_collection");
    //    var filter = Builders<BsonDocument>.Filter.Text(query);  // Simple text search
    //    var documents = await collection.Find(filter).ToListAsync();

    //    // Assuming you have a method to transform documents to vectors and generate responses
    //    var response = GenerateResponseUsingRag(documents);
    //    return response;
    //}

    private string GenerateResponseUsingRag(List<BsonDocument> documents)
    {
        // Transform data to vectors, call RAG, etc.
        return "Generated response based on retrieved documents.";
    }
}