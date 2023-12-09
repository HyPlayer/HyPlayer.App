using HyPlayer.NeteaseApi.ApiContracts;
using HyPlayer.NeteaseApi.Models.ResponseModels;
using HyPlayer.NeteaseProvider.Models;

namespace HyPlayer.NeteaseProvider.Mappers;

public static class PlaylistItemToNeteasePlaylistMapper
{
    public static NeteasePlaylist MapToNeteasePlaylist(this PlaylistDto item)
    {
        return new NeteasePlaylist
               {
                   Name = item.Name ?? "未知歌单",
                   ActualId = item.Id!,
                   Description = item.Description,
                   CreatorList = new List<string>() { item.Creator?.Nickname! },
                   Creator = item.Creator?.MapToNeteaseUser(),
                   Subscribed = item.Subscribed is true,
                   UpdateTime = item.UpdateTime,
                   TrackCount = item.TrackCount,
                   PlayCount = item.PlayCount,
                   SubscribedCount = item.SubscribedCount,
                   CoverUrl = item.CoverUrl
               };
    }

    public static NeteasePlaylist MapToNeteasePlaylist(this RecommendPlaylistDto dto)
    {
        return new NeteasePlaylist
               {
                   Name = dto.Name ?? "未知歌单",
                   ActualId = dto.Id!,
                   CoverUrl = dto.CoverUrl,
                   Creator = dto.Creator?.MapToNeteaseUser(),
                   Subscribed = false,
                   UpdateTime = 0,
                   TrackCount = dto.TrackCount,
                   PlayCount = dto.PlayCount,
                   SubscribedCount = 0,
                   CreatorList = new List<string>() { dto.Creator?.Nickname! },
               };
    }

    public static NeteasePlaylist MapToNeteasePlaylist(this PlaylistDetailResponse.PlayListData data)
    {
        return new NeteasePlaylist
               {
                   Name = data.Name ?? "未知歌单",
                   ActualId = data.Id,
                   Description = data.Description,
                   CreatorList = new List<string>()
                                 {
                                     data.Creator?.Nickname!
                                 },
                   Creator = data.Creator?.MapToNeteaseUser(),
                   Subscribed = data.Subscribed is true,
                   UpdateTime = data.UpdateTime,
                   TrackCount = data.TrackCount,
                   PlayCount = data.PlayCount,
                   SubscribedCount = data.SubscribedCount,
                   CoverUrl = data.CoverUrl,
               };
    }
}