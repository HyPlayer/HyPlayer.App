using Depository.Abstraction.Enums;
using Depository.Abstraction.Interfaces;
using HyPlayer.Interfaces.ViewModels;
using System.Reflection;

namespace HyPlayer.Extensions.DependencyInjectionExtensions;

public static class MvvmExtensions
{
    public static void AddViewModels(this IDepository depository, Assembly? scanningAssembly = null)
    {
        depository.AddAllImplementationsOf<IScopedViewModel>(DependencyLifetime.Scoped, scanningAssembly);
        depository.AddAllImplementationsOf<ISingletonViewModel>(DependencyLifetime.Singleton, scanningAssembly);
        depository.AddAllImplementationsOf<ITransientViewModel>(DependencyLifetime.Transient, scanningAssembly);
    }

    public static void AddMvvm(this IDepository depository, Assembly? scanningAssembly = null)
    {
        depository.AddViewModels(scanningAssembly);
    }
}