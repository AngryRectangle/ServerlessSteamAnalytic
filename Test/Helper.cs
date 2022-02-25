using HtmlAgilityPack;

namespace Test;

public static class Helper
{
    public static HtmlDocument LoadHtml(string htmlString)
    {
        var doc = new HtmlDocument();
        doc.LoadHtml(htmlString);
        return doc;
    }
}