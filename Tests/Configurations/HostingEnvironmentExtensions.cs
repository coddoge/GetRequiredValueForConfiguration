using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace GetRequiredValueForConfiguration.Test.Configurations
{
    public static class HostingEnvironmentExtensions
    {
        public static IConfigurationRoot GetAppConfiguration(this IHostEnvironment env)
        {
            return AppConfigurations.Get(env.ContentRootPath, env.EnvironmentName, env.IsDevelopment());
        }
    }
}
