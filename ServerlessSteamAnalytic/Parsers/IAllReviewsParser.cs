using HtmlAgilityPack;
using ServerlessSteamAnalytic.GameInfoModels;

namespace ServerlessSteamAnalytic.Parsers;

public interface IAllReviewsParser
{
    Reviews? ParseAllReviews(HtmlDocument htmlDocument);
}