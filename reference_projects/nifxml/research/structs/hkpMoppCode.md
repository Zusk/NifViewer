# Struct `hkpMoppCode`

## Attributes
- **module**: `BSHavok`
- **name**: `hkpMoppCode`
- **versions**: `#BETHESDA#`

## Fields
- **Data Size** (`uint`)
  - Number of bytes for MOPP data.
- **Offset** (`Vector4`)
  - Attributes: `since`=`10.1.0.0`
  - XYZ: Origin of the object in mopp coordinates. This is the minimum of all vertices in the packed shape along each axis, minus the radius of the child bhkPackedNiTriStripsShape/
     bhkCompressedMeshShape.
W: The scaling factor to quantize the MOPP: the quantization factor is equal to 256*256 divided by this number.
   In Oblivion and Skyrim files, scale is taken equal to 256*256*254 / (size + 2 * radius) where size is the largest dimension of the bounding box of the packed shape,
   and radius is the radius of the child bhkPackedNiTriStripsShape/bhkCompressedMeshShape.
- **Build Type** (`hkMoppCodeBuildType`)
  - Attributes: `vercond`=`#BS_GT_FO3#`
  - Tells if MOPP Data was organized into smaller chunks (PS3) or not (PC)
- **Data** (`byte`)
  - Attributes: `binary`=`true`, `length`=`Data Size`
  - The tree of bounding volume data.

