using Antlr4.Runtime;
using DynamicFilter.Sql.Grammer;
using DynamicFilter.Sql.Parser;
using System;
using System.Linq;

namespace DynamicFilter.Sql
{
    public class Generator
    {
        public object? Generate(string filter) 
        {
            var inputStream = new AntlrInputStream(filter);
            var lexer = new DynamicFilterLexer(inputStream);
            var parser = new DynamicFilterParser(new CommonTokenStream(lexer));
            var listener = new DynamicFilterErrorListener();
            parser.AddErrorListener(listener);
            var root = parser.root();
            if (listener.Errors.Count > 0)
            {
                throw new DynamicFilterParseException(listener.Errors);
            }
            return new DynamicFilterVisitor().Visit(root);
        }
    }
}
