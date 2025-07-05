using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HexaCleanHybArch.Template.Config.Loader
{
    public static class AdapterEntityLoader
    {
        public static void ApplyAdapterEntityConfiguration(ModelBuilder modelBuilder)
        {
            string basePath = AppContext.BaseDirectory;

            IEnumerable<string> adapterDlls = Directory.EnumerateFiles(basePath, "Adapters.*.dll", SearchOption.TopDirectoryOnly);

            foreach (string dllPath in adapterDlls)
            {
                try
                {
                    // Try to find if the assembly has already been loaded based on the full file path
                    Assembly? existingAssembly = AppDomain.CurrentDomain
                        .GetAssemblies()
                        .FirstOrDefault(a =>
                        {
                            try
                            {
                                return !a.IsDynamic && Path.GetFullPath(a.Location) == Path.GetFullPath(dllPath);
                            }
                            catch
                            {
                                return false;
                            }
                        });

                    // Load the assembly if not already loaded
                    Assembly targetAssembly = existingAssembly ?? Assembly.LoadFrom(dllPath);

                    // Always apply entity configurations from the target assembly
                    modelBuilder.ApplyConfigurationsFromAssembly(targetAssembly);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[ModuleEntityLoader] Failed to load assembly: {dllPath}");
                    Console.WriteLine($"[Error] {ex.Message}");
                }
            }
        }
    }
}
