using HyPlayer.PlayCore.Abstraction.Models;
using Windows.Media.Audio;

namespace HyPlayer.PlayCore.Implementation.AudioGraphService.Abstractions
{
    public class AudioGraphServiceSettings : AudioServiceSettingsBase
    {
        public AudioGraphSettings AudioGraphSettings { get; set; }
    }
}
