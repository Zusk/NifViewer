# Struct `BSPackedGeomObject`

This is the data necessary to access the shared geometry in a PSG/CSG file.

## Attributes
- **module**: `BSMain`
- **name**: `BSPackedGeomObject`
- **size**: `8`
- **versions**: `#FO4# #F76#`

## Fields
- **Filename Hash** (`uint`)
  - BSCRC32 of the filename without the PSG/CSG extension.
- **Data Offset** (`uint`)
  - Offset of the geometry data in the PSG/CSG file.

