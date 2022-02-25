using System;

namespace ServerlessSteamAnalytic.Services;

public class DefaultTimeProvider : ITimeProvider
{
    public DateTime Now => DateTime.Now;
}