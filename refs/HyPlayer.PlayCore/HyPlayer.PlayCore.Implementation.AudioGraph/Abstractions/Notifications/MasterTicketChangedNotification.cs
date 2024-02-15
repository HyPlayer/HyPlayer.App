namespace HyPlayer.PlayCore.Implementation.AudioGraphService.Abstractions.Notifications
{
    public class MasterTicketChangedNotification
    {
        public AudioGraphTicket MasterTicket { get; init; }
        public MasterTicketChangedNotification(AudioGraphTicket ticket)
        {
            MasterTicket = ticket;
        }
    }
}
