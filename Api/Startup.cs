﻿using AccessControl.Models;
using AccessControl.Services;
using Microsoft.IdentityModel.Tokens;
using ServicesApi.Services;
using System.Text;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Api
{
    public class Startup
    {

        private readonly IConfiguration _configuration;
        public Startup(IConfiguration configuration) => _configuration = configuration;
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews();
            services.AddCors(o => o.AddPolicy("CorsPolicy", b => b.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddScoped<IApiService, ApiService>();
            services.AddScoped<IJwtHandler, JwtHandler>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<ICredentialManager, CredentialManager>();
            services.AddScoped<IJwtHandler, JwtHandler>();
            services.AddHttpContextAccessor();
            services.AddControllers();

    
            var jwtSection = _configuration.GetSection("jwt");
            var jwtOptions = new JwtOptions();

            jwtSection.Bind(jwtOptions);
            services.AddAuthentication()
               .AddJwtBearer(cfg =>
               {
                   cfg.TokenValidationParameters = new TokenValidationParameters
                   {
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey)),
                       ValidIssuer = jwtOptions.Issuer,
                       ValidateAudience = jwtOptions.ValidateAudience,
                       ValidateLifetime = jwtOptions.ValidateLifetime
                   };
               });
            services.Configure<JwtOptions>(jwtSection);
            services.Configure<AccessOptions>(_configuration.GetSection("access"));
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Configure the HTTP request pipeline.
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();
            app.UseRouting(); 
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseMiddleware<TokenMiddleware>();
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });


        }

       
    }
}
