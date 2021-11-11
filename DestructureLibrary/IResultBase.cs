using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestructureLibrary
{
    public interface IResultBase
    {
        public string ReturnMessage { get; }
        public object ReturnObject { get; }
        public string ReturnCode { get; }
    }
}
