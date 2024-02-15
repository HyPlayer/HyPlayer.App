using HyPlayer.PlayCore.Abstraction.Models.AudioServiceComponents;
using HyPlayer.PlayCore.Abstraction.Models.Resources;
using System;
using System.Linq;
using System.Threading.Tasks;
using Windows.Media.Audio;
using Windows.Media.Core;

namespace HyPlayer.PlayCore.Implementation.AudioGraphService.Abstractions
{
    public class AudioGraphTicket : AudioTicketBase, IDisposable
    {
        public MediaSourceAudioInputNode PlaybackMediaSourceInputNode { get; internal set; }
        public MediaSource PlaybackMediaSource { get; internal set; }

        internal delegate void AudioGraphTicketReachesEndHandler(AudioGraphTicket audioGraphTicket);

        internal event AudioGraphTicketReachesEndHandler OnAudioGraphTicketReachesEnd;


        public double OutgoingVolume
        {
            get => _outgoingVolume;
            internal set
            {
                _outgoingVolume = value;
                if (PlaybackMediaSourceInputNode != null)
                {
                    PlaybackMediaSourceInputNode.OutgoingGain = _outgoingVolume;
                }
            }
        }
        private double _outgoingVolume = 1d;
        private bool disposedValue;
        public static async Task<AudioGraphTicket> CreateAudioGraphTicket(MusicResourceBase musicResource, AudioGraph audioGraph)
        {
            if (musicResource is AudioGraphMusicResource resource)
            {
                MediaSource mediaSource;
                if (resource.LocalFile != null)
                {
                    mediaSource = MediaSource.CreateFromStorageFile(resource.LocalFile);
                }
                else
                {
                    mediaSource = MediaSource.CreateFromUri(resource.Uri);
                }
                var nodeResult = await audioGraph.CreateMediaSourceAudioInputNodeAsync(mediaSource);
                if (nodeResult.Status != MediaSourceAudioInputNodeCreationStatus.Success)
                {
                    throw nodeResult.ExtendedError;
                }
                return new AudioGraphTicket(nodeResult.Node, mediaSource)
                {
                    AudioServiceId = "com.storyteller.audiograph.chopin",
                    MusicResource = musicResource,
                    Status = AudioTicketStatus.None,
                };
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void ThrowExceptionIfDisposed()
        {
            if (disposedValue)
            {
                throw new ObjectDisposedException(nameof(AudioGraphTicket));
            }
        }
        public void Start()
        {
            ThrowExceptionIfDisposed();
            PlaybackMediaSourceInputNode.Start();
            Status = AudioTicketStatus.Playing;
        }
        public void Stop()
        {
            ThrowExceptionIfDisposed();
            PlaybackMediaSourceInputNode.Stop();
            Status = AudioTicketStatus.Stopped;
        }
        public void RemoveAllOutputConnections()
        {
            ThrowExceptionIfDisposed();
            var connections = PlaybackMediaSourceInputNode.OutgoingConnections.ToList();
            foreach (var connection in connections)
            {
                PlaybackMediaSourceInputNode?.RemoveOutgoingConnection(connection.Destination);
            }
        }
        public async Task ReplaceAudioGraph(AudioGraph audioGraph)
        {
            ThrowExceptionIfDisposed();
            var previousMediaSourceNode = PlaybackMediaSourceInputNode;
            var position = previousMediaSourceNode.Position;
            previousMediaSourceNode.Dispose();
            PlaybackMediaSource.Reset();
            var creationResult = await audioGraph.CreateMediaSourceAudioInputNodeAsync(PlaybackMediaSource);
            if (creationResult.Status != MediaSourceAudioInputNodeCreationStatus.Success)
            {
                throw creationResult.ExtendedError;
            }
            var newMediaSourceNode = creationResult.Node;
            newMediaSourceNode.Seek(position);
            PlaybackMediaSourceInputNode = newMediaSourceNode;
        }
        private void OnPlaybackMediaReachesEnd(MediaSourceAudioInputNode sender, object args)
        {
            OnAudioGraphTicketReachesEnd?.Invoke(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    PlaybackMediaSourceInputNode.MediaSourceCompleted -= OnPlaybackMediaReachesEnd;
                    PlaybackMediaSourceInputNode?.Stop();
                    RemoveAllOutputConnections();
                    PlaybackMediaSourceInputNode?.Dispose();
                    PlaybackMediaSource?.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        private AudioGraphTicket(MediaSourceAudioInputNode playbackMediaSourceInputNode, MediaSource playbackMediaSource)
        {
            PlaybackMediaSourceInputNode = playbackMediaSourceInputNode;
            PlaybackMediaSource = playbackMediaSource;
            PlaybackMediaSourceInputNode.MediaSourceCompleted += OnPlaybackMediaReachesEnd;
        }
    }
}
