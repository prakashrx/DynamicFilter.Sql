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
        [Theory]
        [InlineData("true")]
        [InlineData("false")]
        [InlineData("1")]
        [InlineData("0")]
        [InlineData("-1")]
        [InlineData("1.2")]
        [InlineData("0.0")]
        [InlineData("Active")]
        [InlineData("Value")]
        [InlineData("Id")]
        public void Should_Compile(string filter)
        {
            FilterExpression.Compile<Item>(filter);
        }

        [Theory]
        [InlineData("true")]
        [InlineData("1")]
        [InlineData("-1")]
        [InlineData("1.2")]
        public void Should_Evaluate_True(string filter)
        {
            Assert.True(FilterExpression.Compile<Item>(filter)(null));
        }

        [Theory]
        [InlineData("false")]
        [InlineData("0")]
        [InlineData("0.0")]
        public void Should_Evaluate_False(string filter)
        {
            Assert.False(FilterExpression.Compile<Item>(filter)(null));
        }

        [Theory]
        [InlineData("")]
        [InlineData(" ")]
        [InlineData(null)]
        public void Should_Throw_Exception(string filter)
        {
            Assert.Throws<ArgumentException>("filter", () => FilterExpression.Compile<Item>(filter)(null));
        }
    }
}
