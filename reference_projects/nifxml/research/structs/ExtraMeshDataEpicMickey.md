# Struct `ExtraMeshDataEpicMickey`

## Attributes
- **module**: `NiMesh`
- **name**: `ExtraMeshDataEpicMickey`
- **versions**: `V20_6_5_0_DEM`

## Fields
- **Has Weights** (`uint`)
  - 1 if it has weights, 0 otherwise.
- **Num Transform Floats** (`uint`)
  - Total number of floats in the bone transform matrices - divide by 16 to get the number of matrices.
- **Bone Transforms** (`Matrix44`)
  - Attributes: `length`=`Num Transform Floats #DIV# 16`
  - Transform matrices corresponding to the bones. Note: Stored transposed to normally.
- **Num Weights** (`uint`)
- **Weights** (`WeightDataEpicMickey`)
  - Attributes: `length`=`Num Weights`
- **Vertex to Weight Map Size** (`uint`)
- **Vertex to Weight Map** (`uint`)
  - Attributes: `length`=`Vertex to Weight Map Size`
- **Unknown Data Size** (`uint`)
- **Unknown Data Width** (`uint`)
- **Unknown Data** (`Vector3`)
  - Attributes: `length`=`Unknown Data Size`, `width`=`Unknown Data Width`
- **Unknown Indices** (`ushort`)
  - Attributes: `length`=`Unknown Data Size`
- **Num Mapped Primitives** (`ushort`)
  - Attributes: `vercond`=`#USER# #LT# 17`
  - When non-zero, equal to the number of primitives.
- **Num Mapped Primitives** (`uint`)
  - Attributes: `vercond`=`#USER# == 17`
  - When non-zero, equal to the number of primitives.
- **Mapped Primitives** (`byte`)
  - Attributes: `length`=`Num Mapped Primitives`
  - Some integer associated with each primitive.
- **Partition Size** (`uint`)
- **Partitions** (`PartitionDataEpicMickey`)
  - Attributes: `length`=`Partition Size`
- **Max Primitive Map Index** (`uint`)
  - The max value to appear in the Mapped Primitives array.

