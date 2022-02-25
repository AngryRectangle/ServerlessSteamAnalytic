using System;
using ServerlessSteamAnalytic.GameInfoModels;

namespace ServerlessSteamAnalytic.Snapshots;

public class GameReviewSnapshot
{
    public int GameId { get; }
    public DateTime Timestamp { get; }
    public Reviews.Type ReviewsType { get; }
    public int TotalReviewCount { get; }
    public int PositivePercentage { get; }

    public GameReviewSnapshot(
        int gameId,
        DateTime timestamp,
        Reviews.Type reviewsType,
        int totalReviewCount,
        int positivePercentage)
    {
        GameId = gameId;
        Timestamp = timestamp;
        ReviewsType = reviewsType;
        TotalReviewCount = totalReviewCount;
        PositivePercentage = positivePercentage;
    }

    public override string ToString()
        => $"GameId: {GameId}, Time: {Timestamp}, Type: {ReviewsType}, Count: {TotalReviewCount}, Positive: {PositivePercentage}%";

    protected bool Equals(GameReviewSnapshot other)
    {
        return GameId == other.GameId && Timestamp.Equals(other.Timestamp) && ReviewsType == other.ReviewsType &&
               TotalReviewCount == other.TotalReviewCount && PositivePercentage == other.PositivePercentage;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((GameReviewSnapshot)obj);
    }
}