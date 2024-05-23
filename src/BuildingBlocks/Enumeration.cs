using System.Reflection;

namespace BuildingBlocks;

public abstract class Enumeration<TEnum, TVal> : IEquatable<Enumeration<TEnum, TVal>>
    where TEnum : Enumeration<TEnum, TVal>
    where TVal : notnull
{
    private static readonly Dictionary<TVal, TEnum> Enumerations = CreateEnumerations();

    protected Enumeration(TVal value, string name)
    {
        Value = value;
        Name = name;
    }

    public TVal Value { get; protected init; }

    public string Name { get; protected init; }

    public static TEnum? FromValue(TVal value)
    {
        return Enumerations.TryGetValue(
            value,
            out TEnum? enumeration) ?
                enumeration :
                default;
    }

    public static TEnum? FromName(string name)
    {
        return Enumerations
            .Values
            .SingleOrDefault(e => e.Name == name);
    }

    public bool Equals(Enumeration<TEnum, TVal>? other)
    {
        if (other is null)
        {
            return false;
        }

        return GetType() == other.GetType() &&
               Value.Equals(other.Value);
    }

    public override bool Equals(object? obj)
    {
        return obj is Enumeration<TEnum, TVal> other &&
               Equals(other);
    }

    public override int GetHashCode()
    {
        return Value.GetHashCode();
    }

    public override string ToString()
    {
        return Name;
    }

    private static Dictionary<TVal, TEnum> CreateEnumerations()
    {
        var enumerationType = typeof(TEnum);

        var fieldsForType = enumerationType
            .GetFields(
                BindingFlags.Public |
                BindingFlags.Static |
                BindingFlags.FlattenHierarchy)
            .Where(fieldInfo =>
                enumerationType.IsAssignableFrom(fieldInfo.FieldType))
            .Select(fieldInfo =>
                (TEnum)fieldInfo.GetValue(default)!);

        return fieldsForType.ToDictionary(x => x.Value);
    }
}