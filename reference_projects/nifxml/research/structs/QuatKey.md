# Struct `QuatKey`

A special version of the key type used for quaternions. Never has tangents. #T# should always be Quaternion.

## Attributes
- **generic**: `true`
- **module**: `NiMain`
- **name**: `QuatKey`

## Fields
- **Time** (`float`)
  - Attributes: `until`=`10.1.0.0`
  - Time the key applies.
- **Time** (`float`)
  - Attributes: `cond`=`#ARG# != 4`, `since`=`10.1.0.106`
  - Time the key applies.
- **Value** (`#T#`)
  - Attributes: `cond`=`#ARG# != 4`
  - Value of the key.
- **TBC** (`TBC`)
  - Attributes: `cond`=`#ARG# == 3`
  - The TBC of the key.

