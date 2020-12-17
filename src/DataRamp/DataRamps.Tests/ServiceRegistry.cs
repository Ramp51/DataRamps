using DataRamp;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataRamps.Tests
{
    public class ServiceRegistry
    {
        IServiceCollection services = null;

        public ServiceRegistry()
        {
            services = new ServiceCollection();
            services.AddSingleton(
                (new ConfigurationBuilder())
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build() as IConfiguration);
            services.AddLogging();
            services.AddScoped<DatabaseProviderFactory>();
            
        }

        public ServiceProvider GetScopedServiceProvider()
        {
            return services.BuildServiceProvider();
        }
    }
}
