using Antlr4.Runtime.Misc;
using DynamicFilter.Sql.Grammer;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DynamicFilter.Sql.Parser
{
    internal class DynamicFilterVisitor<T> : DynamicFilterBaseVisitor<Expression>
    {
        public override Expression VisitRoot([NotNull] DynamicFilterParser.RootContext context)
        {
            var expression = VisitExpr(context.expr());
            expression = expression.CanReduce ? expression.Reduce() : expression;

            var lambdaParameter = Expression.Parameter(typeof(T), "object");
            return Expression.Lambda<Func<T, bool>>(expression, lambdaParameter);
        }

        public override Expression VisitExpr([NotNull] DynamicFilterParser.ExprContext context)
        {
            return Expression.Constant(true);
        }
    }
}
