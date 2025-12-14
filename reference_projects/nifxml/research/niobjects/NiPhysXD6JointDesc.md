# Niobject `NiPhysXD6JointDesc`

A 6DOF (6 degrees of freedom) joint.

## Attributes
- **inherit**: `NiPhysXJointDesc`
- **module**: `NiPhysX`
- **name**: `NiPhysXD6JointDesc`
- **since**: `V20_2_0_8`

## Fields
- **X Motion** (`NxD6JointMotion`)
  - Attributes: `default`=`MOTION_FREE`
- **Y Motion** (`NxD6JointMotion`)
  - Attributes: `default`=`MOTION_FREE`
- **Z Motion** (`NxD6JointMotion`)
  - Attributes: `default`=`MOTION_FREE`
- **Swing 1 Motion** (`NxD6JointMotion`)
  - Attributes: `default`=`MOTION_FREE`
- **Swing 2 Motion** (`NxD6JointMotion`)
  - Attributes: `default`=`MOTION_FREE`
- **Twist Motion** (`NxD6JointMotion`)
  - Attributes: `default`=`MOTION_FREE`
- **Linear Limit** (`NxJointLimitSoftDesc`)
- **Swing 1 Limit** (`NxJointLimitSoftDesc`)
- **Swing 2 Limit** (`NxJointLimitSoftDesc`)
- **Twist Low Limit** (`NxJointLimitSoftDesc`)
- **Twist High Limit** (`NxJointLimitSoftDesc`)
- **X Drive** (`NxJointDriveDesc`)
- **Y Drive** (`NxJointDriveDesc`)
- **Z Drive** (`NxJointDriveDesc`)
- **Swing Drive** (`NxJointDriveDesc`)
- **Twist Drive** (`NxJointDriveDesc`)
- **Slerp Drive** (`NxJointDriveDesc`)
- **Drive Position** (`Vector3`)
- **Drive Orientation** (`Quaternion`)
- **Drive Linear Velocity** (`Vector3`)
- **Drive Angular Velocity** (`Vector3`)
- **Projection Mode** (`NxJointProjectionMode`)
- **Projection Distance** (`float`)
  - Attributes: `default`=`0.1`
- **Projection Angle** (`float`)
  - Attributes: `default`=`0.0872`
- **Gear Ratio** (`float`)
  - Attributes: `default`=`1.0`
- **Flags** (`uint`)

