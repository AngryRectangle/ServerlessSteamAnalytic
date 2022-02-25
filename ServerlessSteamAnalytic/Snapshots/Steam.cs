using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace ServerlessSteamAnalytic.Snapshots;

public class Steam : ISteam
{
    private readonly HttpClient _client;

    public Steam(HttpClient client)
    {
        _client = client;
    }

    public async Task<string> LoadGamePage(int id)
    {
        if (id <= 0)
            throw new ArgumentOutOfRangeException(nameof(id), "Game id must be greater then 0");
        
        var url = GetGamePageUrl(id);
        var response = await _client.GetAsync(url);
        if (response.StatusCode == HttpStatusCode.Redirect)
            throw new GameNotFoundException();

        if (response.StatusCode != HttpStatusCode.OK)
            throw new WebException($"Failed to load game page for id {id} with status code {response.StatusCode}");
        return await response.Content.ReadAsStringAsync();
    }

    private string GetGamePageUrl(int id) => $"https://store.steampowered.com/app/{id}/";
}