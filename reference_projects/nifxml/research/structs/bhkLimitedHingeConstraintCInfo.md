# Struct `bhkLimitedHingeConstraintCInfo`

Serialization data for bhkLimitedHingeConstraint.
This constraint allows rotation about a specified axis, limited by specified boundaries.

## Attributes
- **module**: `BSHavok`
- **name**: `bhkLimitedHingeConstraintCInfo`
- **versions**: `#BETHESDA#`

## Fields
- **Pivot A** (`Vector4`)
  - Attributes: `vercond`=`#NI_BS_LTE_16#`
  - Pivot point around which the object will rotate.
- **Axis A** (`Vector4`)
  - Attributes: `vercond`=`#NI_BS_LTE_16#`
  - Axis of rotation.
- **Perp Axis In A1** (`Vector4`)
  - Attributes: `vercond`=`#NI_BS_LTE_16#`
  - Vector in the rotation plane which defines the zero angle.
- **Perp Axis In A2** (`Vector4`)
  - Attributes: `vercond`=`#NI_BS_LTE_16#`
  - Vector in the rotation plane, orthogonal on the previous one, which defines the positive direction of rotation. This is always the vector product of Axis A and Perp Axis In A1.
- **Pivot B** (`Vector4`)
  - Attributes: `vercond`=`#NI_BS_LTE_16#`
  - Pivot A in second entity coordinate system.
- **Axis B** (`Vector4`)
  - Attributes: `vercond`=`#NI_BS_LTE_16#`
  - Axis A in second entity coordinate system.
- **Perp Axis In B2** (`Vector4`)
  - Attributes: `vercond`=`#NI_BS_LTE_16#`
  - Perp Axis In A2 in second entity coordinate system.
- **Axis A** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Axis of rotation.
- **Perp Axis In A1** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Vector in the rotation plane which defines the zero angle.
- **Perp Axis In A2** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Vector in the rotation plane, orthogonal on the previous one, which defines the positive direction of rotation. This is always the vector product of Axis A and Perp Axis In A1.
- **Pivot A** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Pivot point around which the object will rotate.
- **Axis B** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Axis A in second entity coordinate system.
- **Perp Axis In B1** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Perp Axis In A1 in second entity coordinate system.
- **Perp Axis In B2** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Perp Axis In A2 in second entity coordinate system.
- **Pivot B** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Pivot A in second entity coordinate system.
- **Min Angle** (`float`)
  - Minimum rotation angle.
- **Max Angle** (`float`)
  - Maximum rotation angle.
- **Max Friction** (`float`)
  - Maximum friction, typically either 0 or 10. In Fallout 3, typically 100.
- **Motor** (`bhkConstraintMotorCInfo`)
  - Attributes: `since`=`20.2.0.7`, `vercond`=`!#NI_BS_LTE_16#`

