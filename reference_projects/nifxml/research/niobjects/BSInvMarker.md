# Niobject `BSInvMarker`

Orientation marker for Skyrim's inventory view.
    How to show the nif in the player's inventory.
    Typically attached to the root node of the nif tree.
    If not present, then Skyrim will still show the nif in inventory,
    using the default values.
    Name should be 'INV' (without the quotes).
    For rotations, a short of "4712" appears as "4.712" but "959" appears as "0.959"  meshes\weapons\daedric\daedricbowskinned.nif

## Attributes
- **inherit**: `NiExtraData`
- **module**: `BSMain`
- **name**: `BSInvMarker`
- **versions**: `#SKY_AND_LATER#`

## Fields
- **Rotation X** (`ushort`)
  - Attributes: `default`=`0`
- **Rotation Y** (`ushort`)
  - Attributes: `default`=`0`
- **Rotation Z** (`ushort`)
  - Attributes: `default`=`0`
- **Zoom** (`float`)
  - Attributes: `default`=`1.0`
  - Zoom factor.

