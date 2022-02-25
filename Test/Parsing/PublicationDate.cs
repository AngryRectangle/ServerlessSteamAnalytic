using System;
using HtmlAgilityPack;
using NUnit.Framework;
using ServerlessSteamAnalytic.Parsers;

namespace Test.Parsing;

public class PublicationDate
{
    [Test]
    public void HollowNightDate() => TestDate(Resources.Hollow_Knight, new DateOnly(2017, 2, 24));

    [Test]
    public void KingdomfallDate() => TestDate(Resources.Kingdomfall, "TBD");

    [Test]
    public void SpaceRiftDate() => TestDate(Resources.Space_Rift, new DateOnly(2028, 1, 3));

    [Test]
    public void NoDate()
    {
        var parser = new PageParser();
        var doc = new HtmlDocument();
        var parsed = parser.ParsePublicationDate(doc);
        Assert.Null(parsed, "Date == null");
    }

    private void TestDate(string html, DateOnly date)
    {
        var parser = new PageParser();
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        
        var parsed = parser.ParsePublicationDate(doc);
        Assert.NotNull(parsed, "Title != null");
        Assert.True(parsed.IsValidDate, "parsed.IsValidDate");
        Assert.AreEqual(date, parsed.Date);
    }
    
    private void TestDate(string html, string date)
    {
        var parser = new PageParser();
        var doc = new HtmlDocument();
        doc.LoadHtml(html);
        
        var parsed = parser.ParsePublicationDate(doc);
        Assert.NotNull(parsed, "Title != null");
        Assert.False(parsed.IsValidDate, "parsed.IsValidDate");
        Assert.AreEqual(date, parsed.StringDate);
    }
}