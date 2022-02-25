using HtmlAgilityPack;
using ServerlessSteamAnalytic.GameInfoModels;

namespace ServerlessSteamAnalytic.Parsers;

public interface IRecentReviewsParsers
{
    Reviews? ParseRecentReviews(HtmlDocument htmlDocument);
}