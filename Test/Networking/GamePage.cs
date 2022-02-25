using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using ServerlessSteamAnalytic.Snapshots;

namespace Test.Networking;

public class GamePage
{
    private const int EldenRingId = 1245620;

    [Test]
    public async Task SuccessLoadingTest()
    {
        var steam = new Steam(CreateMockedHttpClient());
        var htmlCode = await steam.LoadGamePage(EldenRingId);
        Assert.AreEqual(Resources.Elden_Ring, htmlCode);
    }

    [Test]
    public void InvalidIdException()
    {
        var steam = new Steam(CreateMockedHttpClient());
        Assert.ThrowsAsync<ArgumentOutOfRangeException>(() => steam.LoadGamePage(0));
    }

    [Test]
    public void GameNotFoundException()
    {
        var steam = new Steam(CreateMockedHttpClient());
        Assert.ThrowsAsync<GameNotFoundException>(() => steam.LoadGamePage(1));
    }

    private HttpClient CreateMockedHttpClient() => new(new HttpMockHandler());
}