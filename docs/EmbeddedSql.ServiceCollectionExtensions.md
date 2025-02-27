### [EmbeddedSql](EmbeddedSql.md 'EmbeddedSql')

## ServiceCollectionExtensions Class

Extension methods for configuring services at application startup.

```csharp
public static class ServiceCollectionExtensions
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; ServiceCollectionExtensions
### Methods

<a name='EmbeddedSql.ServiceCollectionExtensions.AddEmbeddedSql(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,System.Action_EmbeddedSql.EmbeddedSqlOptions_)'></a>

## ServiceCollectionExtensions.AddEmbeddedSql(this IServiceCollection, Action<EmbeddedSqlOptions>) Method

Adds the [EmbeddedSql](EmbeddedSql.md 'EmbeddedSql') services to the [Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') 
using custom [EmbeddedSqlOptions](EmbeddedSql.EmbeddedSqlOptions.md 'EmbeddedSql.EmbeddedSqlOptions'):
- [ISql](EmbeddedSql.ISql.md 'EmbeddedSql.ISql') with a [Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton 'Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton')
- [IMigrator](EmbeddedSql.IMigrator.md 'EmbeddedSql.IMigrator') with a [Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped 'Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped')

```csharp
public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddEmbeddedSql(this Microsoft.Extensions.DependencyInjection.IServiceCollection services, System.Action<EmbeddedSql.EmbeddedSqlOptions> configure);
```
#### Parameters

<a name='EmbeddedSql.ServiceCollectionExtensions.AddEmbeddedSql(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,System.Action_EmbeddedSql.EmbeddedSqlOptions_).services'></a>

`services` [Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection')

<a name='EmbeddedSql.ServiceCollectionExtensions.AddEmbeddedSql(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection,System.Action_EmbeddedSql.EmbeddedSqlOptions_).configure'></a>

`configure` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')[EmbeddedSqlOptions](EmbeddedSql.EmbeddedSqlOptions.md 'EmbeddedSql.EmbeddedSqlOptions')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-1 'System.Action`1')

#### Returns
[Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')

<a name='EmbeddedSql.ServiceCollectionExtensions.AddEmbeddedSql(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection)'></a>

## ServiceCollectionExtensions.AddEmbeddedSql(this IServiceCollection) Method

Adds the [EmbeddedSql](EmbeddedSql.md 'EmbeddedSql') services to the [Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection') 
using the default [EmbeddedSqlOptions](EmbeddedSql.EmbeddedSqlOptions.md 'EmbeddedSql.EmbeddedSqlOptions'):
- [ISql](EmbeddedSql.ISql.md 'EmbeddedSql.ISql') with a [Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton 'Microsoft.Extensions.DependencyInjection.ServiceLifetime.Singleton')
- [IMigrator](EmbeddedSql.IMigrator.md 'EmbeddedSql.IMigrator') with a [Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped 'Microsoft.Extensions.DependencyInjection.ServiceLifetime.Scoped')

```csharp
public static Microsoft.Extensions.DependencyInjection.IServiceCollection AddEmbeddedSql(this Microsoft.Extensions.DependencyInjection.IServiceCollection services);
```
#### Parameters

<a name='EmbeddedSql.ServiceCollectionExtensions.AddEmbeddedSql(thisMicrosoft.Extensions.DependencyInjection.IServiceCollection).services'></a>

`services` [Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection')

#### Returns
[Microsoft.Extensions.DependencyInjection.IServiceCollection](https://docs.microsoft.com/en-us/dotnet/api/Microsoft.Extensions.DependencyInjection.IServiceCollection 'Microsoft.Extensions.DependencyInjection.IServiceCollection')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

[System.InvalidOperationException](https://docs.microsoft.com/en-us/dotnet/api/System.InvalidOperationException 'System.InvalidOperationException')