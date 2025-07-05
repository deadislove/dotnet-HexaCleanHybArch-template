using Adapters.Auth.Application.Interfaces;
using Adapters.Auth.Application.Services;
using HexaCleanHybArch.Template.Shared.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adapters.Auth
{
    public class AdapterAuth: IAdapterModule
    {
        public void Register(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtTokenService, JwtTokenService>();
        }
    }
}
