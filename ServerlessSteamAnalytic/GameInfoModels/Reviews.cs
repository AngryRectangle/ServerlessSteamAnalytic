using System;

namespace ServerlessSteamAnalytic.GameInfoModels;

public class Reviews
{
    public Type ReviewType { get; }
    public int Count { get; }
    public int PositivePercentage { get; }

    public Reviews(Type type, int count, int positivePercentage)
    {
        ReviewType = type;
        Count = count;
        PositivePercentage = positivePercentage;
    }

    public static Type GetType(string stringType)
    {
        return stringType.ToLower() switch
        {
            "overhwelmingly positive" => Type.OverhwelminglyPositive,
            "very positive" => Type.VeryPositive,
            "positive" => Type.Positive,
            "mostly positive" => Type.MostlyPositive,
            "mixed" => Type.Mixed,
            "mostly negative" => Type.MostlyNegative,
            "negative" => Type.Negative,
            "very negative" => Type.VeryNegative,
            "overhwelmingly negative" => Type.OverhwelminglyNegative,
            _ => throw new ArgumentOutOfRangeException()
        };
    }
    
    public enum Type
    {
        NoReviews,
        NotEnough,
        OverhwelminglyPositive,
        VeryPositive,
        Positive,
        MostlyPositive,
        Mixed,
        MostlyNegative,
        Negative,
        VeryNegative,
        OverhwelminglyNegative
    }
}