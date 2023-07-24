using Microsoft.UI.Xaml.Controls;
using System;

namespace HyPlayer.App.Contract.Services;

public interface IPageService
{
    Type GetPageType(string key);
}