using System;

namespace HyPlayer.Interfaces.Views;

public interface IPageService
{
    Type GetPageType(string key);
}