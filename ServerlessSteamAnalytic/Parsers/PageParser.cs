using System.Text.RegularExpressions;
using HtmlAgilityPack;
using ServerlessSteamAnalytic.GameInfoModels;

namespace ServerlessSteamAnalytic.Parsers;

public class PageParser : ITitleParser, IPublicationDateParser, IAllReviewsParser
{
    public Title? ParseGameTitle(HtmlDocument html)
    {
        var titleNode = html.DocumentNode.SelectSingleNode("//div[@id='appHubAppName']");
        return titleNode is null ? null : new Title(titleNode.InnerText);
    }

    public PublicationDate? ParsePublicationDate(HtmlDocument html)
    {
        var releaseDateNode = html.DocumentNode
            .SelectSingleNode("//div[@class='release_date']")
            ?.SelectSingleNode("//div[@class='date']");

        return releaseDateNode is null ? null : new PublicationDate(releaseDateNode.InnerText);
    }

    public Reviews? ParseAllReviews(HtmlDocument htmlDocument)
    {
        var node = htmlDocument.DocumentNode.SelectNodes("//div[@class='user_reviews_summary_row']");
        if (node is null || node.Count == 0)
            return null;

        var allReviewsNode = node[1];
        var type = GetType(allReviewsNode);
        var shortDescription
            = allReviewsNode.SelectSingleNode(allReviewsNode.XPath +
                                              "//span[contains(@class, 'responsive_reviewdesc_short')]");
        if (shortDescription is null || type == Reviews.Type.NoReviews)
            return new Reviews(Reviews.Type.NoReviews, 0, 0);

        if (type == Reviews.Type.NotEnough)
        {
            var text = allReviewsNode.SelectSingleNode("//span[contains(@class, 'game_review_summary')]").InnerText;
            var count = int.Parse(Regex.Match(text, @"([\d]+)*").Groups[1].Value);
            return new Reviews(type, count, 0);
        }
        var match = Regex.Match(shortDescription.InnerText, @"\(([\d]+)% of ([\d]+)");
        var positivePercentage = int.Parse(match.Groups[1].Value);
        var totalCount = int.Parse(match.Groups[2].Value);
        return new Reviews(type, totalCount, positivePercentage);
    }

    private Reviews.Type GetType(HtmlNode reviewsNode)
    {
        var typeNode = reviewsNode.SelectSingleNode("//span[contains(@class, 'game_review_summary')]");
        if (typeNode is null)
            return Reviews.Type.NoReviews;
        if (typeNode.GetAttributeValue("class", string.Empty).Contains("not_enough_reviews"))
            return Reviews.Type.NotEnough;
        return Reviews.GetType(typeNode.InnerText);
    }
}