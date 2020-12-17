using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DataRamp
{
    public class DatabaseProviderFactory
    {
        private readonly IConfiguration config;
        private readonly ILogger<DatabaseProviderFactory> logger;
        private const string StandardDefaultConnectionProviderType = "System.Data.SqlClient";

        public DatabaseProviderFactory(IConfiguration config, ILogger<DatabaseProviderFactory> logger)
        {
            this.config = config ?? throw new ArgumentNullException(nameof(config));
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));

            if (!DbProviderFactories.TryGetFactory(StandardDefaultConnectionProviderType, out DbProviderFactory factory))
            {
                DbProviderFactories.RegisterFactory(StandardDefaultConnectionProviderType, Microsoft.Data.SqlClient.SqlClientFactory.Instance);
            }
        }

        public IDatabase CreateDefault()
        {
            string defaultConnectionString = config.GetConnectionString("DefaultConnection");
            string defaultConnectionProviderType = config.GetConnectionString("DefaultConnection_ProviderName");
            var names = DbProviderFactories.GetProviderInvariantNames();
            bool haveConnectionString = !string.IsNullOrWhiteSpace(defaultConnectionString);
            bool haveConnectionProviderType = !string.IsNullOrWhiteSpace(defaultConnectionProviderType);

            if (haveConnectionString && !haveConnectionProviderType)
            {
                defaultConnectionProviderType = StandardDefaultConnectionProviderType;
            }

            DbProviderFactory factory = DbProviderFactories.GetFactory(defaultConnectionProviderType);

            IDbConnection connection = factory.CreateConnection();
            connection.ConnectionString = defaultConnectionString;
            
            return new Database(connection, factory);
        }
    }
}
