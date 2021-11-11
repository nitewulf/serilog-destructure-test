using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestructureLibrary
{
    public class TestResult : ResultBase<object, ResultReturnCode>
    {
        public TestResult(ResultReturnCode returnCode, string returnMessage, object returnObject = null)
        {
            ReturnMessage = returnMessage;
            ReturnObject = returnObject;
            ReturnCode = returnCode;
        }
    }
    public class TestResult<TReturnObject> : ResultBase<TReturnObject, ResultReturnCode>
    {
        public TestResult(ResultReturnCode returnCode, string returnMessage, TReturnObject returnObject)
        {
            ReturnMessage = returnMessage;
            ReturnObject = returnObject;
            ReturnCode = returnCode;
        }
    }
}
