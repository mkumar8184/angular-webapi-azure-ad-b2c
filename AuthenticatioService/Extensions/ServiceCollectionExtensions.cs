
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;
using Microsoft.Extensions.Configuration;

namespace AuthenticatioService.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthService(
           this IServiceCollection services,   IConfiguration configuration
          )
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
          .AddMicrosoftIdentityWebApi(options =>
          {
              configuration.Bind("AzureADB2C", options);

              options.TokenValidationParameters.NameClaimType = "name";
          },
           options => { configuration.Bind("AzureADB2C", options); });

            return services;
        }
        public static IServiceCollection InjectOtherSerives(
           this IServiceCollection services
          )
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();          
            return services;
        }
    }
}
