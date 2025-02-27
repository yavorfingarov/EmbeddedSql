### [EmbeddedSql](EmbeddedSql.md 'EmbeddedSql')

## MigratorOptions Class

Options for [IMigrator](EmbeddedSql.IMigrator.md 'EmbeddedSql.IMigrator').

```csharp
public sealed class MigratorOptions
```

Inheritance [System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object') &#129106; MigratorOptions
### Properties

<a name='EmbeddedSql.MigratorOptions.CreateMigrationCommand'></a>

## MigratorOptions.CreateMigrationCommand Property

Sets the key of a command for creating an entry for an applied migration.

```csharp
public string CreateMigrationCommand { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')

### Remarks
Default: `_Migration.Create`

<a name='EmbeddedSql.MigratorOptions.EnsureMigrationTableCommand'></a>

## MigratorOptions.EnsureMigrationTableCommand Property

Sets the key of an idempotent command for creating the migration table.

```csharp
public string EnsureMigrationTableCommand { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')

### Remarks
Default: `_Migration.EnsureTable`

<a name='EmbeddedSql.MigratorOptions.GetMigrationsQuery'></a>

## MigratorOptions.GetMigrationsQuery Property

Sets the key of a query for getting the already applied migrations.

```csharp
public string GetMigrationsQuery { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')

### Remarks
Default: `_Migration.GetAll`

<a name='EmbeddedSql.MigratorOptions.Idempotent'></a>

## MigratorOptions.Idempotent Property

Sets the boolean flag that determines whether the scripts are idempotent.

```csharp
public bool Idempotent { get; set; }
```

#### Property Value
[System.Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean 'System.Boolean')

### Remarks
Default: [false](https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool 'https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/bool')

<a name='EmbeddedSql.MigratorOptions.MigrationScriptPrefix'></a>

## MigratorOptions.MigrationScriptPrefix Property

Sets the key prefix used for marking a migration.

```csharp
public string MigrationScriptPrefix { get; set; }
```

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')

### Remarks
Default: `Migration.`

<a name='EmbeddedSql.MigratorOptions.TransactionBehavior'></a>

## MigratorOptions.TransactionBehavior Property

Sets the transaction behavior when applying migrations.

```csharp
public EmbeddedSql.TransactionBehavior TransactionBehavior { get; set; }
```

#### Property Value
[TransactionBehavior](EmbeddedSql.TransactionBehavior.md 'EmbeddedSql.TransactionBehavior')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

[System.ArgumentOutOfRangeException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentOutOfRangeException 'System.ArgumentOutOfRangeException')

### Remarks
Default: [PerScript](EmbeddedSql.TransactionBehavior.md#EmbeddedSql.TransactionBehavior.PerScript 'EmbeddedSql.TransactionBehavior.PerScript')
### Methods

<a name='EmbeddedSql.MigratorOptions.ConfigureCreateMigrationCommandParameters(System.Action_System.Collections.Generic.Dictionary_string,object_,string_)'></a>

## MigratorOptions.ConfigureCreateMigrationCommandParameters(Action<Dictionary<string,object>,string>) Method

Configures the parameters for [CreateMigrationCommand](EmbeddedSql.MigratorOptions.md#EmbeddedSql.MigratorOptions.CreateMigrationCommand 'EmbeddedSql.MigratorOptions.CreateMigrationCommand').

```csharp
public void ConfigureCreateMigrationCommandParameters(System.Action<System.Collections.Generic.Dictionary<string,object?>,string> configure);
```
#### Parameters

<a name='EmbeddedSql.MigratorOptions.ConfigureCreateMigrationCommandParameters(System.Action_System.Collections.Generic.Dictionary_string,object_,string_).configure'></a>

`configure` [System.Action&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-2 'System.Action`2')[System.Collections.Generic.Dictionary&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System.Collections.Generic.Dictionary`2')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System.Collections.Generic.Dictionary`2')[System.Object](https://docs.microsoft.com/en-us/dotnet/api/System.Object 'System.Object')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2 'System.Collections.Generic.Dictionary`2')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Action-2 'System.Action`2')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Action-2 'System.Action`2')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

### Remarks
Default: `(parameters, migration) => parameters.Add("@Id", migration)`