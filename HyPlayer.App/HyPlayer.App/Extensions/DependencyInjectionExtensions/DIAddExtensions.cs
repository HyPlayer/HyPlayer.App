using System.Reflection;
using Depository.Abstraction.Enums;
using Depository.Abstraction.Interfaces;
using Depository.Extensions;

namespace HyPlayer.App.Extensions.DependencyInjectionExtensions;

public static class DIAddExtensions
{
    public static void AddAllImplementationsOf<T>(this IDepository depository,
                                                  DependencyLifetime lifetime = DependencyLifetime.Singleton,
                                                  Assembly? scanningAssembly = null)
    {
        var assembly = scanningAssembly ?? Assembly.GetExecutingAssembly();
        foreach (var type in assembly.GetTypes())
        {
            if (!typeof(T).IsAssignableFrom(type) || type.IsInterface || type.IsAbstract) continue;
            depository.Add(typeof(T), type, lifetime);
            depository.Add(type, type, lifetime);
        }
    }
}