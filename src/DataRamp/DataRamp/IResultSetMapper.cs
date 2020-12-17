using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataRamp
{
    public interface IResultSetMapper<TReturnType>
    {
        IEnumerable<TReturnType> MapSet(IDataReader reader);
    }
}
