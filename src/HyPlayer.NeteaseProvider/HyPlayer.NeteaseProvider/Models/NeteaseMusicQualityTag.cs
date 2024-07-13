using HyPlayer.PlayCore.Abstraction.Models.Resources;

namespace HyPlayer.NeteaseProvider.Models;

public class NeteaseMusicQualityTag : ResourceQualityTag
{
    public string Quality { get; set; }
    
    public NeteaseMusicQualityTag(string qualityString)
    {
        Quality = qualityString;
    }
}