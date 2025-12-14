# Struct `NiAGDDataStream`

## Attributes
- **module**: `NiMain`
- **name**: `NiAGDDataStream`

## Fields
- **Type** (`uint`)
  - Type of data in this channel
- **Unit Size** (`uint`)
  - Number of bytes per element of this channel
- **Total Size** (`uint`)
  - Total number of bytes of this channel (num vertices times num bytes per element)
- **Stride** (`uint`)
  - Number of bytes per element in all channels together. Sum of num channel bytes per element over all block infos.
- **Block Index** (`uint`)
  - Unsure. The block in which this channel is stored? Usually there is only one block, and so this is zero.
- **Block Offset** (`uint`)
  - Offset (in bytes) of this channel. Sum of all num channel bytes per element of all preceeding block infos.
- **Flags** (`NiAGDDataStreamFlags`)
  - Attributes: `default`=`2`

