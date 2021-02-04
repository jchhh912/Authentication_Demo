using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication_Demo
{
    public static class AuthenticationServiceCollectionExtensions
    {
        public static void AddMicrosoftAuthenticaton(this IServiceCollection services, IConfiguration configuration)
        {
            var authentication = new AzureAdOption();
            configuration.Bind("AzureAd", authentication);
            services.Configure<AzureAdOption>(configuration.GetSection("AzureAd"));
            services.Configure<AzureAdOption>(option =>
            {
                option.CallbackPath = authentication.CallbackPath;
                option.ClientId = authentication.ClientId;
                option.Domain = authentication.Domain;
                option.Instance = authentication.Instance;
                option.TenantId = authentication.TenantId;
            }).AddSingleton<IConfigureOptions<OpenIdConnectOptions>, ConfigureAzureOptions>();
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            }).AddOpenIdConnect().AddCookie();
        }
        public class ConfigureAzureOptions : IConfigureNamedOptions<OpenIdConnectOptions>
        {
            private readonly AzureAdOption _azureOptions;

            public ConfigureAzureOptions(IOptions<AzureAdOption> azureOptions)
            {
                _azureOptions = azureOptions.Value;
            }

            public void Configure(string name, OpenIdConnectOptions options)
            {
                options.ClientId = _azureOptions.ClientId;
                options.Authority = $"{_azureOptions.Instance}{_azureOptions.TenantId}";
                options.UseTokenLifetime = true;
                options.CallbackPath = _azureOptions.CallbackPath;
                options.RequireHttpsMetadata = false;
            }

            public void Configure(OpenIdConnectOptions options)
            {
                Configure(Options.DefaultName, options);
            }
        }
    }
}
