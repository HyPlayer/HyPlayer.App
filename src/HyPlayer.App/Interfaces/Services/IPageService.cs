using System;

namespace HyPlayer.Interfaces.Services;

public interface IPageService
{
    Type GetPageType(string key);
}
