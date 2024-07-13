namespace HyPlayer.NeteaseApi.Bases;

public class IdOrIdListListRequest : RequestBase
{
    /// <summary>
    /// 歌曲 ID 列表
    /// </summary>
    public List<string>? IdList { get; set; }
    
    /// <summary>
    /// 歌曲 ID
    /// </summary>
    public string? Id { get; set; }

    public string ConvertToIdStringList()
    {
        return string.IsNullOrWhiteSpace(Id)
            ? $"[{string.Join(",", IdList ?? new List<string>())}]"
            : $"[{Id}]";
    }

    public string ParseToIdObjects()
    {
        return string.IsNullOrWhiteSpace(Id)
            ? $"[{string.Join(",", IdList?.Select(id => $$"""{"id":'{{id}}'}""") ?? new List<string>())}]"
            : $$"""[{"id": '{{Id}}'}]""";
    }
}