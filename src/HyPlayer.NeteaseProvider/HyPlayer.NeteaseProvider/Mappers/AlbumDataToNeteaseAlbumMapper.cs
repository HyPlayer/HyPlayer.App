using HyPlayer.NeteaseApi.Models.ResponseModels;
using HyPlayer.NeteaseProvider.Models;

namespace HyPlayer.NeteaseProvider.Mappers;

public static class AlbumDataToNeteaseAlbumMapper
{
    public static NeteaseAlbum? MapToNeteaseAlbum(this AlbumDto? data)
    {
        if (data is null) return null;
        return new NeteaseAlbum
               {
                   Name = data.Name!,
                   ActualId = data.Id!,
                   PictureUrl = data.PictureUrl
               };
        
    }
}