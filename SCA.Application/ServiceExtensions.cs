using System.Reflection;
using FluentValidation;
using HamedStack.CQRS;
using HamedStack.CQRS.FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace SCA.Application;

public static class ServiceExtensions
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var commandValidatorsAssemblies = GetAllAppDomainAssemblies().FindAssembliesWithImplementationsOf(typeof(CommandValidator<>));
        var commandValidatorsAssemblies2 = GetAllAppDomainAssemblies().FindAssembliesWithImplementationsOf(typeof(CommandValidator<,>));
        var queryValidatorsAssemblies = GetAllAppDomainAssemblies().FindAssembliesWithImplementationsOf(typeof(QueryValidator<,>));

        var validatorsAssemblies = commandValidatorsAssemblies.Concat(commandValidatorsAssemblies2).Concat(queryValidatorsAssemblies).ToList();
        if (validatorsAssemblies.Any())
        {
            services.AddValidatorsFromAssemblies(validatorsAssemblies);
        }

        var allTypes = new[]
        {
            typeof(IMediator),
            typeof(IQuery<>),
            typeof(ICommand),
            typeof(ICommand<>),
            typeof(IQueryHandler<,>),
            typeof(ICommandHandler<>),
            typeof(ICommandHandler<,>)

        };

        var appDomain = AppDomain.CurrentDomain.GetAssemblies();

        var assemblies1 = AppDomainContains(allTypes);
        var assemblies2 = appDomain.FindAssembliesWithImplementationsOf(typeof(ICommand));
        var assemblies3 = appDomain.FindAssembliesWithImplementationsOf(typeof(ICommand<>));
        var assemblies4 = appDomain.FindAssembliesWithImplementationsOf(typeof(IQuery<>));

        var assemblies = assemblies1.Concat(assemblies2).Concat(assemblies3).Concat(assemblies4).ToArray();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assemblies));
        services.AddScoped<ICommandQueryDispatcher, CommandQueryDispatcher>();

        return services;
    }

    internal static bool Contains(this Assembly assembly, params Type[] types)
    {
        var assemblyTypes = assembly.GetTypes().SelectMany(t => new[] { t }.Concat(t.GetNestedTypes()));
        return types.Any(type => assemblyTypes.Contains(type));
    }

    internal static IEnumerable<Assembly> AppDomainContains(params Type[] types)
    {
        return AppDomain.CurrentDomain.GetAssemblies().Where(a => a.Contains(types));
    }
    internal static IEnumerable<Assembly> FindAssembliesWithImplementationsOf(this IEnumerable<Assembly> assemblies, Type targetType)
    {
        var resultAssemblies = new HashSet<Assembly>();

        foreach (var assembly in assemblies)
        {
            foreach (var type in assembly.GetTypes())
            {
                if (!type.IsClass || type.IsAbstract || type == targetType)
                {
                    continue;
                }

                bool typeMatches;

                if (targetType.IsGenericTypeDefinition)
                {
                    typeMatches = type.GetInterfaces().Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == targetType) ||
                                  type.BaseType is { IsGenericType: true } && type.BaseType.GetGenericTypeDefinition() == targetType;
                }
                else
                {
                    typeMatches = targetType.IsAssignableFrom(type);
                }

                if (!typeMatches) continue;

                resultAssemblies.Add(assembly);
                break;
            }
        }

        return resultAssemblies;
    }
    internal static IEnumerable<Assembly> GetAllAppDomainAssemblies()
    {
        var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

        var assemblyDir = AppDomain.CurrentDomain.BaseDirectory;

        var allDllFiles = Directory.GetFiles(assemblyDir, "*.dll", SearchOption.AllDirectories);

        foreach (var dllFile in allDllFiles)
        {
            try
            {
                var assemblyName = AssemblyName.GetAssemblyName(dllFile);

                if (assemblies.Any(a => a.FullName == assemblyName.FullName)) continue;

                var loadedAssembly = Assembly.Load(assemblyName);
                assemblies.Add(loadedAssembly);
            }
            catch
            {
                // ignored
            }
        }

        return assemblies;
    }
}