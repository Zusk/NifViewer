# Struct `bhkHingeConstraintCInfo`

Serialization data for bhkHingeConstraint. A basic hinge with no angular limits or motor.

## Attributes
- **module**: `BSHavok`
- **name**: `bhkHingeConstraintCInfo`
- **versions**: `#BETHESDA#`

## Fields
- **Pivot A** (`Vector4`)
  - Attributes: `until`=`20.0.0.5`
  - Pivot point around which the object will rotate.
- **Perp Axis In A1** (`Vector4`)
  - Attributes: `until`=`20.0.0.5`
  - Vector in the rotation plane which defines the zero angle.
- **Perp Axis In A2** (`Vector4`)
  - Attributes: `until`=`20.0.0.5`
  - Vector in the rotation plane, orthogonal on the previous one, which defines the positive direction of rotation.
- **Pivot B** (`Vector4`)
  - Attributes: `until`=`20.0.0.5`
  - Pivot A in second entity coordinate system.
- **Axis B** (`Vector4`)
  - Attributes: `until`=`20.0.0.5`
  - Axis A (vector orthogonal on Perp Axes) in second entity coordinate system.
- **Axis A** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Axis of rotation.
- **Perp Axis In A1** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Vector in the rotation plane which defines the zero angle.
- **Perp Axis In A2** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Vector in the rotation plane, orthogonal on the previous one, which defines the positive direction of rotation. This is always the vector product of Axis A and Perp Axis In A1.
- **Pivot A** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Pivot point around which the object will rotate.
- **Axis B** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Axis A in second entity coordinate system.
- **Perp Axis In B1** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Perp Axis In A1 in second entity coordinate system.
- **Perp Axis In B2** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Perp Axis In A2 in second entity coordinate system.
- **Pivot B** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Pivot A in second entity coordinate system.

