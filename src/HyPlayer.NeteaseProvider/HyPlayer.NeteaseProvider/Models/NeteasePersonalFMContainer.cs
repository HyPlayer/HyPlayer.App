using HyPlayer.NeteaseApi.ApiContracts;
using HyPlayer.NeteaseProvider.Constants;
using HyPlayer.NeteaseProvider.Mappers;
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Containers;

namespace HyPlayer.NeteaseProvider.Models;

public class NeteasePersonalFMContainer : UndeterminedContainerBase
{
    public override string ProviderId => "ncm";
    public override string TypeId => NeteaseTypeIds.PersonalFm;

    public string RecommendType = "DEFAULT";
    
    public override async Task<List<ProvidableItemBase>> GetNextItemsRangeAsync(CancellationToken ctk = new CancellationToken())
    {
        return (await NeteaseProvider.Instance.RequestAsync(NeteaseApis.PersonalFmApi, new PersonalFmRequest(){ Mode = RecommendType}, ctk))
            .Match(s => s.Items?.Select(t => (ProvidableItemBase)t.MapToNeteaseMusic()).ToList() ?? new List<ProvidableItemBase>(),
                   e => new List<ProvidableItemBase>());
    }
}