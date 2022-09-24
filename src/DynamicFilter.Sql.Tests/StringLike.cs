using System.Collections.Generic;
using Xunit;

namespace DynamicFilter.Sql.Tests
{
    public partial class StringLike
    {

        static Item item1 = new Item { Id = 0, Name = "John Alice", Value = 53.2, Active = true };
        static Item item2 = new Item { Id = 10, Name = "Alice Bob", Value = 153.2, Active = true };
        static Item item3 = new Item { Id = 20, Name = "Bob Josh.", Value = 523.2, Active = false };
        static Item item4 = new Item { Id = 30, Name = "Dave. John", Value = 1.2, Active = false };

        public static IEnumerable<object[]> Data = new List<object[]>
        {
                new object[] { "Name like 'John Alice'", item1.ToJson(), true },
                new object[] { "Name like 'Alice%'", item2.ToJson(), true },
                new object[] { "Name like '%Bob'", item2.ToJson(), true },
                new object[] { "Name like '%.%'", item4.ToJson(), true },
                new object[] { "Name like 'Dave_ John'", item4.ToJson(), true },
                new object[] { "Name like '_lice _ob'", item2.ToJson(), true },

                new object[] { "Name not like 'John Alice'", item2.ToJson(), true },
                new object[] { "Name not like '%.%'", item1.ToJson(), true },
                new object[] { "Name not like '%.%'", item3.ToJson(), false },
                new object[] { "Name not like '%.%'", item4.ToJson(), false },
                new object[] { "Name not like 'Dave_ John'", item4.ToJson(), false },
                new object[] { "Name not like '%John%'", item3.ToJson(), true },
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
