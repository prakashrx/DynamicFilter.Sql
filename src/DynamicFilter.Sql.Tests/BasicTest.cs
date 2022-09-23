using System;
using Xunit;

namespace DynamicFilter.Sql.Tests
{
    public class Compile
    {
        [Fact]
        public void Should_Compile()
        {

            Assert.True(FilterExpression.Compile<Item>("Id = 0")(new Item { Id = 0 }));
            Assert.False(FilterExpression.Compile<Item>("Id = 0")(new Item { Id = 1 }));
            Assert.True(FilterExpression.Compile<Item>("Id == 0")(new Item { Id = 0 }));
            Assert.False(FilterExpression.Compile<Item>("Id == 0")(new Item { Id = 1 }));

            Assert.False(FilterExpression.Compile<Item>("Id != 0")(new Item { Id = 0 }));
            Assert.True(FilterExpression.Compile<Item>("Id != 0")(new Item { Id = 1 }));
            Assert.False(FilterExpression.Compile<Item>("Id <> 0")(new Item { Id = 0 }));
            Assert.True(FilterExpression.Compile<Item>("Id <> 0")(new Item { Id = 1 }));

            FilterExpression.Compile<Item>("Id < 1");
            FilterExpression.Compile<Item>("Id > 1");
            FilterExpression.Compile<Item>("Id <= 1");
            FilterExpression.Compile<Item>("Id >= 1");
            FilterExpression.Compile<Item>("Id = 1");
            FilterExpression.Compile<Item>("Id == 1");

            FilterExpression.Compile<Item>("Active = true");
            FilterExpression.Compile<Item>("Active == true");
            FilterExpression.Compile<Item>("Active = false");
            FilterExpression.Compile<Item>("Active == false");
            FilterExpression.Compile<Item>("Active != true");
            FilterExpression.Compile<Item>("Name is null");
            FilterExpression.Compile<Item>("Name IS NULL");
            FilterExpression.Compile<Item>("Name IS null");
            FilterExpression.Compile<Item>("Name is not null");
            FilterExpression.Compile<Item>("Name IS not null");
            FilterExpression.Compile<Item>("Name IS NOT NULL");

            FilterExpression.Compile<Item>("Name = 'abc'");
            FilterExpression.Compile<Item>("Name == 'abc'");

            Assert.True(FilterExpression.Compile<Item>("Name like 'A%'")(new Item { Name = "ABC"}));
            Assert.False(FilterExpression.Compile<Item>("Name like 'A%'")(new Item { Name = "BCD" }));
            Assert.True(FilterExpression.Compile<Item>("Name not like 'A%'")(new Item { Name = "DEF" }));
            Assert.False(FilterExpression.Compile<Item>("Name not like 'A%'")(new Item { Name = "ABC" }));


            Assert.True(FilterExpression.Compile<Item>("Id in (1,2,3)")(new Item { Id = 2 }));
            Assert.True(FilterExpression.Compile<Item>("Id not in (1,2,3)")(new Item { Id = 4 }));
            Assert.True(FilterExpression.Compile<Item>("Name in ('a','b','c')")(new Item { Name = "c" }));
            Assert.True(FilterExpression.Compile<Item>("Name not in ('a','b','c')")(new Item { Name = "d" }));

            FilterExpression.Compile<Item>("Id == 1 or Id == 2");
            FilterExpression.Compile<Item>("Id == 1 and Id == 2");
            FilterExpression.Compile<Item>("not (Id == 1)");
            FilterExpression.Compile<Item>("(Id == 1 or Id ==2) or (Id = 2)");
        }
    }
}
