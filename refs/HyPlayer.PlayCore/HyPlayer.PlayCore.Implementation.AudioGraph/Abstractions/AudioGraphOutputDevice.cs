using HyPlayer.PlayCore.Abstraction.Interfaces.AudioServices;
using Windows.Devices.Enumeration;

namespace HyPlayer.PlayCore.Implementation.AudioGraphService.Abstractions
{
    public class AudioGraphOutputDevice : OutputDeviceBase
    {
        public DeviceInformation DeviceInformation { get; init; }
    }
}
