using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Security.Principal;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

using HyPlayer.App.Interfaces.ViewModels;


namespace HyPlayer.App.ViewModels
{
    public partial class AccountViewModel : ObservableObject, IScopedViewModel
    {

        public readonly NeteaseProvider.NeteaseProvider _provider;
        public AccountViewModel(NeteaseProvider.NeteaseProvider provider)
        { 
            _provider = provider;
        }

        public string UsrName
        {
            get
            {
                if (_provider.LoginedUser != null)
                {
                    return _provider.LoginedUser.Name;
                }
                else return "";
            }
        }

        public string description
        {
            get
            {
                if (_provider.LoginedUser != null)
                {
                    return _provider.LoginedUser.Description;
                }
                else return "";
            }
        }

        public async Task<bool> SignInAsync(string usr,  string pwd)
        {
            bool isPhone = Regex.Match(usr, "^[0-9]+$").Success;
            if (isPhone)
            {
                var result = await _provider.LoginCellphone(usr, pwd);
                return result;
            }
            else
            {
                var result = await _provider.LoginEmail(usr, pwd);
                return result;
            }
        }
    }
}
