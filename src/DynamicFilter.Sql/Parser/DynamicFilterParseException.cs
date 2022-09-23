using System;
using System.Collections.Generic;
using System.Linq;

namespace DynamicFilter.Sql.Parser
{
    public class DynamicFilterParseException : Exception
    {
        public DynamicFilterParseException(string message) : base(message)
        {
        }
        public DynamicFilterParseException(List<Error> errors) : base(string.Join(Environment.NewLine, errors.Select(x => x.ToString())))
        {
            Errors = errors;
        }

        public List<Error> Errors { get; set; } = new List<Error>();
    }
}
