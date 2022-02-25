using HtmlAgilityPack;
using NUnit.Framework;
using ServerlessSteamAnalytic.Parsers;

namespace Test.Parsing;

public class TitleParsing
{
    [Test]
    public void HollowNightTitle() => TestTitle(Resources.Hollow_Knight, "Hollow Knight");

    [Test]
    public void EldenRingTitle() => TestTitle(Resources.Elden_Ring, "ELDEN RING");

    [Test]
    public void Destiny2Title() => TestTitle(Resources.Destiny_2, "Destiny 2");

    [Test]
    public void SpaceRiftTitle() => TestTitle(Resources.Space_Rift, "");

    [Test]
    public void NoTitle()
    {
        var parser = new GamePageParser();
        var doc = new HtmlDocument();
        var parsed = parser.ParseGameTitle(doc);
        Assert.Null(parsed, "Title == null");
    }

    private void TestTitle(string html, string title)
    {
        var parser = new GamePageParser();
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        
        var parsed = parser.ParseGameTitle(doc);
        Assert.NotNull(parsed, "Title != null");
        Assert.AreEqual(title, (string) parsed);
    }
}