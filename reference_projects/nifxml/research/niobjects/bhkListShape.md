# Niobject `bhkListShape`

A list of shapes.

Shapes collected in a bhkListShape may not have the correct collision sound/FX due to HavokMaterial issues.
Do not put a bhkPackedNiTriStripsShape in the Sub Shapes. Use a separate collision nodes without a list shape for those.

## Attributes
- **inherit**: `bhkShapeCollection`
- **module**: `BSHavok`
- **name**: `bhkListShape`
- **versions**: `#BETHESDA#`

## Fields
- **Num Sub Shapes** (`uint`)
  - Attributes: `default`=`1`, `range`=`1:256`
- **Sub Shapes** (`Ref`)
  - Attributes: `length`=`Num Sub Shapes`, `template`=`bhkShape`
  - List of shapes. Max of 256.
- **Material** (`HavokMaterial`)
  - The material of the shape.
- **Child Shape Property** (`bhkWorldObjCInfoProperty`)
- **Child Filter Property** (`bhkWorldObjCInfoProperty`)
- **Num Filters** (`uint`)
  - Attributes: `calc`=`#LEN[Sub Shapes]#`
- **Filters** (`HavokFilter`)
  - Attributes: `length`=`Num Filters`
  - Always zeroed. Seemingly unused, or 0 for all values means no override.

