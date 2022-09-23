using Antlr4.Runtime.Misc;
using DynamicFilter.Sql.Grammer;
using DynamicFilter.Sql.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace DynamicFilter.Sql.Parser
{
    internal class DynamicFilterVisitor<T> : DynamicFilterBaseVisitor<Expression>
    {
        private Type type_ = typeof(T);
        private ParameterExpression lambdaParameter_ = Expression.Parameter(typeof(T), "object");
        private static readonly MethodInfo ConvertInteger32ToInt64 = typeof(Convert).GetMethod(nameof(Convert.ToInt64), new[] { typeof(Int32) });
        private static readonly MethodInfo ConvertInteger32ToBool = typeof(Convert).GetMethod(nameof(Convert.ToBoolean), new[] { typeof(Int32) });
        private static readonly MethodInfo ConvertInteger64ToBool = typeof(Convert).GetMethod(nameof(Convert.ToBoolean), new [] { typeof(Int64) } );
        private static readonly MethodInfo ConvertDoubleToBool = typeof(Convert).GetMethod(nameof(Convert.ToBoolean), new [] { typeof(double) } );
        private static readonly MethodInfo SqlLike = typeof(SqlLikeUtility).GetMethod(nameof(SqlLikeUtility.SqlLike));
        public override Expression VisitRoot([NotNull] DynamicFilterParser.RootContext context)
        {
            var expression = base.Visit(context.expr());
            expression = expression.CanReduce ? expression.Reduce() : expression;
            if (expression.Type == typeof(Int32))
            {
                expression = Expression.Call(ConvertInteger32ToBool, expression);
            }
            if (expression.Type == typeof(Int64)) 
            {
                expression = Expression.Call(ConvertInteger64ToBool, expression);
            } 
            else if (expression.Type == typeof(double))
            {
                expression = Expression.Call(ConvertDoubleToBool, expression);
            }
            return Expression.Lambda<Func<T, bool>>(expression, lambdaParameter_);
        }

        public override Expression VisitParanexpr([NotNull] DynamicFilterParser.ParanexprContext context)
        {
            return base.Visit(context.expr());
        }

        public override Expression VisitNotExpr([NotNull] DynamicFilterParser.NotExprContext context)
        {
            return Expression.Not(base.Visit(context.expr()));
        }

        public override Expression VisitLogicExpr([NotNull] DynamicFilterParser.LogicExprContext context)
        {
            var left = base.Visit(context.left);
            var right = base.Visit(context.right);

            if(context.operation.OR() is not null) 
            {
                return Expression.OrElse(left, right);
            } 
            else if(context.operation.AND() is not null)
            {
                return Expression.AndAlso(left, right);
            }
            throw new DynamicFilterException("Invalid binary operation found while building expression");
        }

        public override Expression VisitNullExpr([NotNull] DynamicFilterParser.NullExprContext context)
        {
            var left =  base.Visit(context.left);
            if(context.operation.ISNULL() is not null)
                return Expression.Equal(left, Expression.Constant(null));
            else if(context.operation.ISNOTNULL() is not null)
                return Expression.Equal(left, Expression.Constant(null));
            throw new DynamicFilterException("Invalid null equality operation found while building expression");
        }

        public override Expression VisitCompareExpr([NotNull] DynamicFilterParser.CompareExprContext context)
        {
            var left = base.Visit(context.left);
            var right = base.Visit(context.right);

            return context.operation.GetText().ToLower() switch 
            {
                "=" or "==" => Expression.Equal(CastExpression(left, right.Type), right),
                "!=" or "<>" => Expression.NotEqual(CastExpression(left, right.Type), right),
                ">" => Expression.GreaterThan(CastExpression(left, right.Type), right),
                ">=" => Expression.GreaterThanOrEqual(CastExpression(left, right.Type), right),
                "<" => Expression.LessThan(CastExpression(left, right.Type), right),
                "<=" => Expression.LessThanOrEqual(CastExpression(left, right.Type), right),
                _ => throw new DynamicFilterException("Invalid comparison operation found while building expression")
            };
        }
        public override Expression VisitCompareLikeExpr([NotNull] DynamicFilterParser.CompareLikeExprContext context)
        {
            var left = base.Visit(context.left);
            var right = context.right.Text.Trim('\'');

            if (context.operation.LIKE() is not null)
                return Expression.Call(SqlLike, Expression.Constant(right), left);
            else if (context.operation.NOTLIKE() is not null)
                return Expression.Not(Expression.Call(SqlLike, Expression.Constant(right), left));

            throw new DynamicFilterException("Invalid like operation found while building expression");
        }
        public override Expression VisitListExpr([NotNull] DynamicFilterParser.ListExprContext context)
        {
            var left = base.Visit(context.left);
            var right = base.Visit(context.right);

            if (context.operation.IN() is not null)
                return Expression.Call(right, right.Type.GetMethod("Contains"), CastExpression(left, right.Type.GenericTypeArguments[0]));
            else if (context.operation.NOTIN() is not null)
                return Expression.Not(Expression.Call(right, right.Type.GetMethod("Contains"), CastExpression(left, right.Type.GenericTypeArguments[0])));

            throw new DynamicFilterException("Invalid list operation found while building expression");
        }

        public override Expression VisitListValue([NotNull] DynamicFilterParser.ListValueContext context)
        {
            var expressions = context.children.OfType<DynamicFilterParser.ConstantContext>().Select(base.Visit).Cast<ConstantExpression>();
            switch(expressions.First().Type)
            {
                case Type t when t == typeof(Int64):
                    return Expression.Constant(new HashSet<Int64>(expressions.Select(x => (Int64)x.Value)));
                case Type t when t == typeof(double):
                    return Expression.Constant(new HashSet<double>(expressions.Select(x => (double)x.Value)));
                case Type t when t == typeof(bool):
                    return Expression.Constant(new HashSet<bool>(expressions.Select(x => (bool)x.Value)));
                case Type t when t == typeof(string):
                    return Expression.Constant(new HashSet<string>(expressions.Select(x => (string)x.Value)));
                default:
                    throw new DynamicFilterException("Invalid list constant type found while building expression");
            }
        }

        public override Expression VisitBooleanValue([NotNull] DynamicFilterParser.BooleanValueContext context)
        {
            return Expression.Constant(bool.Parse(context.GetText()));
        }

        public override Expression VisitStringValue([NotNull] DynamicFilterParser.StringValueContext context)
        {
            return Expression.Constant(context.GetText().Trim('\''));
        }
        public override Expression VisitIntegerValue([NotNull] DynamicFilterParser.IntegerValueContext context)
        {
            return Expression.Constant(Int64.Parse(context.GetText()));
        }
        public override Expression VisitDecimalValue([NotNull] DynamicFilterParser.DecimalValueContext context)
        {
            return Expression.Constant(double.Parse(context.GetText()));
        }
        public override Expression VisitIdentifierExpr([NotNull] DynamicFilterParser.IdentifierExprContext context)
        {
            var identifier = context.IDENTIFIER().GetText();
            if(type_ == typeof(Dictionary<string, object>))
                return Expression.Property(lambdaParameter_, "Item", Expression.Constant(identifier));
            else if(type_.GetProperty(identifier) != null)
                return Expression.PropertyOrField(lambdaParameter_, identifier);
            return Expression.Constant(null);
        }

        Expression CastExpression(Expression expression,
                                  Type type)
        {
            if(expression.Type == typeof(Int32))
                return Expression.Call(ConvertInteger32ToInt64, expression);

            return expression.Type == typeof(object) ? Expression.Convert(expression, type) : expression;
        }
    }
}
