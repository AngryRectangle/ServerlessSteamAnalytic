using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using HtmlAgilityPack;
using ServerlessSteamAnalytic.GameInfoModels;

namespace ServerlessSteamAnalytic.Parsers;

public class PageParser : ITitleParser, IPublicationDateParser, IAllReviewsParser, IRecentReviewsParsers
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
        var nodes = GetReviewsNodes(htmlDocument);
        if (nodes is null || nodes.Count == 0)
            return null;

        return ParseReviews(nodes.Count < 2 ? nodes[0] : nodes[1]);
    }

    public Reviews? ParseRecentReviews(HtmlDocument htmlDocument)
    {
        var nodes = GetReviewsNodes(htmlDocument);
        if (nodes is null || nodes.Count < 2)
            return null;

        return ParseReviews(nodes[0]);
    }

    private IList<HtmlNode>? GetReviewsNodes(HtmlDocument document)
        => document.DocumentNode.SelectNodes("//div[@id='userReviews']/div[@class='user_reviews_summary_row']");

    private Reviews? ParseReviews(HtmlNode? node)
    {
        if (node is null)
            return null;

        var reviewSummaryNode = node.SelectSingleNode(node.XPath + "//span[contains(@class, 'game_review_summary')]");
        if (reviewSummaryNode is null)
            return new Reviews(Reviews.Type.NoReviews, 0, 0);

        if (reviewSummaryNode.GetAttributeValue("class", string.Empty).Contains("not_enough_reviews"))
        {
            var count = int.Parse(Regex.Match(reviewSummaryNode.InnerText, @"([\d]+)*").Groups[1].Value);
            return new Reviews(Reviews.Type.NotEnough, count, 0);
        }

        var type = Reviews.GetType(reviewSummaryNode.InnerText);
        var shortDescription
            = node.SelectSingleNode(node.XPath + "//span[contains(@class, 'responsive_reviewdesc')]");
        if (shortDescription is null)
            return new Reviews(Reviews.Type.NoReviews, 0, 0);

        var match = Regex.Match(shortDescription.InnerText, @"([\d]+)%[\D]+([\d]+,?[\d]*).*");
        var positivePercentage = int.Parse(match.Groups[1].Value);
        var totalCount = int.Parse(match.Groups[2].Value, NumberStyles.AllowThousands, CultureInfo.InvariantCulture);
        return new Reviews(type, totalCount, positivePercentage);
    }
}