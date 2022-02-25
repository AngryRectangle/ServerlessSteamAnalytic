using HtmlAgilityPack;
using ServerlessSteamAnalytic.GameInfoModels;

namespace ServerlessSteamAnalytic.Parsers;

public interface ITitleParser
{
    Title? ParseGameTitle(HtmlDocument htmlDocument);
}