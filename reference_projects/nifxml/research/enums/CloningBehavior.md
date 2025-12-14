# Enum `CloningBehavior`

Sets how objects are to be cloned.

## Attributes
- **module**: `NiMesh`
- **name**: `CloningBehavior`
- **since**: `V20_5_0_0`
- **storage**: `uint`

## Values
- `option` `CLONING_SHARE` (`value`=`0`) – Share this object pointer with the newly cloned scene.
- `option` `CLONING_COPY` (`value`=`1`) – Create an exact duplicate of this object for use with the newly cloned scene.
- `option` `CLONING_BLANK_COPY` (`value`=`2`) – Create a copy of this object for use with the newly cloned stream, leaving some of the data to be written later.

