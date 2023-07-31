using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using HyPlayer.App.Interfaces.ViewModels;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction.Models.Containers;


namespace HyPlayer.App.ViewModels
{
    public partial class AccountViewModel : ObservableObject, ISingletonViewModel
    {

        private readonly NeteaseProvider.NeteaseProvider _provider;
        public AccountViewModel(NeteaseProvider.NeteaseProvider provider)
        { 
            _provider = provider;
        }

        [ObservableProperty] private bool _isLogin;
        [ObservableProperty] private NeteaseUser? _user;


        public async Task SignInAsync(string usr,  string pwd)
        {
            bool isPhone = Regex.Match(usr, "^[0-9]+$").Success;
            if (isPhone)
            {
                IsLogin = await _provider.LoginCellphone(usr, pwd);
                User = _provider.LoginedUser;
            }
            else
            {
                IsLogin = await _provider.LoginEmail(usr, pwd);
                User = _provider.LoginedUser;
            }
        }
    }
}
