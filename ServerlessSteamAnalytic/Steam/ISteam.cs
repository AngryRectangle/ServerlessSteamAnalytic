using System.Threading.Tasks;

namespace ServerlessSteamAnalytic.Steam;

public interface ISteam
{
    Task<string> LoadGamePage(int id);
}