using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.Interfaces.ViewModels;
using HyPlayer.NeteaseProvider.Models;

namespace HyPlayer.ViewModels
{
    public partial class UserViewModel : ObservableObject, IScopedViewModel
    {
        private readonly NeteaseProvider.NeteaseProvider _provider;

        [ObservableProperty] public NeteaseUser? _ncmUser;
        [ObservableProperty] private string? _userName;
        [ObservableProperty] private string? _description;
        [ObservableProperty] private int? _vipType;

        public UserViewModel(NeteaseProvider.NeteaseProvider provider)
        {
            _provider = provider;
        }

        [RelayCommand]
        public void GetUserInfo()
        {
            if (NcmUser == null)
            {
                UserName = "HyPlayer";
                Description = "A Third-party Netease Music Cilent";
                VipType = 0;
                return;
            }

            UserName = NcmUser.Name;
            Description = NcmUser.Description;
            VipType = NcmUser.VipType;
        }
    }

}
