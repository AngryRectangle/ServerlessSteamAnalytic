using HtmlAgilityPack;
using ServerlessSteamAnalytic.GameInfoModels;

namespace ServerlessSteamAnalytic.Parsers;

public interface IGameTitleParser
{
    Title? ParseGameTitle(HtmlDocument htmlDocument);
}