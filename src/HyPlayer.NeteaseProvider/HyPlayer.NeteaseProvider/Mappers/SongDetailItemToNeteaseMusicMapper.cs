using HyPlayer.NeteaseApi.Models.ResponseModels;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction.Models.Containers;

namespace HyPlayer.NeteaseProvider.Mappers;

public static class SongDetailItemToNeteaseMusicMapper
{
    public static NeteaseSong MapToNeteaseMusic(this EmittedSongDto item)
    {
        return new NeteaseSong
               {
                   Name = item.Name ?? "未知歌曲",
                   ActualId = item.Id!,
                   Album = item.Album.MapToNeteaseAlbum(),
                   CreatorList = item.Artists is { Length: > 0 }
                       ? item.Artists.Select(ar => ar.Name??"未知歌手").ToList()
                       : new List<string>(),
                   Duration = item.Duration,
                   Available = true,
                   Artists = item.Artists?.Select(ar=>(PersonBase)ar.MapToNeteaseArtist()).ToList(),
                   CoverUrl = item.Album?.PictureUrl
               };
    }
    
    public static NeteaseSong MapToNeteaseMusic(this EmittedSongDtoWithPrivilege item)
    {
        return new NeteaseSong
               {
                   Name = item.Name ?? "未知歌曲",
                   ActualId = item.Id!,
                   Album = item.Album.MapToNeteaseAlbum(),
                   CreatorList = item.Artists is { Length: > 0 }
                       ? item.Artists.Select(ar => ar.Name??"未知歌手").ToList()
                       : new List<string>(),
                   Duration = item.Duration,
                   Available = item.Privilege?.PlayLevel is not (null or "none"),
                   Artists = item.Artists?.Select(ar=>(PersonBase)ar.MapToNeteaseArtist()).ToList(),
                   CoverUrl = item.Album?.PictureUrl
               };
    }
    
    public static NeteaseSong MapToNeteaseMusic(this SongDto item)
    {
        return new NeteaseSong
               {
                   Name = item.Name ?? "未知歌曲",
                   ActualId = item.Id!,
                   Album = item.Album.MapToNeteaseAlbum(),
                   CreatorList = item.Artists is { Length: > 0 }
                       ? item.Artists.Select(ar => ar.Name??"未知歌手").ToList()
                       : new List<string>(),
                   Duration = item.Duration,
                   Available = true,
                   Artists = item.Artists?.Select(ar=>(PersonBase)ar.MapToNeteaseArtist()).ToList(),
                   CoverUrl = item.Album?.PictureUrl
               };
    }
    
    public static NeteaseSong MapToNeteaseMusic(this SongWithPrivilegeDto item)
    {
        return new NeteaseSong
               {
                   Name = item.Name ?? "未知歌曲",
                   ActualId = item.Id!,
                   Album = item.Album.MapToNeteaseAlbum(),
                   CreatorList = item.Artists is { Length: > 0 }
                       ? item.Artists.Select(ar => ar.Name??"未知歌手").ToList()
                       : new List<string>(),
                   Duration = item.Duration,
                   Available = item.Privilege?.PlayLevel is not (null or "none"),
                   Artists = item.Artists?.Select(ar=>(PersonBase)ar.MapToNeteaseArtist()).ToList(),
                   CoverUrl = item.Album?.PictureUrl
               };
    }
}