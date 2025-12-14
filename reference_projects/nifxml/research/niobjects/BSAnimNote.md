# Niobject `BSAnimNote`

Bethesda-specific object.

## Attributes
- **inherit**: `NiObject`
- **module**: `BSAnimation`
- **name**: `BSAnimNote`
- **versions**: `#FO3_AND_LATER#`

## Fields
- **Type** (`AnimNoteType`)
  - Type of this note.
- **Time** (`float`)
  - Location in time.
- **Arm** (`uint`)
  - Attributes: `cond`=`Type == 1`
- **Gain** (`float`)
  - Attributes: `cond`=`Type == 2`
- **State** (`uint`)
  - Attributes: `cond`=`Type == 2`

