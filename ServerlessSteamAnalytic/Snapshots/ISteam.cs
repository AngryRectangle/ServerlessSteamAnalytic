using System.Threading.Tasks;

namespace ServerlessSteamAnalytic.Snapshots;

public interface ISteam
{
    Task<string> LoadGamePage(int id);
}