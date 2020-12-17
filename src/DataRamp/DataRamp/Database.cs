using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace DataRamp
{
    public class Database
        : IDatabase
    {
        private readonly IDbConnection connection;
        private readonly DbProviderFactory factory;

        public Database(IDbConnection connection, DbProviderFactory factory)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
            this.factory = factory ?? throw new ArgumentNullException(nameof(factory));
        }

        public async Task<IEnumerable<TReturnType>> ExecuteCommandAsync<TReturnType>(IDbCommand cmd, IResultSetMapper<TReturnType> resultSetMapper)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TReturnType>> ExecuteCommandAsync<TReturnType>(IDbCommand cmd, IRowMapper<TReturnType> rowMapper)
        {
            throw new NotImplementedException();
        }

        public IDbCommand GetStoredProcCommand(string storedProcedureName)
        {
            IDbCommand command = connection.CreateCommand();
            
            command.CommandText = storedProcedureName;
            command.CommandType = CommandType.StoredProcedure;

            return command;
        }
    }
}
