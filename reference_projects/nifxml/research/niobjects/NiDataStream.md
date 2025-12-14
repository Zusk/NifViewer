# Niobject `NiDataStream`

## Attributes
- **inherit**: `NiObject`
- **module**: `NiMesh`
- **name**: `NiDataStream`
- **since**: `V20_5_0_0`

## Fields
- **Usage** (`DataStreamUsage`)
  - Attributes: `abstract`=`true`
- **Access** (`DataStreamAccess`)
  - Attributes: `abstract`=`true`
- **Num Bytes** (`uint`)
  - The size in bytes of this data stream.
- **Cloning Behavior** (`CloningBehavior`)
  - Attributes: `default`=`CLONING_SHARE`
- **Num Regions** (`uint`)
  - Number of regions (such as submeshes).
- **Regions** (`Region`)
  - Attributes: `length`=`Num Regions`
  - The regions in the mesh. Regions can be used to mark off submeshes which are independent draw calls.
- **Num Components** (`uint`)
  - Number of components of the data (matches corresponding field in MeshData).
- **Component Formats** (`ComponentFormat`)
  - Attributes: `length`=`Num Components`
  - The format of each component in this data stream.
- **Data** (`DataStreamData`)
  - Attributes: `arg1`=`Num Bytes`, `arg2`=`Component Formats`
- **Streamable** (`bool`)
  - Attributes: `default`=`true`

