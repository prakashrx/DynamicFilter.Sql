using System;
using Xunit;

namespace DynamicFilter.Sql.Tests
{
    public class BasicGrammerTest
    {
        [Fact]
        public void Should_Throw_No_Exceptions_While_Parsing()
        {
            Generator generator = new Generator();

            generator.Generate("1");
            generator.Generate("a");
            generator.Generate("(a)");
            generator.Generate("(1)");
            generator.Generate("a < 1");
            generator.Generate("a > 1");
            generator.Generate("a <= 1");
            generator.Generate("a >= 1");
            generator.Generate("a = 1");
            generator.Generate("a == 1");

            generator.Generate("a = true");
            generator.Generate("a == true");
            generator.Generate("a = false");
            generator.Generate("a == false");
            generator.Generate("a != true");
            generator.Generate("a is null");
            generator.Generate("a IS NULL");
            generator.Generate("a IS null");
            generator.Generate("a is not null");
            generator.Generate("a IS not null");
            generator.Generate("a IS NOT NULL");

            generator.Generate("a = 'abc'");
            generator.Generate("a == 'abc'");

            generator.Generate("a like 'abc'");
            generator.Generate("a not like '%abc%'");

            generator.Generate("a in (1,2,3)");
            generator.Generate("a not in (1,2,3)");
            generator.Generate("a in ('1','2','3')");
            generator.Generate("a not in ('1','2','3')");

            generator.Generate("a == 1 or a == 2");
            generator.Generate("a == 1 and a == 2");
            generator.Generate("not (a == 1)");
            generator.Generate("(a == 1 or a ==2) or (b = 2)");
        }
    }
}
