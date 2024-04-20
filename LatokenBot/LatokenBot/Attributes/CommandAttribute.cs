namespace LatokenBot.Attributes;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
public class CommandAttribute : Attribute
{
    public string Name { get; private set; }

    public bool IsRegex { get; private set; }

    public CommandAttribute(string name)
    {
        Name = name;
    }

    public CommandAttribute(string name, bool isRegex)
    {
        Name = name;
        IsRegex = isRegex;
    }
}

//[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
//public sealed class KernelFunctionAttribute(string name) : Attribute
//{
//    public string Name { get; } = name;
//}

//[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
//public sealed class DescriptionAttribute(string text) : Attribute
//{
//    public string Text { get; } = text;
//}