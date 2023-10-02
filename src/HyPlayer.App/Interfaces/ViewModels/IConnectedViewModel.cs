namespace HyPlayer.Interfaces.ViewModels;

public interface IConnectedViewModel
{
    /// <summary>
    /// 编号
    /// </summary>
    int? ConnectedItemIndex { get; set; }
    /// <summary>
    /// 元素名称
    /// </summary>
    string? ConnectedElementName { get; set; }
}

