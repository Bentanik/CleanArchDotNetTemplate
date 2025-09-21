namespace _Project_.Contracts.Attributes;

[AttributeUsage(AttributeTargets.Field)]
sealed class MessageAttribute(string message, string code) : Attribute
{
    public string Message { get; } = message;
    public string Code { get; } = code;
}

public static class MessageAttributeExtension
{
    public static (string Message, string Code) GetMessage<T>(this T messageCode) where T : Enum
    {
        var type = typeof(T);
        var field = type.GetField(messageCode.ToString());

        var attribute = (MessageAttribute?)field?.GetCustomAttributes(typeof(MessageAttribute), false).FirstOrDefault();
        return attribute != null ? (attribute.Message, attribute.Code) : (string.Empty, string.Empty);
    }

    public static (string Message, string Code) GetMessageByCode<T>(string code) where T : Enum
    {
        var type = typeof(T);
        foreach (var field in type.GetFields())
        {
            var attribute = (MessageAttribute?)field.GetCustomAttributes(typeof(MessageAttribute), false).FirstOrDefault();
            if (attribute != null && attribute.Code == code)
            {
                return (attribute.Message, attribute.Code);
            }
        }
        return (string.Empty, string.Empty);
    }
}