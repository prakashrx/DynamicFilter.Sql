using System.Collections.Generic;
using Xunit;

namespace DynamicFilter.Sql.Tests
{
    public partial class NullCheck
    {

        static Item item1 = new Item { Name = "John Alice" };
        static Item item2 = new Item { };

        public static IEnumerable<object[]> Data = new List<object[]>
        {
                new object[] { "Name is null", item1.ToJson(), false },
                new object[] { "Name is null", item2.ToJson(), true },
                new object[] { "Name is not null", item1.ToJson(), true },
                new object[] { "Name is not null", item2.ToJson(), false },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void Should_Evaluate_Object(string filter, string data, bool expected)
        {
            var item = data.FromJson<Item>();
            Assert.Equal(expected, FilterExpression.Compile<Item>(filter)(item));
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Should_Evaluate_Dictionary(string filter, string data, bool expected)
        {
            var dict = data.FromJson<Dictionary<string, object>>();
            Assert.Equal(expected, FilterExpression.Compile<Dictionary<string, object>>(filter)(dict));
        }
    }
}
