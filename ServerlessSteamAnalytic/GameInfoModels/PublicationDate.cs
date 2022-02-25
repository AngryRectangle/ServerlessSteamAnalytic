using System;

namespace ServerlessSteamAnalytic.GameInfoModels;

public class PublicationDate
{
    private readonly DateOnly _date;
    public bool IsValidDate { get; }
    public string StringDate { get; }

    public DateOnly? Date => IsValidDate ? _date : null;
    public PublicationDate(string date)
    {
        StringDate = date;
        IsValidDate = DateOnly.TryParse(date, out _date);
    }
}