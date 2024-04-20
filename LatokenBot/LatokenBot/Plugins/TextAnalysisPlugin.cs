using LatokenBot.Extensions;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.ChatCompletion;
using System.ComponentModel;
using System.Text.Json;

namespace LatokenBot.Plugins;

public class TextAnalysisPlugin
{
    //[KernelFunction]
    //[Description("/start, начальное сообщение")]
    //public static string Start(string query)
    //{
    //    var filePath = DetermineFilePath(query);
    //    var result = ReadFileContent(filePath);

    //    return result;
    //}

    [KernelFunction]
    [Description("Анализирует входящий запрос пользователя, чтобы определить, какой текстовый файл следует прочитать, и возвращает содержимое выбранного файла.")]
    public static string AnalyzeTextBasedOnQuery([Description("Запрос пользователя, который анализируется для определения соответствующего текстового файла для чтения. ()")] string query)
    {
        var filePath = DetermineFilePath(query);
        var result = ReadFileContent(filePath);

        return result;
    }

    private static string DetermineFilePath(string query)
    {
        query = query.ToLower();

        if (query.Contains("about"))
        {
            return @"C:\Users\DotNotFact\Desktop\LatokenBot\LatokenBot\Training\about.txt";
        }
        else if (query.Contains("hackathon"))
        {
            return @"C:\Users\DotNotFact\Desktop\LatokenBot\LatokenBot\Training\hackaton.txt";
        }
        else if (query.Contains("test"))
        {
            return @"C:\Users\DotNotFact\Desktop\LatokenBot\LatokenBot\Training\test.txt";
        }
        else if (query.Contains("hard"))
        {
            return @"C:\Users\DotNotFact\Desktop\LatokenBot\LatokenBot\Training\hard.txt";
        }
        return @"C:\Users\DotNotFact\Desktop\LatokenBot\LatokenBot\Training\about.txt";
    }

    private static string ReadFileContent(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !File.Exists(filePath))
            return "Requested content is not available or the file does not exist.";

        return File.ReadAllText(filePath);
    }
}


//// ML
//public class ModelLoader
//{
//    private static readonly MLContext _mlContext = new MLContext();
//    private readonly ITransformer _model;

//    public ModelLoader(string modelPath)
//    {
//        // Загрузка модели
//        DataViewSchema modelSchema;
//        _model = _mlContext.Model.Load(modelPath, out modelSchema);
//    }

//    public PredictionEngine<InputModel, OutputModel> GetPredictionEngine()
//    {
//        // Создание PredictionEngine для использования модели
//        return _mlContext.Model.CreatePredictionEngine<InputModel, OutputModel>(_model);
//    }
//}

//public class TextAnalysisMLPlugin
//{
//    private PredictionEngine<InputModel, OutputModel> _predictionEngine;

//    public TextAnalysisMLPlugin(string modelPath)
//    {
//        var loader = new ModelLoader(modelPath);
//        _predictionEngine = loader.GetPredictionEngine();
//    }

//    public string AnalyzeText(string text)
//    {
//        var input = new InputModel { Text = text };
//        var result = _predictionEngine.Predict(input);
//        return result.Prediction ? "Positive sentiment detected!" : "Negative sentiment detected!";
//    }
//}

//class InputModel
//{
//    public string Text { get; set; }
//}

//class OutputModel
//{
//    [ColumnName("PredictedLabel")]
//    public bool Prediction { get; set; }
//}