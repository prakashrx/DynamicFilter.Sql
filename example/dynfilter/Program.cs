

using DynamicFilter.Sql;
using dynfilter;

var filter = FilterExpression.Compile<User>("(Age > 10) and (Name like 'Alice%' or Name like 'Bob%')");

//Evaluate a single object
bool match = filter(new User { Age = 15, Name = "Alice Wonderland" });

Console.WriteLine($"Is Match? {match}");
