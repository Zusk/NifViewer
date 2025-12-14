# Struct `BSStreamHeader`

Information about how the file was exported

## Attributes
- **module**: `BSMain`
- **name**: `BSStreamHeader`
- **versions**: `#BETHESDA#`

## Fields
- **BS Version** (`ulittle32`)
- **Author** (`ExportString`)
- **Unknown Int** (`uint`)
  - Attributes: `cond`=`BS Version #GT# 130`
- **Process Script** (`ExportString`)
  - Attributes: `cond`=`BS Version #LT# 131`
- **Export Script** (`ExportString`)
- **Max Filepath** (`ExportString`)
  - Attributes: `cond`=`BS Version >= 103`

