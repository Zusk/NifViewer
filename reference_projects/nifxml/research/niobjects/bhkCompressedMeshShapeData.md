# Niobject `bhkCompressedMeshShapeData`

A compressed mesh shape for collision in Skyrim.

## Attributes
- **inherit**: `bhkRefObject`
- **module**: `BSHavok`
- **name**: `bhkCompressedMeshShapeData`
- **versions**: `#SKY_AND_LATER#`

## Fields
- **Bits Per Index** (`uint`)
  - Attributes: `default`=`17`
  - Number of bits in the shape-key reserved for a triangle index
- **Bits Per W Index** (`uint`)
  - Attributes: `default`=`18`
  - Number of bits in the shape-key reserved for a triangle index and its winding
- **Mask W Index** (`uint`)
  - Attributes: `default`=`0x3FFFF`
  - Mask used to get the triangle index and winding from a shape-key/
- **Mask Index** (`uint`)
  - Attributes: `default`=`0x1FFFF`
  - Mask used to get the triangle index from a shape-key.
- **Error** (`float`)
  - Attributes: `default`=`0.001`
  - Quantization error.
- **AABB** (`hkAabb`)
- **Welding Type** (`hkWeldingType`)
  - Attributes: `default`=`ANTICLOCKWISE`
- **Material Type** (`bhkCMSMatType`)
  - Attributes: `default`=`SINGLE_VALUE_PER_CHUNK`
- **Num Materials 32** (`uint`)
- **Materials 32** (`uint`)
  - Attributes: `length`=`Num Materials 32`
  - Unused.
- **Num Materials 16** (`uint`)
- **Materials 16** (`uint`)
  - Attributes: `length`=`Num Materials 16`
  - Unused.
- **Num Materials 8** (`uint`)
- **Materials 8** (`uint`)
  - Attributes: `length`=`Num Materials 8`
  - Unused.
- **Num Materials** (`uint`)
  - Attributes: `default`=`1`
- **Chunk Materials** (`bhkMeshMaterial`)
  - Attributes: `length`=`Num Materials`
  - Materials used by Chunks. Chunks refer to this table by index.
- **Num Named Materials** (`uint`)
  - Number of hkpNamedMeshMaterial. Unused.
- **Num Transforms** (`uint`)
  - Attributes: `default`=`1`
  - Number of chunk transformations
- **Chunk Transforms** (`bhkQsTransform`)
  - Attributes: `length`=`Num Transforms`
  - Transforms used by Chunks. Chunks refer to this table by index.
- **Num Big Verts** (`uint`)
- **Big Verts** (`Vector4`)
  - Attributes: `length`=`Num Big Verts`
  - Vertices paired with Big Tris (triangles that are too large for chunks)
- **Num Big Tris** (`uint`)
- **Big Tris** (`bhkCMSBigTri`)
  - Attributes: `length`=`Num Big Tris`
  - Triangles that are too large to fit in a chunk.
- **Num Chunks** (`uint`)
  - Attributes: `default`=`1`
- **Chunks** (`bhkCMSChunk`)
  - Attributes: `length`=`Num Chunks`
- **Num Convex Piece A** (`uint`)
  - Number of hkpCompressedMeshShape::ConvexPiece. Unused.

