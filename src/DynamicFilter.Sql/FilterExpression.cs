using Antlr4.Runtime;
using DynamicFilter.Sql.Grammer;
using DynamicFilter.Sql.Parser;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace DynamicFilter.Sql
{
    public class FilterExpression
    {
        public static Func<T, bool> Compile<T>(string filter)
        {
            if(string.IsNullOrWhiteSpace(filter))
                throw new ArgumentException("filter is empty", nameof(filter));

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
            var expression =  new DynamicFilterVisitor<T>().Visit(root);
            if(expression is Expression<Func<T, bool>> lambdaExpression)
            {
                return lambdaExpression.Compile();
            }
            throw new Exception("Unexpected Expression Type. Returned Expression is not of type: Func<T, bool>.");
        }
    }
}
