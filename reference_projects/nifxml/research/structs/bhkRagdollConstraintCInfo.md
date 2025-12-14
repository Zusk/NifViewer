# Struct `bhkRagdollConstraintCInfo`

Serialization data for bhkRagdollConstraint.
The area of movement can be represented as a main cone + 2 orthogonal cones which may subtract from the main cone volume depending on limits.

## Attributes
- **module**: `BSHavok`
- **name**: `bhkRagdollConstraintCInfo`
- **versions**: `#BETHESDA#`

## Fields
- **Pivot A** (`Vector4`)
  - Attributes: `vercond`=`#NI_BS_LTE_16#`
  - The point where the constraint is attached to its parent rigidbody.
- **Plane A** (`Vector4`)
  - Attributes: `vercond`=`#NI_BS_LTE_16#`
  - Defines the orthogonal plane in which the body can move, the orthogonal directions in which the shape can be controlled (the direction orthogonal on this one and Twist A).
- **Twist A** (`Vector4`)
  - Attributes: `vercond`=`#NI_BS_LTE_16#`
  - Central directed axis of the cone in which the object can rotate. Orthogonal on Plane A.
- **Pivot B** (`Vector4`)
  - Attributes: `vercond`=`#NI_BS_LTE_16#`
  - The point where the constraint is attached to the other rigidbody.
- **Plane B** (`Vector4`)
  - Attributes: `vercond`=`#NI_BS_LTE_16#`
  - Defines the orthogonal plane in which the shape can be controlled (the direction orthogonal on this one and Twist B).
- **Twist B** (`Vector4`)
  - Attributes: `vercond`=`#NI_BS_LTE_16#`
  - Central directed axis of the cone in which the object can rotate. Orthogonal on Plane B.
- **Twist A** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Central directed axis of the cone in which the object can rotate. Orthogonal on Plane A.
- **Plane A** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Defines the orthogonal plane in which the body can move, the orthogonal directions in which the shape can be controlled (the direction orthogonal on this one and Twist A).
- **Motor A** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Defines the orthogonal directions in which the shape can be controlled (namely in this direction, and in the direction orthogonal on this one and Twist A).
- **Pivot A** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Point around which the object will rotate. Defines the orthogonal directions in which the shape can be controlled (namely in this direction, and in the direction orthogonal on this one and Twist A).
- **Twist B** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Central directed axis of the cone in which the object can rotate. Orthogonal on Plane B.
- **Plane B** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Defines the orthogonal plane in which the body can move, the orthogonal directions in which the shape can be controlled (the direction orthogonal on this one and Twist A).
- **Motor B** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Defines the orthogonal directions in which the shape can be controlled (namely in this direction, and in the direction orthogonal on this one and Twist A).
- **Pivot B** (`Vector4`)
  - Attributes: `vercond`=`!#NI_BS_LTE_16#`
  - Defines the orthogonal directions in which the shape can be controlled (namely in this direction, and in the direction orthogonal on this one and Twist A).
- **Cone Max Angle** (`float`)
  - Maximum angle the object can rotate around the vector orthogonal on Plane A and Twist A relative to the Twist A vector. Note that Cone Min Angle is not stored, but is simply minus this angle.
- **Plane Min Angle** (`float`)
  - Minimum angle the object can rotate around Plane A, relative to Twist A.
- **Plane Max Angle** (`float`)
  - Maximum angle the object can rotate around Plane A, relative to Twist A.
- **Twist Min Angle** (`float`)
  - Minimum angle the object can rotate around Twist A, relative to Plane A.
- **Twist Max Angle** (`float`)
  - Maximum angle the object can rotate around Twist A, relative to Plane A.
- **Max Friction** (`float`)
  - Maximum friction, typically 0 or 10. In Fallout 3, typically 100.
- **Motor** (`bhkConstraintMotorCInfo`)
  - Attributes: `since`=`20.2.0.7`, `vercond`=`!#NI_BS_LTE_16#`

