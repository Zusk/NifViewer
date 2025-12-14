# Struct `Header`

The NIF file header.

## Attributes
- **module**: `NiMain`
- **name**: `Header`

## Fields
- **Header String** (`HeaderString`)
  - 'NetImmerse File Format x.x.x.x' (versions <= 10.0.1.2) or 'Gamebryo File Format x.x.x.x' (versions >= 10.1.0.0), with x.x.x.x the version written out. Ends with a newline character (0x0A).
- **Copyright** (`LineString`)
  - Attributes: `length`=`3`, `until`=`3.1.0.0`
- **Version** (`FileVersion`)
  - Attributes: `default`=`0x04000002`, `since`=`3.1.0.1`
  - The NIF version, in hexadecimal notation: 0x04000002, 0x0401000C, 0x04020002, 0x04020100, 0x04020200, 0x0A000100, 0x0A010000, 0x0A020000, 0x14000004, ...
- **Endian Type** (`EndianType`)
  - Attributes: `default`=`ENDIAN_LITTLE`, `since`=`20.0.0.3`
  - Determines the endianness of the data in the file.
- **User Version** (`ulittle32`)
  - Attributes: `since`=`10.0.1.8`
  - An extra version number, for companies that decide to modify the file format.
- **Num Blocks** (`ulittle32`)
  - Attributes: `since`=`3.1.0.1`
  - Number of file objects.
- **BS Header** (`BSStreamHeader`)
  - Attributes: `cond`=`#BSSTREAMHEADER#`
- **Metadata** (`ByteArray`)
  - Attributes: `since`=`30.0.0.0`
- **Num Block Types** (`ushort`)
  - Attributes: `since`=`5.0.0.1`
  - Number of object types in this NIF file.
- **Block Types** (`SizedString`)
  - Attributes: `cond`=`Version != 20.3.1.2`, `length`=`Num Block Types`, `since`=`5.0.0.1`
  - List of all object types used in this NIF file.
- **Block Type Hashes** (`uint`)
  - Attributes: `length`=`Num Block Types`, `since`=`20.3.1.2`, `until`=`20.3.1.2`
  - List of all object types used in this NIF file.
- **Block Type Index** (`BlockTypeIndex`)
  - Attributes: `length`=`Num Blocks`, `since`=`5.0.0.1`
  - Maps file objects on their corresponding type: first file object is of type object_types[object_type_index[0]], the second of object_types[object_type_index[1]], etc.
- **Block Size** (`uint`)
  - Attributes: `length`=`Num Blocks`, `since`=`20.2.0.5`
  - Array of block sizes
- **Num Strings** (`uint`)
  - Attributes: `since`=`20.1.0.1`
  - Number of strings.
- **Max String Length** (`uint`)
  - Attributes: `since`=`20.1.0.1`
  - Maximum string length.
- **Strings** (`SizedString`)
  - Attributes: `length`=`Num Strings`, `since`=`20.1.0.1`
  - Strings.
- **Num Groups** (`uint`)
  - Attributes: `default`=`0`, `since`=`5.0.0.6`
- **Groups** (`uint`)
  - Attributes: `length`=`Num Groups`, `since`=`5.0.0.6`

