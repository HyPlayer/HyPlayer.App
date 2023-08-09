using System;

namespace HyPlayer.App.Interfaces.Views;

public interface IPageService
{
    Type GetPageType(string key);
}