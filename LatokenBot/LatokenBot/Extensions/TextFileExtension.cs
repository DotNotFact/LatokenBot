using System.Collections.Concurrent;
using System.Text;

namespace LatokenBot.Extensions;

internal class TextFileExtension
{
    public static string[] GetTrainingDocuments(string[] trainingFilePaths)
    {
        //string[] trainingFilePaths =
        //    [
        //        @"C:\Users\DotNotFact\Desktop\Git Project\LatokenBot\LatokenBot\LatokenBot\Training\rules.txt",
        //        @"C:\Users\DotNotFact\Desktop\Git Project\LatokenBot\LatokenBot\LatokenBot\Training\about.txt",
        //        @"C:\Users\DotNotFact\Desktop\Git Project\LatokenBot\LatokenBot\LatokenBot\Training\hackaton.txt",
        //        @"C:\Users\DotNotFact\Desktop\Git Project\LatokenBot\LatokenBot\LatokenBot\Training\test.txt",
        //        @"C:\Users\DotNotFact\Desktop\Git Project\LatokenBot\LatokenBot\LatokenBot\Training\hard.txt",
        //    ];

        var documents = new ConcurrentBag<string>();

        Parallel.ForEach(trainingFilePaths, filePath =>
        {
            try
            {
                if (File.Exists(filePath))
                {
                    using var reader = new StreamReader(filePath, Encoding.UTF8);
                    documents.Add(reader.ReadToEnd());
                }
                else
                {
                    Console.WriteLine($"Файл не найден: {filePath}");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Ошибка при чтении файла {filePath}: {e.Message}");
            }
        });

        return [.. documents];
    }
}