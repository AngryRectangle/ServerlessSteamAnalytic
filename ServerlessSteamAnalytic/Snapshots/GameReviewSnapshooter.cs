using System.Threading.Tasks;
using HtmlAgilityPack;
using ServerlessSteamAnalytic.GameInfoModels;
using ServerlessSteamAnalytic.Parsers;
using ServerlessSteamAnalytic.Services;

namespace ServerlessSteamAnalytic.Snapshots;

public class GameReviewSnapshooter
{
    private readonly Steam _steam;
    private readonly ITimeProvider _timeProvider;
    private readonly PageParser _pageParser;

    public GameReviewSnapshooter(Steam steam, ITimeProvider timeProvider, PageParser pageParser)
    {
        _steam = steam;
        _timeProvider = timeProvider;
        _pageParser = pageParser;
    }

    public async Task<GameReviewSnapshot> MakeSnapshot(int gameId)
    {
        var html = await _steam.LoadGamePage(gameId);
        var document = new HtmlDocument();
        document.LoadHtml(html);
        var allReviews = _pageParser.ParseAllReviews(document) ?? new Reviews(Reviews.Type.NoReviews, 0, 0);
        return new GameReviewSnapshot(gameId, _timeProvider.Now, allReviews.ReviewType, allReviews.Count,
            allReviews.PositivePercentage);
    }
}