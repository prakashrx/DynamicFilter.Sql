# DynamicFilter.Sql

![GitHub](https://img.shields.io/github/license/prakashrx/DynamicFilter.Sql) ![GitHub Workflow Status](https://img.shields.io/github/workflow/status/prakashrx/DynamicFilter.Sql/.NET%20Build%20and%20Test) ![Nuget](https://img.shields.io/nuget/v/DynamicFilter.Sql)

A Fast and efficient dotnet library to dynamically generate compiled lambda expressions from sql style filters.

## Installation

DynamicFilter.Sql package can be install via [Nuget](https://www.nuget.org/packages/DynamicFilter.Sql/)

```bash
dotnet add package DynamicFilter.Sql
```

## Usage/Examples

```csharp
var myfilter = FilterExpression.Compile<User>("(Age > 10) and (Name like 'Alice%' or Name like 'Bob%')");

//Evaluate a single object
bool match = myFilter(new User {Age = 15, Name="Alice Wonderland"})

//Filter a collection
var names = users.Where(myfilter).Select(u => u.Name);

```

## Authors

- [@prakashrx](https://github.com/prakashrx)
