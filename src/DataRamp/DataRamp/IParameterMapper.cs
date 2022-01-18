using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;

namespace DataRamp
{
    public interface IParameterMapper
    {
        void AssignParameters(IDbCommand command, params Object[] parameterValues);
    }
}
