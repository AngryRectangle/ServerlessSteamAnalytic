using NUnit.Framework;
using ServerlessSteamAnalytic.GameInfoModels;
using ServerlessSteamAnalytic.Parsers;

namespace Test.Parsing;

public class ReviewType
{
    [Test]
    public void OverwhelminglyPositive()
        => TestReviewType(Resources.Hollow_Knight, Reviews.Type.OverhwelminglyPositive);

    [Test]
    public void VeryPositive() => TestReviewType(Resources.Destiny_2, Reviews.Type.VeryPositive);

    [Test]
    public void Positive() => TestReviewType(Resources.Dusk_Diver_2, Reviews.Type.Positive);

    [Test]
    public void MostlyPositive() => TestReviewType(Resources.Hokko_Life, Reviews.Type.MostlyPositive);

    [Test]
    public void Mixed() => TestReviewType(Resources.Thief_Simulator_VR, Reviews.Type.Mixed);

    [Test]
    public void MostlyNegative() => TestReviewType(Resources.Dead_by_Death, Reviews.Type.MostlyNegative);

    [Test]
    public void Negative() => TestReviewType(Resources.Dark_Blood, Reviews.Type.Negative);

    [Test]
    public void VeryNegative() => TestReviewType(Resources.New_Life, Reviews.Type.VeryNegative);

    [Test]
    public void OverhwelminglyNegative()
        => TestReviewType(Resources.Command_and_Conquer_4, Reviews.Type.OverhwelminglyNegative);

    private void TestReviewType(string html, Reviews.Type type)
    {
        var doc = Helper.LoadHtml(html);
        var parser = new PageParser();
        var reviews = parser.ParseAllReviews(doc);
        Assert.NotNull(reviews);
        Assert.AreEqual(type, reviews.ReviewType);
    }
}