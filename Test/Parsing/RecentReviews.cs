using NUnit.Framework;
using ServerlessSteamAnalytic.GameInfoModels;
using ServerlessSteamAnalytic.Parsers;

namespace Test.Parsing;

public class RecentReviews
{
    [Test]
    public void ZeroReviews()
    {
        var parser = new PageParser();
        var doc = Helper.LoadHtml(Resources.Elden_Ring);
        var parsed = parser.ParseRecentReviews(doc);
        Assert.Null(parsed, "Reviews == null");
    }
    
    [Test]
    public void ZeroRecentReviews()
    {
        var parser = new PageParser();
        var doc = Helper.LoadHtml(Resources.Many_Buttons_to_Press);
        var parsed = parser.ParseRecentReviews(doc);
        Assert.Null(parsed, "Reviews == null");
    }
    
    [Test]
    public void MatchReviewsDestiny2()
    {
        var parser = new PageParser();
        var doc = Helper.LoadHtml(Resources.Destiny_2);
        var parsed = parser.ParseRecentReviews(doc);
        Assert.NotNull(parsed, "Reviews != null");
        Assert.AreEqual(Reviews.Type.VeryPositive, parsed.ReviewType);
        Assert.AreEqual(9710, parsed.Count);
        Assert.AreEqual(81, parsed.PositivePercentage);
    }
    
    [Test]
    public void NullReviews()
    {
        var parser = new PageParser();
        var doc = Helper.LoadHtml(string.Empty);
        var parsed = parser.ParseAllReviews(doc);
        Assert.Null(parsed, "Reviews == null");
    }
}