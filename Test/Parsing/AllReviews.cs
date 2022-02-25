using NUnit.Framework;
using ServerlessSteamAnalytic.GameInfoModels;
using ServerlessSteamAnalytic.Parsers;
using static System.String;

namespace Test.Parsing;

public class AllReviews
{
    [Test]
    public void ZeroAllReviews()
    {
        var parser = new PageParser();
        var doc = Helper.LoadHtml(Resources.Elden_Ring);
        var parsed = parser.ParseAllReviews(doc);
        Assert.NotNull(parsed, "Reviews != null");
        Assert.AreEqual(Reviews.Type.NoReviews, parsed.ReviewType);
        Assert.AreEqual(0, parsed.Count);
    }
    
    [Test]
    public void NotEnoughAllReviews()
    {
        var parser = new PageParser();
        var doc = Helper.LoadHtml(Resources.Texture_Assembler);
        var parsed = parser.ParseAllReviews(doc);
        Assert.NotNull(parsed, "Reviews != null");
        Assert.AreEqual(Reviews.Type.NotEnough, parsed.ReviewType);
        Assert.AreEqual(1, parsed.Count);
    }
    
    [Test]
    public void MatchAllReviews()
    {
        var parser = new PageParser();
        var doc = Helper.LoadHtml(Resources.Many_Buttons_to_Press);
        var parsed = parser.ParseAllReviews(doc);
        Assert.NotNull(parsed, "Reviews != null");
        Assert.AreEqual(Reviews.Type.MostlyNegative, parsed.ReviewType);
        Assert.AreEqual(11, parsed.Count);
        Assert.AreEqual(36, parsed.PositivePercentage);
    }
    
    [Test]
    public void NullAllReviews()
    {
        var parser = new PageParser();
        var doc = Helper.LoadHtml(Empty);
        var parsed = parser.ParseAllReviews(doc);
        Assert.Null(parsed, "Reviews == null");
    }
}