using HtmlAgilityPack;
using ServerlessSteamAnalytic.GameInfoModels;

namespace ServerlessSteamAnalytic.Parsers;

public class PageParser : ITitleParser, IPublicationDateParser
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
}