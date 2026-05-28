# Release Notes

## Version 10.0.0

### Breaking Changes

#### Albatross.Text.Table

**`TableOptions` is now type-agnostic.**

`TableOptions` represents a column rendering configuration — headers, formatters, ordering, separators — and has no inherent relationship to a data source type. Accordingly:

- `TableOptions.Type` property has been **removed**
- `TableOptions(Type type)` constructor has been **removed**
- `TableOptionsExtension.Cast<T>()` extension method has been **removed**
- `TableOptionFactory.Register(TableOptions)` has been **replaced** by `Register(Type, TableOptions)`
- `TableOptionFactory.Get<T>()` now returns `TableOptions` instead of `TableOptions<T>`
- `TableOptionFactory.TryGet<T>(out options)` out parameter is now `TableOptions?` instead of `TableOptions<T>?`

**Migration guide**

`Cast<T>()` was a workaround at the end of fluent chains. It is no longer needed — `BuildColumnsByReflection<T>()` and all generic extension methods already return `TableOptions<T>` throughout the chain.

Before:
```csharp
var options = new TableOptions<Product>()
    .BuildColumnsByReflection()
    .Cast<Product>()
    .Ignore(x => x.Id)
    .Format(x => x.Price, "C2");

TableOptionFactory.Instance.Register(options);
```

After:
```csharp
var options = new TableOptions<Product>()
    .BuildColumnsByReflection()
    .Ignore(x => x.Id)
    .Format(x => x.Price, "C2");

TableOptionFactory.Instance.Register(options);
```

---

### New Features

#### Documentation site

Full API documentation and usage guides are now published at https://rushuiguan.github.io/text/ covering all three libraries.

#### Albatross.Text

- `StringExtensions.MarkdownLink(text, url)` — creates a `[text](url)` markdown hyperlink
- `StringExtensions.SlackLink(text, url)` — creates a `<url|text>` Slack hyperlink
- `StringBuilder.EndsWith(char)` — character overload for `EndsWith`

#### Albatross.Text.Table

- `Extensions.MarkdownTable<T>` — renders a typed collection as a Markdown-formatted table
- `StringTable.FilterColumns(params string[])` — returns a new table with only the specified columns
- `StringTable.FilterRows(column, predicate)` — returns a new table with rows filtered by a column value predicate
- `StringTableExtensions.AlignFirst` — aligns multiple tables to the first table's column widths
- `StringTableExtensions.AlignAll` — aligns multiple tables using the maximum width across all tables per column

#### Albatross.Text.CliFormat

- `Extensions.CreateExpression(string?)` is now public API, allowing callers to pre-parse format expressions for reuse across multiple `CliPrintWithExpression` calls
