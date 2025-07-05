using Adapters.User.Application.Facades;
using Adapters.User.Application.Interfaces;
using Adapters.User.Application.Interfaces.Facades;
using Adapters.User.Application.Mappings;
using Adapters.User.Application.Services;
using Adapters.User.Domain.Interfaces;
using Adapters.User.Infra.Repositories;
using HexaCleanHybArch.Template.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Adapters.User
{
    public class AdapterUser : IAdapterModule
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            // Automapper profile setting
            services.AddAutoMapper(typeof(UserMappingProfile));
            // Repository register
            services.AddScoped<IUserRepository, UserRepository>();
            // Service register
            services.AddScoped<IUserService, UserService>();
            // Facade register
            services.AddScoped<IUserFacade, UserFacade>();
        }
    }
}
