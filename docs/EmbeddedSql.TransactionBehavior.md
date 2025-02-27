### [EmbeddedSql](EmbeddedSql.md 'EmbeddedSql')

## TransactionBehavior Enum

Specifies a transaction behavior when applying migrations.

```csharp
public enum TransactionBehavior
```
### Fields

<a name='EmbeddedSql.TransactionBehavior.None'></a>

`None` 0

Migrations are applied without transaction.

<a name='EmbeddedSql.TransactionBehavior.Overarching'></a>

`Overarching` 2

All migrations are wrapped in a single transaction.

<a name='EmbeddedSql.TransactionBehavior.PerScript'></a>

`PerScript` 1

Every migration is wrapped in a transaction.