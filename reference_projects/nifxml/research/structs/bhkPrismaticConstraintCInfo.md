# Struct `bhkPrismaticConstraintCInfo`

Serialization data for bhkPrismaticConstraint.
Creates a rail between two bodies that allows translation along a single axis with linear limits and a motor.
All three rotation axes and the remaining two translation axes are fixed.

## Attributes
- **module**: `BSHavok`
- **name**: `bhkPrismaticConstraintCInfo`
- **versions**: `#BETHESDA#`

## Fields
- **Pivot A** (`Vector4`)
  - Attributes: `until`=`20.0.0.5`
  - Pivot.
- **Rotation A** (`Vector4`)
  - Attributes: `until`=`20.0.0.5`
  - Rotation axis.
- **Plane A** (`Vector4`)
  - Attributes: `until`=`20.0.0.5`
  - Plane normal. Describes the plane the object is able to move on.
- **Sliding A** (`Vector4`)
  - Attributes: `until`=`20.0.0.5`
  - Describes the axis the object is able to travel along. Unit vector.
- **Sliding B** (`Vector4`)
  - Attributes: `until`=`20.0.0.5`
  - Describes the axis the object is able to travel along in B coordinates. Unit vector.
- **Pivot B** (`Vector4`)
  - Attributes: `until`=`20.0.0.5`
  - Pivot in B coordinates.
- **Rotation B** (`Vector4`)
  - Attributes: `until`=`20.0.0.5`
  - Rotation axis.
- **Plane B** (`Vector4`)
  - Attributes: `until`=`20.0.0.5`
  - Plane normal. Describes the plane the object is able to move on in B coordinates.
- **Sliding A** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Describes the axis the object is able to travel along. Unit vector.
- **Rotation A** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Rotation axis.
- **Plane A** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Plane normal. Describes the plane the object is able to move on.
- **Pivot A** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Pivot.
- **Sliding B** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Describes the axis the object is able to travel along in B coordinates. Unit vector.
- **Rotation B** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Rotation axis.
- **Plane B** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Plane normal. Describes the plane the object is able to move on in B coordinates.
- **Pivot B** (`Vector4`)
  - Attributes: `since`=`20.2.0.7`
  - Pivot in B coordinates.
- **Min Distance** (`float`)
  - Describe the min distance the object is able to travel.
- **Max Distance** (`float`)
  - Describe the max distance the object is able to travel.
- **Friction** (`float`)
  - Friction.
- **Motor** (`bhkConstraintMotorCInfo`)
  - Attributes: `since`=`20.2.0.7`, `vercond`=`!#NI_BS_LTE_16#`

