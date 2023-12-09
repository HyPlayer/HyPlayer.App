using Windows.Media.Audio;
using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;

namespace AudioGraphAudioService;

public class AudioGraphAudioTicket : AudioTicketBase, 
{
    public MediaSourceAudioInputNode Input { get; set; }
    public double Volume { get; set; }
    public long Duration { get; set; }
    public 
    public double PlaybackSpeed { get; set; }

    public void Abc()
    {
        
    }
}