﻿using AsyncAwaitBestPractices;
using Depository.Abstraction.Interfaces;
using HyPlayer.PlayCore.Abstraction;
using System.Collections.ObjectModel;

namespace HyPlayer.PlayCore;

public sealed partial class Chopin : PlayCoreBase
{
    public Chopin(
        IEnumerable<AudioServiceBase> audioServices,
        IEnumerable<ProviderBase> providers,
        IEnumerable<PlayControllerBase> playListControllers,
        IDepository depository)
    {
        SafeFireAndForgetExtensions.Initialize(false);
        _depository = depository;
        AudioServices =
            new(
                new ObservableCollection<AudioServiceBase>(audioServices.ToList()));
        MusicProviders =
            new(
                new ObservableCollection<ProviderBase>(providers.ToList()));
        PlayListControllers =
            new(
                new ObservableCollection<PlayControllerBase>(playListControllers.ToList()));
    }
}