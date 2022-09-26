# DynamicFilter.Sql

![GitHub](https://img.shields.io/github/license/prakashrx/DynamicFilter.Sql) ![GitHub Workflow Status](https://img.shields.io/github/workflow/status/prakashrx/DynamicFilter.Sql/.NET%20Build%20and%20Test) ![Nuget](https://img.shields.io/nuget/v/DynamicFilter.Sql)

A Fast and efficient dotnet library to dynamically generate compiled lambda expressions from sql style filters.

## Installation

DynamicFilter.Sql package can be install via [Nuget](https://www.nuget.org/packages/DynamicFilter.Sql/)

```bash
dotnet add package DynamicFilter.Sql
```

## Usage/Examples

The `Compile` function takes a generic parameter that is used to determine the entity fields. This could be a user defined class or it could be a `Dictionary<string, object>` type.

#### User defined class

With a user defined class, the field names within the filter expression have to match the property names defined within the class. This is resolved at compile time.

```csharp
var myfilter = FilterExpression.Compile<User>("(Age > 10) and (Name like 'Alice%' or Name like 'Bob%')");

//Evaluate a single object
bool match = myFilter(new User {Age = 15, Name="Alice Wonderland"})

//Filter a collection
var names = users.Where(myfilter).Select(u => u.Name);

```

#### Dictionary

```csharp
var dict = new Dictionary<string, object> {
                {"Name" , "Alice"},
                {"Age" , 12},
            };

var filter = FilterExpression.Compile<Dictionary<string, object>>("(Age > 10) and (Name like 'Alice%' or Name like 'Bob%')");

var match = filter(dict); //returns true

```

## Features

- Compile to .NET IL code.
- Support Sql Comparison operations
  - =, <, >, <=, >=
  - like, not like
  - in, not in
  - is null, is not null
- Dictionary and User objects

## Use cases

- Dynamically generate code to filter objects in memory
- Filter a stream of data with a multiple criteria
- User Preferences/Filters that could be potentially saved/loaded from a File/Database.

## Authors

- [@prakashrx](https://github.com/prakashrx)
