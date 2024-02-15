using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;

namespace HyPlayer.PlayCore.Abstraction.Models.Notifications
{
    public abstract class AudioTicketReachesEndNotification
    {
        public abstract AudioTicketBase AudioGraphTicket { get; init; }
    }
}
