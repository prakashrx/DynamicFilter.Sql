using System;
using Xunit;

namespace DynamicFilter.Sql.Tests
{
    public class BasicGrammerTest
    {
        [Fact]
        public void Should_Throw_No_Exceptions_While_Parsing()
        {
            FilterExpression.Compile<Object>("1");
            FilterExpression.Compile<Object>("a");
            FilterExpression.Compile<Object>("(a)");
            FilterExpression.Compile<Object>("(1)");
            FilterExpression.Compile<Object>("a < 1");
            FilterExpression.Compile<Object>("a > 1");
            FilterExpression.Compile<Object>("a <= 1");
            FilterExpression.Compile<Object>("a >= 1");
            FilterExpression.Compile<Object>("a = 1");
            FilterExpression.Compile<Object>("a == 1");

            FilterExpression.Compile<Object>("a = true");
            FilterExpression.Compile<Object>("a == true");
            FilterExpression.Compile<Object>("a = false");
            FilterExpression.Compile<Object>("a == false");
            FilterExpression.Compile<Object>("a != true");
            FilterExpression.Compile<Object>("a is null");
            FilterExpression.Compile<Object>("a IS NULL");
            FilterExpression.Compile<Object>("a IS null");
            FilterExpression.Compile<Object>("a is not null");
            FilterExpression.Compile<Object>("a IS not null");
            FilterExpression.Compile<Object>("a IS NOT NULL");

            FilterExpression.Compile<Object>("a = 'abc'");
            FilterExpression.Compile<Object>("a == 'abc'");

            FilterExpression.Compile<Object>("a like 'abc'");
            FilterExpression.Compile<Object>("a not like '%abc%'");

            FilterExpression.Compile<Object>("a in (1,2,3)");
            FilterExpression.Compile<Object>("a not in (1,2,3)");
            FilterExpression.Compile<Object>("a in ('1','2','3')");
            FilterExpression.Compile<Object>("a not in ('1','2','3')");

            FilterExpression.Compile<Object>("a == 1 or a == 2");
            FilterExpression.Compile<Object>("a == 1 and a == 2");
            FilterExpression.Compile<Object>("not (a == 1)");
            FilterExpression.Compile<Object>("(a == 1 or a ==2) or (b = 2)");
        }
    }
}
