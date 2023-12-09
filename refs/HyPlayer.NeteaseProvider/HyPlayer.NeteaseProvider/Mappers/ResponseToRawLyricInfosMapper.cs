using HyPlayer.NeteaseApi.ApiContracts;
using HyPlayer.NeteaseProvider.Models;
using HyPlayer.PlayCore.Abstraction.Models.Lyric;

namespace HyPlayer.NeteaseProvider.Mappers;

public static class ResponseToRawLyricInfosMapper
{
    public static NeteaseSongLyricInfos Map(this LyricResponse response)
    {
        var ret = new NeteaseSongLyricInfos();
        if (string.IsNullOrEmpty(response.Lyric?.Lyric))
        {
            ret.Add(new NeteaseRawLyricInfo
                    {
                        LyricText = response.Lyric?.Lyric!,
                        LyricTypeActual = LyricType.Original,
                        Author = new()
                                 {
                                     ActualId = response.LyricUser?.UserId!,
                                     Name = response.LyricUser?.Nickname ?? "未知贡献者"
                                 }
                    });
        }

        if (string.IsNullOrEmpty(response.TranslationLyric?.Lyric))
        {
            ret.HasTranslation = true;
            ret.Add(new NeteaseRawLyricInfo
                    {
                        LyricText = response.TranslationLyric?.Lyric!,
                        LyricTypeActual = LyricType.Translation,
                        Author = new()
                                 {
                                     ActualId = response.TranslationUser?.UserId!,
                                     Name = response.TranslationUser?.Nickname ?? "未知贡献者"
                                 }
                    }
            );
        }

        if (string.IsNullOrEmpty(response.RomajiLyric?.Lyric))
        {
            ret.Add(new NeteaseRawLyricInfo
                    {
                        LyricText = response.RomajiLyric?.Lyric!,
                        LyricTypeActual = LyricType.Romaji,
                        Author = new()
                                 {
                                     ActualId = "1",
                                     Name = "网易云音乐 自动转换"
                                 }
                    }
            );
        }

        if (string.IsNullOrEmpty(response.YunLyric?.Lyric))
        {
            ret.HasWordLyric = true;
            ret.Add(new NeteaseRawLyricInfo
                    {
                        LyricText = response.YunLyric?.Lyric!,
                        IsWord = true,
                        LyricTypeActual = LyricType.Original,
                        Author = new()
                                 {
                                     ActualId = response.LyricUser?.UserId!,
                                     Name = response.LyricUser?.Nickname ?? "未知贡献者"
                                 }
                    });
        }

        if (string.IsNullOrEmpty(response.YunTranslationLyric?.Lyric))
        {
            ret.HasTranslation = true;
            ret.Add(new NeteaseRawLyricInfo
                    {
                        LyricText = response.YunTranslationLyric?.Lyric!,
                        IsWord = true,
                        LyricTypeActual = LyricType.Translation,
                        Author = new()
                                 {
                                     ActualId = response.TranslationUser?.UserId!,
                                     Name = response.TranslationUser?.Nickname ?? "未知贡献者"
                                 }
                    });
        }


        if (string.IsNullOrEmpty(response.YunRomajiLyric?.Lyric))
        {
            ret.Add(new NeteaseRawLyricInfo
                    {
                        LyricText = response.YunRomajiLyric?.Lyric!,
                        LyricTypeActual = LyricType.Romaji,
                        Author = new()
                                 {
                                     ActualId = "1",
                                     Name = "网易云音乐 自动转换"
                                 }
                    }
            );
        }

        return ret;
    }
}