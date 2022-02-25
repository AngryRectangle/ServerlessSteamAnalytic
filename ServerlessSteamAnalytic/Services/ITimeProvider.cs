using System;

namespace ServerlessSteamAnalytic.Services;

public interface ITimeProvider
{
    DateTime Now { get; }
}