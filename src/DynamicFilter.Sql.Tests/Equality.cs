using System.Collections.Generic;
using Xunit;

namespace DynamicFilter.Sql.Tests
{
    public partial class Equality
    {

        static Item item1 = new Item { Id = 0, Name = "John", Value = 53.2, Active = true };
        static Item item2 = new Item { Id = 10, Name = "Alice", Value = 153.2, Active = true };
        static Item item3 = new Item { Id = 20, Name = "Bob", Value = 523.2, Active = false };
        static Item item4 = new Item { Id = 30, Name = "Dave", Value = 1.2, Active = false };
        static Item item5 = new Item { Id = -10, Name = "Eve", Value = -100.2, Active = true };

        public static IEnumerable<object[]> Data = new List<object[]>
        {
                new object[] { "Name = 'John'", item1.ToJson(), true },
                new object[] { "Name == 'John'", item1.ToJson(), true },
                new object[] { "Id = 20", item3.ToJson(), true },
                new object[] { "Id == 30", item4.ToJson(), true },
                new object[] { "Id = -10", item2.ToJson(), false },
                new object[] { "Id = -10", item5.ToJson(), true },
                new object[] { "Id = 10", item2.ToJson(), true },
                new object[] { "Value = 53.2", item1.ToJson(), true },
                new object[] { "Value == 153.2", item2.ToJson(), true },
                new object[] { "Value == 54.2", item2.ToJson(), false },
                new object[] { "Value == -100.2", item5.ToJson(), true },
        };

        [Theory]
        [InlineData("Value = 1.2")]
        [InlineData("Value == 100.234")]
        [InlineData("Value = -200.234")]
        [InlineData("Value == -240.234")]
        [InlineData("Active = true")]
        [InlineData("Active == true")]
        [InlineData("Active = false")]
        [InlineData("Active == false")]
        public void Should_Compile(string filter)
        {
            FilterExpression.Compile<Item>(filter);
        }

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
