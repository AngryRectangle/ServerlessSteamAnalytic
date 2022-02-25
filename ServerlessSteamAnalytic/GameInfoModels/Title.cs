namespace ServerlessSteamAnalytic.GameInfoModels;

public class Title
{
    private readonly string _title;

    public Title(string title)
    {
        _title = title;
    }
    
    public static implicit operator string(Title title) => title._title;
}