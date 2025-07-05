using HexaCleanHybArch.Template.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace HexaCleanHybArch.Template.Config.Loader
{
    public class ApapterModuleLoader
    {
        public static void RegisterAdapter(IServiceCollection services, IConfiguration configuration, params Assembly[] assemblies)
        {
            IEnumerable<Type>? adapterModuleTypes = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(w => typeof(IAdapterModule).IsAssignableFrom(w) && !w.IsInterface && !w.IsAbstract);

            foreach (Type? type in adapterModuleTypes)
            {
                try
                {
                    if (Activator.CreateInstance(type) is IAdapterModule adapter)
                    {
                        adapter.Register(services, configuration);
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine($"Failed to register adapter module: {type.FullName}, {ex.Message}");
                }
            }
        }
    }
}
