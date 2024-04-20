using System.Text;

namespace LatokenBot.Extensions;

internal class TextFileExtension
{
    public static string GetTraining()
    {
        var combinedContent = new StringBuilder();

        string[] trainingfilePaths = [
            @"C:\Users\DotNotFact\Desktop\LatokenBot\LatokenBot\Training\about.txt",
            @"C:\Users\DotNotFact\Desktop\LatokenBot\LatokenBot\Training\hackaton.txt",
            @"C:\Users\DotNotFact\Desktop\LatokenBot\LatokenBot\Training\test.txt",
            @"C:\Users\DotNotFact\Desktop\LatokenBot\LatokenBot\Training\hard.txt",
        ];

        foreach (string filePath in trainingfilePaths)
        {
            if (!File.Exists(filePath))
                Console.WriteLine($"Файл не найден: {filePath}");

            using var reader = new StreamReader(filePath, Encoding.UTF8);
            combinedContent.AppendLine(reader.ReadToEnd());
        }

        return combinedContent.ToString();
    }
}