using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace DynamicFilter.Sql.Tests
{
    public partial class Logic
    {
        class Person
        {
            public int Id  { get; set; }
            public string Name { get; set; }
            public int Age { get; set; }
            public double Height { get; set; }
            public bool Alive { get; set; }
        }

        static List<Person> persons = new List<Person>
        {
            new Person {Id = 1, Name = "John Alice", Age=21, Height=170.2, Alive = true },
            new Person {Id = 2,  Name = "David Alexander", Age=27, Height=178.1, Alive = true },
            new Person {Id = 3,  Name = "Nancy Stark", Age=32, Height=172.3, Alive = true },
            new Person {Id = 4,  Name = "Eve Ma", Age=65, Height=185.0, Alive = false },
            new Person {Id = 5,  Name = "Anna Walsh", Age=50, Height=160.5, Alive = true },
            new Person {Id = 6,  Name = "Hannah Walton", Age=33, Height=170.0, Alive = true },
            new Person {Id = 7,  Name = "Brandon Olsen", Age=40, Height=182.6, Alive = false },
            new Person {Id = 8,  Name = "Brad Wilson", Age=80, Height=165.3, Alive = true },
            new Person {Id = 9,  Name = "John Doe", Age=15, Height=145.0, Alive = true },
            new Person {Id = 10,  Name = "Roger Romero", Age=9, Height=110.3, Alive = true },
        };

        static List<Dictionary<string, object>> persons_dict = persons.Select(p => p.ToJson().FromJson<Dictionary<string, object>>()).ToList();

        public static IEnumerable<object[]> Data = new List<object[]>
        {
                new object[] { "Name like 'John%' and Age < 20", 9 },
                new object[] { "Name like 'John%' and Alive = true", 1, 9 },
                new object[] { "Alive = true and Age < 20", 9, 10 },

                new object[] { "not (Alive = true)", 4, 7 },
                new object[] { "not (Age < 30)", 3, 4, 5, 6, 7, 8 },
                new object[] { "(not (Age < 30)) and Name like '%W%'", 5, 6, 8 },

                new object[] { "Alive = false or Age < 20", 4, 7, 9, 10 },
                new object[] { "not (Alive = true) or Age < 20", 4, 7, 9, 10 },

        };

        [Theory]
        [MemberData(nameof(Data))]
        public void Should_Evaluate_Object(string filter, params int[] expected)
        {
            var expectedArray = Enumerable.Range(1, 10).Select(x => expected.Contains(x)).ToArray();
            var filterFunc = FilterExpression.Compile<Person>(filter);
            var actual = Enumerable.Range(0, 10).Select(i => filterFunc(persons[i])).ToArray();
            Assert.All(Enumerable.Range(0, 10), (i) => Assert.Equal(expectedArray[i], actual[i] ));
        }

        [Theory]
        [MemberData(nameof(Data))]
        public void Should_Evaluate_Dictionary(string filter, params int[] expected)
        {
            var expectedArray = Enumerable.Range(1, 10).Select(x => expected.Contains(x)).ToArray();
            var filterFunc = FilterExpression.Compile<Dictionary<string, object>>(filter);
            var actual = Enumerable.Range(0, 10).Select(i => filterFunc(persons_dict[i])).ToArray();
            Assert.All(Enumerable.Range(0, 10), (i) => Assert.Equal(expectedArray[i], actual[i]));
        }
    }
}
