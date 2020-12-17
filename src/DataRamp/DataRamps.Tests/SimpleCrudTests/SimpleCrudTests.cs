using DataRamp;
using DataRamps.Tests.TestingModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Xunit;

namespace DataRamps.Tests.SimpleCrudTests
{
    
    public class SimpleCrudTests
    {
        [Fact]
        public void Should_CreateAVehicle_WithAStoredProcCommand()
        {
            ServiceRegistry registry = new ServiceRegistry();
            ServiceProvider services = registry.GetScopedServiceProvider();

            DatabaseProviderFactory factory = services.GetRequiredService<DatabaseProviderFactory>();

            IDatabase db = factory.CreateDefault();

            Vehicle fordVehicle = new Vehicle()
            {
                Manufacturer = "Ford"
            };

            using(IDbCommand cmd = db.GetStoredProcCommand("dbo.MergeVehicles"))
            {
                
            }

        }   
    }
}
