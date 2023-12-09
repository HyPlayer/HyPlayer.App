namespace HyPlayer.PlayCore.Abstraction.Interfaces.AudioServices;

/// <summary>
/// 可以更改输出设备
/// </summary>
public interface IOutputDeviceChangeableService : IAudioService
{
    /// <summary>
    /// 获取可输出设备列表
    /// </summary>
    /// <returns></returns>
    public Task<List<OutputDeviceBase>> GetOutputDevices( CancellationToken ctk = new());

    /// <summary>
    /// 设置输出设备
    /// </summary>
    /// <param name="device">设备信息</param>
    /// <returns></returns>
    public Task SetOutputDevices(OutputDeviceBase device, CancellationToken ctk = new());
}

public abstract class OutputDeviceBase
{
    public required string Name { get; set; }
}