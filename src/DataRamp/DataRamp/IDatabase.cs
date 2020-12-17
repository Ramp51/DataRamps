using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace DataRamp
{
    public interface IDatabase
    {
        IDbCommand GetStoredProcCommand(string storedProcedureName);

        Task<IEnumerable<TReturnType>> ExecuteCommandAsync<TReturnType>(IDbCommand cmd, IResultSetMapper<TReturnType> resultSetMapper);
        Task<IEnumerable<TReturnType>> ExecuteCommandAsync<TReturnType>(IDbCommand cmd, IRowMapper<TReturnType> rowMapper);
    }
}
