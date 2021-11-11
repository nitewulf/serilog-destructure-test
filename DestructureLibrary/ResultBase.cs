using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DestructureLibrary
{
    public abstract class ResultBase<TReturnObject, TEnum> : IResultBase where TEnum : struct, IComparable, IFormattable, IConvertible
    {
        public TReturnObject ReturnObject { get; set; }
        public TEnum ReturnCode { get; set; }
        public string ReturnMessage { get; set; }
        object IResultBase.ReturnObject => ReturnObject;
        string IResultBase.ReturnCode => ReturnCode.ToString();
    }
}
