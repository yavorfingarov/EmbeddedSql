### [EmbeddedSql](EmbeddedSql.md 'EmbeddedSql')

## EmbeddedSqlOptions Class

Options for [ISql](EmbeddedSql.ISql.md 'EmbeddedSql.ISql') and [IMigrator](EmbeddedSql.IMigrator.md 'EmbeddedSql.IMigrator').

```csharp
public sealed class EmbeddedSqlOptions
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; EmbeddedSqlOptions
### Properties

<a name='EmbeddedSql.EmbeddedSqlOptions.Assemblies'></a>

## EmbeddedSqlOptions.Assemblies Property

Sets the assemblies to scan for embedded `.sql` files.

```csharp
public System.Collections.Generic.IEnumerable<System.Reflection.Assembly> Assemblies { get; set; }
```

#### Property Value
[System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Reflection.Assembly](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.Assembly 'System.Reflection.Assembly')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

### Remarks
Default: the calling assembly for 
[AddEmbeddedSql(this IServiceCollection, Action&lt;EmbeddedSqlOptions&gt;)](EmbeddedSql.ServiceCollectionExtensions.md#EmbeddedSql.ServiceCollectionExtensions.AddEmbeddedSql(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,System.Action_EmbeddedSql.EmbeddedSqlOptions_) 'EmbeddedSql.ServiceCollectionExtensions.AddEmbeddedSql(this Microsoft.Extensions.DependencyInjection.IServiceCollection, System.Action<EmbeddedSql.EmbeddedSqlOptions>)')

<a name='EmbeddedSql.EmbeddedSqlOptions.ServiceKey'></a>

## EmbeddedSqlOptions.ServiceKey Property

Sets the [ISql](EmbeddedSql.ISql.md 'EmbeddedSql.ISql'), [IMigrator](EmbeddedSql.IMigrator.md 'EmbeddedSql.IMigrator') and [MigratorOptions](EmbeddedSql.MigratorOptions.md 'EmbeddedSql.MigratorOptions') service key.

```csharp
public object? ServiceKey { get; set; }
```

#### Property Value
[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')

### Remarks
Default: [null](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/null')
### Methods

<a name='EmbeddedSql.EmbeddedSqlOptions.ConfigureMigrator(System.Action_EmbeddedSql.MigratorOptions_)'></a>

## EmbeddedSqlOptions.ConfigureMigrator(Action<MigratorOptions>) Method

Configures the [MigratorOptions](EmbeddedSql.MigratorOptions.md 'EmbeddedSql.MigratorOptions') for [IMigrator](EmbeddedSql.IMigrator.md 'EmbeddedSql.IMigrator').

```csharp
public void ConfigureMigrator(System.Action<EmbeddedSql.MigratorOptions> configure);
```
#### Parameters

<a name='EmbeddedSql.EmbeddedSqlOptions.ConfigureMigrator(System.Action_EmbeddedSql.MigratorOptions_).configure'></a>

`configure` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[MigratorOptions](EmbeddedSql.MigratorOptions.md 'EmbeddedSql.MigratorOptions')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

<a name='EmbeddedSql.EmbeddedSqlOptions.UseFilter(System.Func_string,bool_)'></a>

## EmbeddedSqlOptions.UseFilter(Func<string,bool>) Method

Sets a resource name filter.

```csharp
public void UseFilter(System.Func<string,bool> filter);
```
#### Parameters

<a name='EmbeddedSql.EmbeddedSqlOptions.UseFilter(System.Func_string,bool_).filter'></a>

`filter` [System.Func&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Func-2 'System.Func`2')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')