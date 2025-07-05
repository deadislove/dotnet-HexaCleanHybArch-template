using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace HexaCleanHybArch.Template.Shared.Interfaces
{
    public interface IAdapterModule
    {
        void Register(IServiceCollection services, IConfiguration configuration);
    }
}
