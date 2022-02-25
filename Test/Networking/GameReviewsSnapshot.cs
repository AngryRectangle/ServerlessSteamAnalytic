using System;
using System.Net.Http;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using ServerlessSteamAnalytic.GameInfoModels;
using ServerlessSteamAnalytic.Parsers;
using ServerlessSteamAnalytic.Services;
using ServerlessSteamAnalytic.Snapshots;

namespace Test.Networking;

public class GameReviewsSnapshot
{
    private const int Destiny2Id = 1085660;
    private static readonly DateTime TestTime = new DateOnly(2022, 1, 1).ToDateTime(default);

    [Test]
    public async Task Destiny2()
    {
        var timeProvider = Mock.Of<ITimeProvider>(i => i.Now == TestTime);
        var steam = new Steam(new HttpClient(new HttpMockHandler()));
        var parser = new PageParser();
        var snapshooter = new GameReviewSnapshooter(steam, timeProvider, parser);
        var snapshot = await snapshooter.MakeSnapshot(Destiny2Id);
        var expectedSnapshot = new GameReviewSnapshot(Destiny2Id, TestTime, Reviews.Type.VeryPositive, 447780, 84);
        Assert.AreEqual(expectedSnapshot, snapshot);
    }
}