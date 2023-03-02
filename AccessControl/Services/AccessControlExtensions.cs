using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessControl.Services
{
    public static class AccessControlExtensions
    {
        public static IApplicationBuilder UseCredentials(
            this IApplicationBuilder app
            )
        {
            app.UseMiddleware<ErrorHandlerMiddleware>();
            return app;
        }

        public static IServiceCollection AddCrententialsAccess(
            this IServiceCollection services
            )
        {
            services.AddSingleton<ICredentialManager, CredentialManager>();
            return AddServices(services);
        }

        private static IServiceCollection AddServices(IServiceCollection services)
        {
            services.AddSingleton<IAccessService, AccessService>();
            services.AddTransient<ITokenManager, TokenManager>();
            services.AddSingleton<IJwtHandler, JwtHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            return services;
        }
    }
}
