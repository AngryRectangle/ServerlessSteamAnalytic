using HtmlAgilityPack;
using ServerlessSteamAnalytic.GameInfoModels;

namespace ServerlessSteamAnalytic.Parsers;

public class GamePageParser : IGameTitleParser
{
    public Title? ParseGameTitle(HtmlDocument html)
    {
        var titleNode = html.DocumentNode.SelectSingleNode("//div[@id='appHubAppName']");
        return titleNode is null ? null : new Title(titleNode.InnerText);
    }
}