using HyPlayer.PlayCore.Abstraction.Models.Containers;

namespace HyPlayer.PlayCore.Abstraction.Models.SingleItems;

public abstract class CommentBase : ProvidableItemBase
{
    public string? Content { get; set; }
    public long SendDate { get; set; }
    public PersonBase? Sender { get; set; }
    public int LikedCount { get; set; }
}