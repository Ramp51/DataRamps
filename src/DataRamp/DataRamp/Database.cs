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

        public async Task<IEnumerable<TReturnType>> ExecuteCommandAsync<TReturnType>(IDbCommand cmd, IResultSetMapper<TReturnType> resultSetMapper, IParameterMapper parameterMapper, params Object[] parameters)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TReturnType>> ExecuteCommandAsync<TReturnType>(IDbCommand cmd, IRowMapper<TReturnType> rowMapper, IParameterMapper parameterMapper, params Object[] parameters)
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

        /// <summary>
        /// Executes the command and returns an IDataReader through which the result can be read. It is the responsibility of the caller to close the connection and reader when finished.
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(IDbCommand dbCommand)
        {
            connection.Open();

            return dbCommand.ExecuteReader();
        }

        public IEnumerable<TReturnType> ExecuteResult<TReturnType>(IDbCommand dbCommand,
            IParameterMapper[] parameterMappers, IResultSetMapper<TReturnType> resultSetMapper,
            params object[] parameterValues)
        {
            for(int idxParameterMapper = 0; idxParameterMapper < parameterMappers.Length; idxParameterMapper++)
            {
                //todo: enhance this to ask the parametter mapper how many parameters it expects
                //      then consume that number of parameters from the parameterValues array
                parameterMappers[idxParameterMapper].AssignParameters(dbCommand, parameterValues[idxParameterMapper]);
            }

            IEnumerable<TReturnType> results;

            connection.Open();
            try
            {
                using (IDataReader reader = dbCommand.ExecuteReader())
                {
                    results = resultSetMapper.MapSet(reader);
                }
            }
            finally
            {
                connection.Close();
            }

            return results;
        }

        /// <summary>
        /// Executes the command within a transaction and returns an IDataReader through which the result can be read. It is the responsibility of the caller to close the connection and reader when finished.
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(IDbCommand dbCommand, IDbTransaction transaction)
        {
            dbCommand.Transaction = transaction;

            return ExecuteReader(dbCommand);
        }

        /// <summary>
        /// Executes the command and returns the number of rows affected.
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(IDbCommand dbCommand)
        {
            connection.Open();

            return dbCommand.ExecuteNonQuery();
        }

        /// <summary>
        /// Executes the command within the given transaction, and returns the number of rows affected.
        /// </summary>
        /// <param name="dbCommand"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(IDbCommand dbCommand, IDbTransaction transaction)
        {
            dbCommand.Transaction = transaction;

            return ExecuteNonQuery(dbCommand);
        }

        public void AddInParameter(IDbCommand dbCommand, string parameterName, DbType parameterType, object value)
        {
            IDbDataParameter param = dbCommand.CreateParameter();
            param.ParameterName = parameterName;
            param.DbType = parameterType;
            param.Value = value;
            param.Direction = ParameterDirection.Input;

            dbCommand.Parameters.Add(param);

        }


        //todo: design methods based on this https://docs.microsoft.com/en-us/previous-versions/msp-n-p/bb744807(v=pandp.31)?redirectedfrom=MSDN
    }
}
