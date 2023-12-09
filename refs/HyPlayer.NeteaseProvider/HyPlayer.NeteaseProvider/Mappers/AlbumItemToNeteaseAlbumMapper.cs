using HyPlayer.NeteaseApi.ApiContracts;
using HyPlayer.NeteaseProvider.Models;

namespace HyPlayer.NeteaseProvider.Mappers;

public static class AlbumItemToNeteaseAlbumMapper
{
    public static NeteaseAlbum? MapToNeteaseAlbum(this ArtistAlbumsResponse.ArtistAlbumDto item)
    {
        return new NeteaseAlbum
               {
                   Name = item.Name ?? "未知专辑",
                   ActualId = item.Id!, 
                   PictureUrl = item.PictureUrl,
                   Alias = item.Alias?.ToList(),
                   Translations = item.Translations?.ToList(), 
                   Company = item.Company,
                   BriefDescription = item.BriefDescription,
                   SubType = item.Subtype,
                   AlbumType = item.AlbumType,
                   IsSubscribed = item.IsSubscribed,
                   Artists = item.Artists?.Select(t=>t.MapToNeteaseArtist()).ToList(),
                   Translation = string.Join(" / ",item.Translations??new string[]{}),
                   Description = item.Description,
                   CreatorList = item.Artists?.Select(t=>t.Name??"未知歌手").ToList()

               };
    }
}