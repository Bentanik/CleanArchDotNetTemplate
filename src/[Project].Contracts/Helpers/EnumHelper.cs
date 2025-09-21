namespace _Project_.Contracts.Helpers;

public static class EnumHelper
{
    /// <summary>
    /// Automatically maps between two enums with the same value names.
    /// </summary>
    public static TTarget ToEnum<TSource, TTarget>(this TSource source)
        where TSource : Enum
        where TTarget : Enum
    {
        var sourceName = source.ToString();

        if (Enum.TryParse(typeof(TTarget), sourceName, ignoreCase: true, out var result))
            return (TTarget)result!;

        throw new ArgumentException(
            $"Cannot map enum {typeof(TSource).Name}.{sourceName} to {typeof(TTarget).Name}"
        );
    }

    /// <summary>
    /// Automatically maps between two nullable enums with the same value names.
    /// Returns null if the source is null.
    /// </summary>
    public static TTarget? ToEnumNullable<TSource, TTarget>(this TSource? source)
        where TSource : struct, Enum
        where TTarget : struct, Enum
    {
        if (source == null) return null;

        var sourceName = source.Value.ToString();

        if (Enum.TryParse<TTarget>(sourceName, ignoreCase: true, out var result))
            return result;

        throw new ArgumentException(
            $"Cannot map enum {typeof(TSource).Name}.{sourceName} to {typeof(TTarget).Name}"
        );
    }

    /// <summary>
    /// Gets the Description attribute of an enum value, or the enum name if no Description is set.
    /// </summary>
    public static string GetDescription(this Enum value)
    {
        var member = value.GetType().GetMember(value.ToString());
        var attr = member.FirstOrDefault()?.GetCustomAttribute<DescriptionAttribute>();

        return attr?.Description ?? value.ToString();
    }
}