namespace HyPlayer.PlayCore.Abstraction.Models.Lyric;

public abstract class RawLyricInfo : ResourceBase
{
    public override ResourceType Type => ResourceType.Text;
    public abstract LyricType LyricType { get; }
}

public enum LyricType
{
    Original,
    Translation,
    Romaji,
    Furigana,
    Commentary,
    Soramimi, // 空耳
}