using HyPlayer.PlayCore.Abstraction.Models;
using HyPlayer.PlayCore.Abstraction.Models.Resources;
using System;
using System.Threading;
using System.Threading.Tasks;
using Windows.Storage;

namespace HyPlayer.PlayCore.Implementation.AudioGraphService.Abstractions
{
    public class AudioGraphMusicResource : MusicResourceBase
    {
        public StorageFile LocalFile { get; private set; }
        public override async Task<ResourceResultBase> GetResourceAsync(ResourceQualityTag qualityTag = null, CancellationToken ctk = default)
        {
            try
            {
                if (Uri == null)
                {
                    return new AudioGraphMusicResourceResult() { ExternalException = new ArgumentNullException(), ResourceStatus = ResourceStatus.Fail };
                }
                if (string.IsNullOrEmpty(Uri.Host))
                {
                    LocalFile = await StorageFile.GetFileFromPathAsync(Uri.LocalPath);
                }
                return
                    new AudioGraphMusicResourceResult() { ExternalException = null, ResourceStatus = ResourceStatus.Success };
            }
            catch (Exception ex)
            {
                return
                    new AudioGraphMusicResourceResult() { ExternalException = ex, ResourceStatus = ResourceStatus.Fail };
            }
        }
    }
    public class AudioGraphMusicResourceResult : ResourceResultBase
    {
        public override required Exception ExternalException { get; init; }
        public override required ResourceStatus ResourceStatus { get; init; }
    }
}
