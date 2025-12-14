# Basic `HeaderString`

A variable length string that ends with a newline character (0x0A).  The string starts as follows depending on the version:

Version <= 10.0.1.0:  'NetImmerse File Format'
Version >= 10.1.0.0:  'Gamebryo File Format'

## Attributes
- **integral**: `false`
- **name**: `HeaderString`

