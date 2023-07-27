using System;
using Depository.Abstraction.Interfaces;
using Depository.Core;
using Depository.Extensions;
using HyPlayer.App.Interfaces.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HyPlayer.App.Interfaces.Views;

public abstract class AppPageBase<TViewModel> : Page, IDisposable
    where TViewModel : class, IScopedViewModel
{
    public static readonly DependencyProperty ViewModelProperty =
        DependencyProperty.Register(nameof(ViewModel), typeof(TViewModel), typeof(TViewModel), new PropertyMetadata(default));

    public TViewModel ViewModel
    {
        get => (TViewModel)GetValue(ViewModelProperty);
        set => SetValue(ViewModelProperty, value);
    }

    private readonly IDepositoryResolveScope _scope;

        
    protected AppPageBase()
    {
        _scope = DepositoryResolveScope.Create();
        ViewModel = App.GetDIContainer().ResolveInScope<TViewModel>(_scope);
        DataContext = ViewModel;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _scope.Dispose();
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}