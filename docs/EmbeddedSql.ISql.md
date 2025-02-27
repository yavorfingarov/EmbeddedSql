### [EmbeddedSql](EmbeddedSql.md 'EmbeddedSql')

## ISql Interface

Specifies the contract for getting embedded SQL statements.

```csharp
public interface ISql
```
### Properties

<a name='EmbeddedSql.ISql.this[string]'></a>

## ISql.this[string] Property

Gets the SQL statement associated with the specified key.

```csharp
string this[string key] { get; }
```
#### Parameters

<a name='EmbeddedSql.ISql.this[string].key'></a>

`key` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Property Value
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')

[System.Collections.Generic.KeyNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyNotFoundException 'System.Collections.Generic.KeyNotFoundException')
### Methods

<a name='EmbeddedSql.ISql.AsEnumerable()'></a>

## ISql.AsEnumerable() Method

Gets all SQL statements.

```csharp
System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<string,string>> AsEnumerable();
```

#### Returns
[System.Collections.Generic.IEnumerable&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')[System.Collections.Generic.KeyValuePair&lt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2 'System.Collections.Generic.KeyValuePair`2')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[,](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2 'System.Collections.Generic.KeyValuePair`2')[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyValuePair-2 'System.Collections.Generic.KeyValuePair`2')[&gt;](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1 'System.Collections.Generic.IEnumerable`1')

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string,string,string)'></a>

## ISql.UnsafeFormat(string, string, string, string) Method

Replaces the format item in the SQL statement associated with the specified key.

```csharp
string UnsafeFormat(string key, string arg0, string arg1, string arg2);
```
#### Parameters

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string,string,string).key'></a>

`key` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string,string,string).arg0'></a>

`arg0` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string,string,string).arg1'></a>

`arg1` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string,string,string).arg2'></a>

`arg2` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')

[System.Collections.Generic.KeyNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyNotFoundException 'System.Collections.Generic.KeyNotFoundException')

[System.FormatException](https://docs.microsoft.com/en-us/dotnet/api/System.FormatException 'System.FormatException')

### Remarks
<b>Never</b> pass non-validated user-provided values into this method. 
            Doing so may expose your application to SQL injection attacks.

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string,string)'></a>

## ISql.UnsafeFormat(string, string, string) Method

Replaces the format item in the SQL statement associated with the specified key.

```csharp
string UnsafeFormat(string key, string arg0, string arg1);
```
#### Parameters

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string,string).key'></a>

`key` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string,string).arg0'></a>

`arg0` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string,string).arg1'></a>

`arg1` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')

[System.Collections.Generic.KeyNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyNotFoundException 'System.Collections.Generic.KeyNotFoundException')

[System.FormatException](https://docs.microsoft.com/en-us/dotnet/api/System.FormatException 'System.FormatException')

### Remarks
<b>Never</b> pass non-validated user-provided values into this method. 
            Doing so may expose your application to SQL injection attacks.

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string)'></a>

## ISql.UnsafeFormat(string, string) Method

Replaces the format item in the SQL statement associated with the specified key.

```csharp
string UnsafeFormat(string key, string arg0);
```
#### Parameters

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string).key'></a>

`key` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string).arg0'></a>

`arg0` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')

[System.Collections.Generic.KeyNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyNotFoundException 'System.Collections.Generic.KeyNotFoundException')

[System.FormatException](https://docs.microsoft.com/en-us/dotnet/api/System.FormatException 'System.FormatException')

### Remarks
<b>Never</b> pass non-validated user-provided values into this method. 
            Doing so may expose your application to SQL injection attacks.

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string[])'></a>

## ISql.UnsafeFormat(string, string[]) Method

Replaces the format item in the SQL statement associated with the specified key.

```csharp
string UnsafeFormat(string key, params string[] args);
```
#### Parameters

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string[]).key'></a>

`key` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

<a name='EmbeddedSql.ISql.UnsafeFormat(string,string[]).args'></a>

`args` [System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')[[]](https://docs.microsoft.com/en-us/dotnet/api/System.Array 'System.Array')

#### Returns
[System.String](https://docs.microsoft.com/en-us/dotnet/api/System.String 'System.String')

#### Exceptions

[System.ArgumentNullException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentNullException 'System.ArgumentNullException')

[System.ArgumentException](https://docs.microsoft.com/en-us/dotnet/api/System.ArgumentException 'System.ArgumentException')

[System.Collections.Generic.KeyNotFoundException](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyNotFoundException 'System.Collections.Generic.KeyNotFoundException')

[System.FormatException](https://docs.microsoft.com/en-us/dotnet/api/System.FormatException 'System.FormatException')

### Remarks
<b>Never</b> pass non-validated user-provided values into this method. 
            Doing so may expose your application to SQL injection attacks.