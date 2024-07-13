using HyPlayer.NeteaseApi.Extensions;
using HyPlayer.NeteaseProvider.Constants;

namespace HyPlayer.NeteaseProvider.Mappers;

public static class TypeIdToSearchIdMapper
{
    public static Dictionary<string, int> ResourceMap =
        new()
        {
            { NeteaseTypeIds.SingleSong, 1 },
            { NeteaseTypeIds.Playlist, 1000 },
            { NeteaseTypeIds.Album, 10 },
            { NeteaseTypeIds.Artist, 100 },
            { NeteaseTypeIds.User, 1002 },
            { NeteaseTypeIds.RadioProgram, 1009 },
            { NeteaseTypeIds.MBlog, 1014 },
            { NeteaseTypeIds.Mv, 1004 },
            { NeteaseTypeIds.Lyric, 1006 },
            { NeteaseTypeIds.Dynamic, 2000 },
        };

    public static int MapToResourceId(string typeId)
    {
        return ResourceMap.GetValueOrDefault(typeId, 1);
    }
}