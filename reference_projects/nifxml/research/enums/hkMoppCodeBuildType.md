# Enum `hkMoppCodeBuildType`

hkpMoppCode::BuildType
A byte describing if MOPP Data is organized into chunks (PS3) or not (PC)

## Attributes
- **name**: `hkMoppCodeBuildType`
- **storage**: `byte`
- **versions**: `#SKY_AND_LATER#`

## Values
- `option` `BUILT_WITH_CHUNK_SUBDIVISION` (`value`=`0`) – Organized in chunks for PS3.
- `option` `BUILT_WITHOUT_CHUNK_SUBDIVISION` (`value`=`1`) – Not organized in chunks for PC. (Default)
- `option` `BUILD_NOT_SET` (`value`=`2`) – Build type not set yet.

