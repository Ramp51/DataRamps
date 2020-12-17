using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace DataRamp
{
    public interface IRowMapper<TReturnType>
    {
        TReturnType MapRow(IDataRecord row);
    }
}
