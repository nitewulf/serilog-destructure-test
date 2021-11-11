using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestructureLibrary
{
    public class ResultDestructuringPolicy : IDestructuringPolicy
    {
        public bool TryDestructure(object value, ILogEventPropertyValueFactory propertyValueFactory, out LogEventPropertyValue result)
        {
            if (value is IResultBase response)
            {
                result = propertyValueFactory.CreatePropertyValue(new { response.ReturnMessage, response.ReturnCode });
                return true;
            }

            result = null;
            return false;
        }
    }
}
