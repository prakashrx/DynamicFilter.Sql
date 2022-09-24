using System.Collections.Generic;
using Xunit;

namespace DynamicFilter.Sql.Tests
{
    public partial class Contains
    {

        static Item item1 = new Item { Name = "John" };
        static Item item2 = new Item { Name = "Alice" };
        static Item item3 = new Item { Id = 1 };
        static Item item4 = new Item { Value = 10.5};

        public static IEnumerable<object[]> Data = new List<object[]>
        {
                new object[] { "Name in ('John', 'Alice')", item1.ToJson(), true },
                new object[] { "Name in ('John', 'Alice')", item2.ToJson(), true },
                new object[] { "Name in ('Bob', 'Alice')", item1.ToJson(), false },
                new object[] { "Name in ('John', 'Bob')", item2.ToJson(), false },
                new object[] { "Name in ('John', 'Alice')", item3.ToJson(), false},

                new object[] { "Id in (0, 1)", item1.ToJson(), true},
                new object[] { "Id in (0, 1, 2)", item2.ToJson(), true},
                new object[] { "Id in (0, 1, 2)", item3.ToJson(), true},
                new object[] { "Id in (1,2)", item1.ToJson(), false},
                new object[] { "Id in (1,2)", item2.ToJson(), false},
                new object[] { "Id in (2,3)", item3.ToJson(), false},

                new object[] { "Value in (0.0, 0.1, 10.5)", item1.ToJson(), true},
                new object[] { "Value in (0.0, 0.1, 10.5)", item2.ToJson(), true},
                new object[] { "Value in (0.0, 0.1, 10.5)", item3.ToJson(), true},
                new object[] { "Value in (0.0, 0.1, 10.5)", item4.ToJson(), true},
                new object[] { "Value in (0.1, 10.5)", item1.ToJson(), false},
                new object[] { "Value in (0.1, 23.5)", item4.ToJson(), false},

                new object[] { "Name not in ('John', 'Alice')", item1.ToJson(), false },
                new object[] { "Name not in ('John', 'Alice')", item2.ToJson(), false },
                new object[] { "Name not in ('Bob', 'Alice')", item1.ToJson(), true },
                new object[] { "Name not in ('John', 'Bob')", item2.ToJson(), true },
                new object[] { "Name not in ('John', 'Alice')", item3.ToJson(), true},

                new object[] { "Id not in (0, 1)", item1.ToJson(), false},
                new object[] { "Id not in (0, 1, 2)", item2.ToJson(), false},
                new object[] { "Id not in (0, 1, 2)", item3.ToJson(), false},
                new object[] { "Id not in (1,2)", item1.ToJson(), true},
                new object[] { "Id not in (1,2)", item2.ToJson(), true},
                new object[] { "Id not in (2,3)", item3.ToJson(), true},

                new object[] { "Value not in (0.0, 0.1, 10.5)", item1.ToJson(), false},
                new object[] { "Value not in (0.0, 0.1, 10.5)", item2.ToJson(), false},
                new object[] { "Value not in (0.0, 0.1, 10.5)", item3.ToJson(), false},
                new object[] { "Value not in (0.0, 0.1, 10.5)", item4.ToJson(), false},
                new object[] { "Value not in (0.1, 10.5)", item1.ToJson(), true},
                new object[] { "Value not in (0.1, 23.5)", item4.ToJson(), true},

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
