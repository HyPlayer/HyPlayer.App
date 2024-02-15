using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;
using HyPlayer.PlayCore.Abstraction.Models.Notifications;

namespace HyPlayer.PlayCore.Implementation.AudioGraphService.Abstractions.Notifications
{
    public class AudioGraphTicketReachesEndNotification:AudioTicketReachesEndNotification
    {
        public override AudioTicketBase AudioGraphTicket { get; init; }
        public AudioGraphTicketReachesEndNotification(AudioGraphTicket ticket)
        {
            AudioGraphTicket = ticket;
        }
    }
}
