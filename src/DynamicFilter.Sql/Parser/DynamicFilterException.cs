using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicFilter.Sql.Parser
{
    public class DynamicFilterException : Exception 
    {
        public DynamicFilterException(string message) : base(message) {}
    }
}
