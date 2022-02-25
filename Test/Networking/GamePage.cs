using System;
using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using ServerlessSteamAnalytic.Steam;

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

    private class HttpMockHandler : HttpMessageHandler
    {
        protected override Task<HttpResponseMessage> SendAsync(
            HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            if (request.RequestUri is null)
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));

            var match = Regex.Match(request.RequestUri.AbsoluteUri, @"https:\/\/store.steampowered.com\/app\/([\d]+)\/");
            if (match.Groups.Count < 2)
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound));

            var id = int.Parse(match.Groups[1].Value);
            var html = HtmlByGameId(id);
            if (html is null)
                return Task.FromResult(new HttpResponseMessage(HttpStatusCode.Redirect));

            var result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StringContent(html);
            return Task.FromResult(result);
        }

        private string? HtmlByGameId(int id)
        {
            return id switch
            {
                1245620 => Resources.Elden_Ring,
                367520 => Resources.Hollow_Knight,
                _ => null
            };
        }
    }
}