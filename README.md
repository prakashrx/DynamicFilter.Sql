# DynamicFilter.Sql

A .Net Nuget Library to generate Lambda Expressions dynamically based on a Sql Based Filter criteria.

```csharp
//Basic Idea on how we can use this Library
var generator = new Generator()
var userFilter = generator.Generate<User>("(Age > 10) and (Name like 'Alice%' or Name like 'Bob%')");

//Filter a User collection (users) using the generated lambda expression
var names = users.Where(userFilter).Select(u => u.Name);

```

### Development In Progress...
