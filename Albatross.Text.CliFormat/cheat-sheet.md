# CLI Format Operations Cheat Sheet

The CLI Format system provides expression-based formatting for data objects using prefix notation. All operations use the built-in variable `value` to reference the input data.

## Basic Syntax

```
operation_name(arguments)
```

- Use `value` to reference the input data
- Operations can be chained: `table(first(value, 3), Name, Age)`
- String literals use single or double quotes: `'text'` or `"text"`
- JSON pointers use forward slashes: `/property/0/field`

## Core Data Operations

### **table** - Tabular Output
Formats collections as tables with optional column filtering.

```bash
table(value)                    # All columns
table(value, Name, Age)         # Specific columns only
table(subset(value, 5), Name)    # Chained with other operations
```

**Input:** Collection  
**Output:** Formatted table with headers and aligned columns

### **csv** - CSV with Headers
Exports data as comma-separated values with column headers.

```bash
csv(value)                      # All columns with headers
csv(value, FirstName, LastName) # Specific columns with headers
```

**Input:** Collection  
**Output:** CSV string with headers

### **ccsv** - Compact CSV (No Headers)
Exports data as CSV without column headers.

```bash
ccsv(value)                     # All columns, no headers
ccsv(value, Name, Email)        # Specific columns, no headers
```

**Input:** Collection  
**Output:** CSV string without headers

### **json** - JSON Formatting
Serializes data to formatted JSON.

```bash
json(value)                     # Full JSON serialization
json(first(value))              # JSON of first element
```

**Input:** Any object or collection  
**Output:** Indented JSON string

### **list** - Simple List Format
Displays each item on a separate line, optionally filtering by property.

```bash
list(value)                     # All items as key-value pairs
list(value, Name)               # Extract specific property from each item
```

**Input:** Collection  
**Output:** Multi-line list format

## Collection Operations

### **first** - Get First Element
Extracts the first element from a collection.

```bash
first(value)                    # Get first item
first(property(value, 'Emails')) # Get first email
```

**Input:** Collection  
**Output:** Single element

### **last** - Get Last Element
Extracts the last element from a collection.

```bash
last(value)                     # Get last item
```

**Input:** Collection  
**Output:** Single element

### **subset** - Extract Range
Returns a subset of collection elements starting at specified index.

```bash
subset(value, 2)                # From index 2 to end
subset(value, 1, 3)             # From index 1, take 3 items
subset(value, 0, 5)             # First 5 items (alternative to first)
```

**Input:** Collection  
**Output:** Array subset  
**Parameters:** start_index, [count]

## Property Access Operations

### **property** - Access Object Properties
Extracts property values from objects, supports nested access.

```bash
property(value, 'Name')         # Simple property
property(value, 'Address.Street') # Nested property
property(value, 'Emails[0]')    # Array indexing
```

**Input:** Object  
**Output:** Property value  
**Parameters:** property_path (string)

### **cproperty** - Collection Property Extract
Extracts the same property from all elements in a collection.

```bash
cproperty(value, 'Name')        # Get Name from all items
cproperty(value, 'Age')         # Get Age from all items
```

**Input:** Collection  
**Output:** Array of property values  
**Parameters:** property_name

## JSON Pointer Operations

### **jsonpointer** - Extract JSON Data
Uses JSON Pointer syntax to extract specific data from objects.

```bash
jsonpointer(value, '/0/name')   # Get name from first array element
jsonpointer(value, '/address/street') # Get nested address street
```

**Input:** Any object  
**Output:** Extracted JSON value  
**Parameters:** JSON pointer path (e.g., `/property/0/field`)

### **cjsonpointer** - Collection JSON Pointer
Applies JSON pointer extraction to each element in a collection.

```bash
cjsonpointer(value, '/name')    # Extract name from each item
cjsonpointer(value, '/address/city') # Extract city of the address of a collection of item.
```

**Input:** Collection  
**Output:** JSON array of extracted values  
**Parameters:** JSON pointer path

## Formatting Operations

### **cformat** - Apply .NET Format Strings
Applies .NET format strings to collection elements.

```bash
cformat(value, 'C')              # Currency format for numbers
cformat(value, 'yyyy-MM-dd')     # Date format
cformat(value, 'F2')             # Two decimal places
```

**Input:** Collection
**Output:** Array of formatted strings  
**Parameters:** .NET format string

## Operation Chaining Examples

Chain operations by nesting them - inner operations execute first:

```bash
# Get table of first 3 contacts with specific columns
table(first(value, 3), FirstName, LastName, Email)

# Get JSON of the last contact's address
json(property(last(value), 'Address'))

# Get CSV of names from first 5 items
csv(cproperty(first(value, 5), 'Name'))

# Extract email from first contact
first(property(value, 'Email'))

# Get formatted dates from a subset
cformat(subset(cproperty(value, 'CreatedDate'), 0, 10), 'yyyy-MM-dd')

# JSON pointer on first element
jsonpointer(first(value), '/address/street')

# Collection property then subset
subset(cproperty(value, 'Tags'), 2, 5)
```

## Auto-Detection Behavior

When no format is specified, the system automatically chooses:
- **Collections:** Display as table with all columns
- **Single Objects:** Display as property table
- **Simple Values:** Display as string

## Common Patterns

### Data Exploration
```bash
json(first(value))              # See structure of first item
table(first(value, 5))          # Preview first 5 rows
list(value, Name)               # Quick name listing
```

### Filtered Views  
```bash
table(value, Name, Email, Phone) # Contact summary
csv(value, FirstName, LastName)  # Export names only
subset(value, 10, 20)           # Pagination
```

### Property Analysis
```bash
cproperty(value, Status)        # All status values
cformat(cproperty(value, Price), 'C') # Formatted prices
jsonpointer(value, '/stats/count') # Nested statistics
```

### Data Extraction
```bash
cjsonpointer(value, '/id')      # Extract all IDs
property(last(value), 'UpdatedAt') # Last update time
first(cproperty(value, 'Email')) # First email address
```

## Tips

- Use `first(value)` or `json(first(value))` to explore object structure
- Chain `subset` with `table` for pagination: `table(subset(value, 0, 10), Name)`
- Use `cproperty` to extract arrays of specific fields
- JSON pointers are great for complex nested data extraction
- Format operations work well with numeric and date collections
- Property paths support dot notation and array indexing: `'items[0].name'`