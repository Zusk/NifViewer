# Struct `bhkQsTransform`

Bethesda extension of hkQsTransform. The scale vector is not serialized.

## Attributes
- **module**: `BSHavok`
- **name**: `bhkQsTransform`
- **size**: `32`
- **versions**: `#SKY_AND_LATER#`

## Fields
- **Translation** (`Vector4`)
  - A vector that moves the chunk by the specified amount. W is not used.
- **Rotation** (`hkQuaternion`)
  - Rotation. Reference point for rotation is bhkRigidBody translation.

