using HyPlayer.NeteaseApi;
using HyPlayer.NeteaseApi.ApiContracts;
using HyPlayer.NeteaseApi.Bases;
using HyPlayer.NeteaseProvider.Constants;
using HyPlayer.NeteaseProvider.Mappers;
using HyPlayer.PlayCore.Abstraction.Interfaces.PlayListContainer;
using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Containers;

namespace HyPlayer.NeteaseProvider.Models;

public class NeteaseSearchContainer : LinerContainerBase, IProgressiveLoadingContainer
{
    public override string ProviderId => "ncm";
    public override string TypeId => NeteaseTypeIds.SearchResult; // search


    public required int SearchTypeId { get; set; }
    public required string SearchKeyword { get; set; }

    public NeteaseSearchContainer()
    {
        Name = "搜索结果";
    }

    public override async Task<List<ProvidableItemBase>> GetAllItemsAsync(CancellationToken ctk = new())
    {
        return (await GetProgressiveItemsListAsync(0, MaxProgressiveCount, ctk)).Item2;
    }

    public async Task<(bool, List<ProvidableItemBase>)> GetProgressiveItemsListAsync(int start, int count,CancellationToken ctk = new())
    {
        switch (SearchTypeId)
        {
            case 1:
                var result = await NeteaseProvider.Instance
                                                  .RequestAsync<SearchSongResponse, SearchRequest, SearchResponse,
                                                      ErrorResultBase, SearchActualRequest>(
                                                      NeteaseApis.SearchApi,
                                                      new SearchRequest
                                                      {
                                                          Keyword = SearchKeyword,
                                                          Type = SearchTypeId,
                                                          Limit = count,
                                                          Offset = start
                                                      }, ctk);
                return result.Match(success => (success.Result?.Count > start + count, success.Result?.Items
                                                    ?.Select(t => (ProvidableItemBase)t.MapToNeteaseMusic())
                                                    .ToList() ?? new()),
                                    _ => (false, new()));
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public int MaxProgressiveCount => 30;
}