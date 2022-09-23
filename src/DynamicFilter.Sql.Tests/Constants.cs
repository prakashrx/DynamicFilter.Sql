using DynamicFilter.Sql.Parser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace DynamicFilter.Sql.Tests
{
    public class Constants
    {
        [Fact]
        public void Should_Compile()
        {
            FilterExpression.Compile<Item>("true");
            FilterExpression.Compile<Item>("false");
            FilterExpression.Compile<Item>("1");
            FilterExpression.Compile<Item>("0");
            FilterExpression.Compile<Item>("-1");
            FilterExpression.Compile<Item>("0");
            FilterExpression.Compile<Item>("1.2");
            FilterExpression.Compile<Item>("0.0");
        }

        [Fact]
        public void Should_Evaluate_True()
        {
            Assert.True(FilterExpression.Compile<Item>("true")(null));
            Assert.True(FilterExpression.Compile<Item>("1")(null));
            Assert.True(FilterExpression.Compile<Item>("-1")(null));
            Assert.True(FilterExpression.Compile<Item>("1.2")(null));
        }

        [Fact]
        public void Should_Evaluate_False()
        {
            Assert.False(FilterExpression.Compile<Item>("false")(null));
            Assert.False(FilterExpression.Compile<Item>("0")(null));
            Assert.False(FilterExpression.Compile<Item>("0")(null));
            Assert.False(FilterExpression.Compile<Item>("0.0")(null));
        }

        [Fact]
        public void Should_Throw_Exception()
        {
            Assert.Throws<ArgumentException>("filter", () => FilterExpression.Compile<Item>(string.Empty)(null) );
            Assert.Throws<ArgumentException>("filter", () => FilterExpression.Compile<Item>(" ")(null));
            Assert.Throws<ArgumentException>("filter", () => FilterExpression.Compile<Item>(null)(null));
        }
    }
}
