﻿using Microsoft.SemanticKernel;
using System.ComponentModel;

namespace LatokenBot.Plugins;

internal class MongoDBPlugin(DataRetrievalService dataRetrievalService)
{
    private readonly DataRetrievalService _dataRetrievalService = dataRetrievalService;

    [KernelFunction]
    // Need write promt Retrieves information beyond the AI's immediate knowledge by synthesizing data into precise summaries. Excelling at interpreting natural language queries and extracting fundamental insights from multiple sources.
    //[Description("Retrieves information beyond the AI's immediate knowledge by synthesizing data into precise summaries. Excelling at interpreting natural language queries and extracting fundamental insights from multiple sources.")]
    [Description("Retrieves information beyond the AI's immediate knowledge by synthesizing data into precise summaries. Excelling at interpreting natural language queries and extracting fundamental insights from multiple sources.")]
    public async Task<string> RetrieveDataFromQDrantAsync(string query)
    {
        return Task.CompletedTask.ToString(); // _dataRetrievalService.RetrieveDataFromQDrantAsync(query);
    }

    [KernelFunction]
    [Description("Archives specified information into the knowledge database. This method expects pre-processed input where date information, if present in the content, is identified using a Large Language Model (LLM) before invocation. If no date is extracted, the current date is used.")]
    public async Task<bool> ArchiveInformationAsync(
        [Description("Indicates the date and time the information is deemed current or relevant, extracted from the data using an LLM. If no date is provided or extracted, the current system date and time are used.")] DateTime dateTime,
        [Description("The primary content or information to be archived. This should be pre-processed to extract date information using an LLM if relevant for the context.")] string data,
        [Description("A concise summary of the data, utilized to create an embedding for similarity checks to prevent storing similar or duplicate information, thus maintaining database integrity.")] string summary)
    {
        return Task.CompletedTask is null; // return _dataRetrievalService.ArchiveInformationAsync(dateTime, data, summary);
    }
}