using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HyPlayer.Interfaces.ViewModels;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction.Models.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HyPlayer.ViewModels
{
    public partial class SongViewModel : ObservableObject, IScopedViewModel
    {
        private readonly NeteaseProvider.NeteaseProvider _provider;

        [ObservableProperty] public NeteaseSong _ncmSong;
        [ObservableProperty] private string? _songName;
        [ObservableProperty] private string? _songId;
        [ObservableProperty] private TimeSpan? _duration;
        [ObservableProperty] private Uri _coverUri;
        [ObservableProperty] private List<PersonBase>? _artists;
        [ObservableProperty] private AlbumBase? _album;
        [ObservableProperty] private int _loadIndex = -1;

        public SongViewModel(NeteaseProvider.NeteaseProvider provider)
        {
            _provider = provider;
        }
        [RelayCommand]
        public void GetSongInfo()
        {
            if (NcmSong == null)
            {
                SongName = "Song0";
                Duration = new TimeSpan(0);
                CoverUri = new Uri("ms-appx:///Assets/Images/StoreLogo.png");
                return;
            }

            SongName = NcmSong.Name;
            Duration = new TimeSpan(NcmSong.Duration);
            CoverUri = new Uri(NcmSong.CoverUrl ?? "ms-appx:///Assets/Images/StoreLogo.png");
            Artists = NcmSong.Artists ?? new List<PersonBase>();
            SongId = NcmSong.ActualId;
            Album = NcmSong.Album;
        }
    }
}
