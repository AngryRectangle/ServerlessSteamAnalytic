using System.Net;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Test;

public class HttpMockHandler : HttpMessageHandler
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
            1085660 => Resources.Destiny_2,
            _ => null
        };
    }
}