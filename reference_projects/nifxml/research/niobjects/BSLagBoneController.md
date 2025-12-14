# Niobject `BSLagBoneController`

A controller that trails a bone behind an actor.

## Attributes
- **inherit**: `NiTimeController`
- **module**: `BSAnimation`
- **name**: `BSLagBoneController`
- **versions**: `#SKY_AND_LATER#`

## Fields
- **Linear Velocity** (`float`)
  - Attributes: `default`=`3.0`, `range`=`0.0:500.0`
  - How long it takes to rotate about an actor back to rest position.
- **Linear Rotation** (`float`)
  - Attributes: `default`=`1.0`, `range`=`0.0:15.0`
  - How the bone lags rotation
- **Maximum Distance** (`float`)
  - Attributes: `default`=`400.0`, `range`=`#F0_1000#`
  - How far bone will tail an actor.

