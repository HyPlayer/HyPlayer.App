namespace HyPlayer.NeteaseProvider.Models;

public class NeteaseSongLyricInfos : List<NeteaseRawLyricInfo>
{
    public bool HasTranslation { get; set; }
    public bool HasWordLyric { get; set; }
}