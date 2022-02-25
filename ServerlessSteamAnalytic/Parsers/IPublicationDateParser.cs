using HtmlAgilityPack;
using ServerlessSteamAnalytic.GameInfoModels;

namespace ServerlessSteamAnalytic.Parsers;

public interface IPublicationDateParser
{
    PublicationDate? ParsePublicationDate(HtmlDocument htmlDocument);
}