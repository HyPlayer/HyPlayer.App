using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using Depository.Abstraction.Interfaces;
using Depository.Core;
using Depository.Extensions;

namespace HyPlayer.App.Interfaces.Views
{
    public class ReactiveUserControl<TViewModel> : UserControl
        where TViewModel : class
    {
        private readonly IDepositoryResolveScope _scope;

        public static readonly DependencyProperty ViewModelProperty = DependencyProperty
                .Register(nameof(ViewModel), typeof(TViewModel), typeof(ReactiveUserControl<TViewModel>), new PropertyMetadata(default, new PropertyChangedCallback((dp, args) =>
                {
                    var instance = dp as ReactiveUserControl<TViewModel>;
                    instance.OnViewModelChanged(args);
                })));

        public TViewModel ViewModel
        {
            get => (TViewModel)GetValue(ViewModelProperty);
            set => SetValue(ViewModelProperty, value);
        }

        internal virtual void OnViewModelChanged(DependencyPropertyChangedEventArgs e)
        {
        }

        public ReactiveUserControl()
        {
            
            DataContext = ViewModel;
            
        }
    }
}