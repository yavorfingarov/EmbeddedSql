### [EmbeddedSql](EmbeddedSql.md 'EmbeddedSql')

## IMigrator Interface

Specifies the contract for applying database migrations.

```csharp
public interface IMigrator
```
### Methods

<a name='EmbeddedSql.IMigrator.Run()'></a>

## IMigrator.Run() Method

Applies database migrations.

```csharp
void Run();
```

<a name='EmbeddedSql.IMigrator.RunAsync(System.Threading.CancellationToken)'></a>

## IMigrator.RunAsync(CancellationToken) Method

Applies database migrations.

```csharp
System.Threading.Tasks.Task RunAsync(System.Threading.CancellationToken cancellationToken=default(System.Threading.CancellationToken));
```
#### Parameters

<a name='EmbeddedSql.IMigrator.RunAsync(System.Threading.CancellationToken).cancellationToken'></a>

`cancellationToken` [System.Threading.CancellationToken](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.CancellationToken 'System.Threading.CancellationToken')

#### Returns
[System.Threading.Tasks.Task](https://docs.microsoft.com/en-us/dotnet/api/System.Threading.Tasks.Task 'System.Threading.Tasks.Task')