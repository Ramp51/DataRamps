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
        IDataReader ExecuteReader(IDbCommand dbCommand);
        IDataReader ExecuteReader(IDbCommand dbCommand, IDbTransaction transaction);
        IEnumerable<TReturnType> ExecuteResult<TReturnType>(IDbCommand dbCommand,
            IParameterMapper[] parameterMappers, IResultSetMapper<TReturnType> resultSetMapper,
            params object[] parameterValues);

        int ExecuteNonQuery(IDbCommand dbCommand);
        int ExecuteNonQuery(IDbCommand dbCommand, IDbTransaction transaction);

        void AddInParameter(IDbCommand dbCommand, string parameterName, DbType parameterType, object value);

        Task<IEnumerable<TReturnType>> ExecuteCommandAsync<TReturnType>(IDbCommand cmd, IResultSetMapper<TReturnType> resultSetMapper, IParameterMapper parameterMapper, params Object[] parameters);
        Task<IEnumerable<TReturnType>> ExecuteCommandAsync<TReturnType>(IDbCommand cmd, IRowMapper<TReturnType> rowMapper, IParameterMapper parameterMapper, params Object[] parameters);
    }
}
