# Niobject `NiPhysXBodyDesc`

For serializing NxBodyDesc objects.

## Attributes
- **inherit**: `NiObject`
- **module**: `NiPhysX`
- **name**: `NiPhysXBodyDesc`
- **since**: `V20_2_0_8`

## Fields
- **Local Pose** (`Matrix34`)
- **Space Inertia** (`Vector3`)
- **Mass** (`float`)
- **Num Vels** (`uint`)
- **Vels** (`PhysXBodyStoredVels`)
  - Attributes: `length`=`Num Vels`
- **Wake Up Counter** (`float`)
  - Attributes: `default`=`0.4`
- **Linear Damping** (`float`)
- **Angular Damping** (`float`)
  - Attributes: `default`=`0.05`
- **Max Angular Velocity** (`float`)
  - Attributes: `default`=`-1.0`
- **CCD Motion Threshold** (`float`)
- **Flags** (`NxBodyFlag`)
  - Attributes: `default`=`0x900`
- **Sleep Linear Velocity** (`float`)
  - Attributes: `default`=`-1.0`
- **Sleep Angular Velocity** (`float`)
  - Attributes: `default`=`-1.0`
- **Solver Iteration Count** (`uint`)
  - Attributes: `default`=`4`
- **Sleep Energy Threshold** (`float`)
  - Attributes: `default`=`-1.0`, `since`=`20.3.0.0`
- **Sleep Damping** (`float`)
  - Attributes: `since`=`20.3.0.0`
- **Contact Report Threshold** (`float`)
  - Attributes: `default`=`#FLT_MAX#`, `since`=`20.4.0.0`

