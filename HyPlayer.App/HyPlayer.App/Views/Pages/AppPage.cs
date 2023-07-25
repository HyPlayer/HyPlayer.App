using Depository.Abstraction.Interfaces;
using Depository.Abstraction.Models.Options;
using Depository.Core;
using Depository.Extensions;
using HyPlayer.App.Interfaces.ViewModels;
using HyPlayer.App.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace HyPlayer.App.Views.Pages
{
    public class AppPage : Page
    {
        public static readonly DependencyProperty CoreViewModelProperty =
           DependencyProperty.Register(nameof(CoreViewModel), typeof(IViewModel), typeof(IViewModel), new PropertyMetadata(default));

        public IViewModel CoreViewModel
        {
            get { return (IViewModel)GetValue(CoreViewModelProperty); }
            set { SetValue(CoreViewModelProperty, value); }
        }

        private readonly IDepositoryResolveScope _scope;

        public AppPage()
        {
            _scope = DepositoryResolveScope.Create();
            /*CoreViewModel = App.GetDIContainer().Resolve<IViewModel>(new DependencyResolveOption()
            {
                Scope = _scope
            });*/
        }

        public virtual object GetViewModel() => null;
    }

    public class AppPage<IViewModel> : AppPage
        where IViewModel : class
    {
        public static readonly DependencyProperty ViewModelProperty =
           DependencyProperty.Register(nameof(ViewModel), typeof(IViewModel), typeof(IViewModel), new PropertyMetadata(default));

        public IViewModel ViewModel
        {
            get { return (IViewModel)GetValue(ViewModelProperty); }
            set { SetValue(ViewModelProperty, value); }
        }

        private readonly IDepositoryResolveScope _scope;

        public AppPage()
        {
            _scope = DepositoryResolveScope.Create();
            ViewModel = App.GetDIContainer().Resolve<IViewModel>(new DependencyResolveOption()
            {
                Scope = _scope
            });
        }

        public override object GetViewModel() => ViewModel;
    }
}
