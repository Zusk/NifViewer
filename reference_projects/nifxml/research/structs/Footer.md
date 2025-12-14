# Struct `Footer`

The NIF file footer.

## Attributes
- **module**: `NiMain`
- **name**: `Footer`

## Fields
- **Num Roots** (`uint`)
  - Attributes: `since`=`3.3.0.13`
  - The number of root references.
- **Roots** (`Ref`)
  - Attributes: `length`=`Num Roots`, `since`=`3.3.0.13`, `template`=`NiObject`
  - List of root NIF objects. If there is a camera, for 1st person view, then this NIF object is referred to as well in this list, even if it is not a root object (usually we want the camera to be attached to the Bip Head node).

