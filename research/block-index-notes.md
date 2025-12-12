# Civ 4 NIF Block & Index Notes

Observations below are from `Svart_Monk.nif` (declared as Gamebryo 20.0.0.4) cross‑referenced with the specs under `doc/` and the hints in `IMPORTANT_READ_ME.txt`.

## Header layout
- Header string: `Gamebryo File Format, Version 20.0.0.4\n`.
- Version (`FileVersion`): `0x14000004`.
- Endian: little (`EndianType` value `1`).
- User version fields are zeroed; the file jumps straight to block metadata after `Num Blocks = 0x41 (65)`.
- `Num Block Types = 12` followed by 12 `SizedString` entries (see `doc/Header.html`):  
  `NiNode`, `NiCollisionData`, `NiTriShape`, `NiTexturingProperty`, `NiSourceTexture`, `NiAlphaProperty`, `NiMaterialProperty`, `NiAlphaController`, `NiTriShapeData`, `NiStencilProperty`, `NiSkinInstance`, `NiSkinData`.
- `BlockTypeIndex` array (documented in `doc/BlockTypeIndex.html`) begins at offset `0x110`. Each entry is a 16‑bit little‑endian type id; there are 65 entries, one per block.
- The fields that normally follow in modern NIFs (`Block Size`, `Strings`, `Groups`) are stubbed: at offset `0x190` the file stores `Num Strings = 0`, `Max String Length = 15`, and then immediately starts block data. This matches the note in `IMPORTANT_READ_ME.txt` that strings are inlined.

## Block order derived from the index

The table below shows how the 65 block slots map to concrete object types. This comes straight from the `BlockTypeIndex` array and is the foundation for resolving every `Ref`/`Ptr` later in the file.

| # | Type |
| -- | -- |
| 0 | NiNode |
| 1 | NiNode |
| 2 | NiNode |
| 3 | NiNode |
| 4 | NiCollisionData |
| 5 | NiNode |
| 6 | NiCollisionData |
| 7 | NiNode |
| 8 | NiCollisionData |
| 9 | NiNode |
| 10 | NiCollisionData |
| 11 | NiNode |
| 12 | NiCollisionData |
| 13 | NiNode |
| 14 | NiCollisionData |
| 15 | NiNode |
| 16 | NiCollisionData |
| 17 | NiNode |
| 18 | NiCollisionData |
| 19 | NiNode |
| 20 | NiCollisionData |
| 21 | NiNode |
| 22 | NiCollisionData |
| 23 | NiNode |
| 24 | NiCollisionData |
| 25 | NiNode |
| 26 | NiCollisionData |
| 27 | NiNode |
| 28 | NiCollisionData |
| 29 | NiNode |
| 30 | NiCollisionData |
| 31 | NiNode |
| 32 | NiCollisionData |
| 33 | NiNode |
| 34 | NiCollisionData |
| 35 | NiNode |
| 36 | NiCollisionData |
| 37 | NiTriShape |
| 38 | NiTexturingProperty |
| 39 | NiSourceTexture |
| 40 | NiSourceTexture |
| 41 | NiAlphaProperty |
| 42 | NiMaterialProperty |
| 43 | NiAlphaController |
| 44 | NiTriShapeData |
| 45 | NiNode |
| 46 | NiCollisionData |
| 47 | NiNode |
| 48 | NiCollisionData |
| 49 | NiNode |
| 50 | NiCollisionData |
| 51 | NiNode |
| 52 | NiCollisionData |
| 53 | NiNode |
| 54 | NiCollisionData |
| 55 | NiNode |
| 56 | NiCollisionData |
| 57 | NiTriShape |
| 58 | NiStencilProperty |
| 59 | NiMaterialProperty |
| 60 | NiTexturingProperty |
| 61 | NiSourceTexture |
| 62 | NiTriShapeData |
| 63 | NiSkinInstance |
| 64 | NiSkinData |

Blocks 0‑36 and 45‑56 are the bone/collision tree (alternating `NiNode` and `NiCollisionData`). The renderable geometry and material stack sits in blocks 37‑44 and 57‑64.

## How indices are used inside blocks

- `Ref` and `Ptr` (see `doc/Ref.html`) are stored as signed 32-bit little-endian integers. `-1 (0xFFFFFFFF)` still means “no reference”. This matches the README claim that the UI order matches the binary order.
- Example – root `NiNode` (block 0): at offset `0x192` the block begins with a `uint32` shader tag (`0`) followed by a sized string (`0x0F 00 00 00` + `spearman_fx.nif`). Immediately after the transform there is a block of `Ref` values; although parsing every field is still WIP, the sentinel `0xFFFFFFFF` values line up with the optional controller and extra-data slots described in `doc/NiNode.html`.
- Example – `NiTriShape` (block 37). Starting around offset `0xA50` the block’s property list contains three consecutive `Ref` values:
  - `0x0000002A` → block 42 (`NiMaterialProperty`)
  - `0x00000029` → block 41 (`NiAlphaProperty`)
  - `0x00000026` → block 38 (`NiTexturingProperty`)
  The geometry section a few bytes later holds `0x0000002C`, which resolves to block 44 (`NiTriShapeData`). This illustrates how `BlockTypeIndex` plus raw 32-bit references is enough to recover the entire scene graph linkage.
- Example – downstream `NiTriShape` stack (blocks 57-62). The same pattern repeats: property refs point back to blocks 58-61, and the geometry slot points to block 62. Finally, block 63 (`NiSkinInstance`) contains a `Ref` to block 64 (`NiSkinData`), tying the skin partition to its weights.

## NiNode hierarchy and NiCollision links

- The `NodeGeometry` layout from `reference_projects/NifToOpenGL/NifNode.cs` matches this file: `Name (SizedString)` → `Extra Data Count` + list → `Controller Ref` → `Flags (uint16)` → `Vector3 Translation` → `Matrix33 Rotation` → `float Scale` → `Property Count` + list of block refs → `Collision Object Ref`. Every `NiNode` then appends `Child Count`/`Child List` and `Effect Count`/`Effect List`. The effect arrays are empty in `Svart_Monk.nif`, but the child arrays form the entire skeleton.
- Using `research/parse_civ4_nif.py` to walk those arrays yields the exact NiNode tree. Leafs reference geometry blocks (e.g. NiTriShape 37 under `BIP R Hand`). Only the NiNode entries are shown below; geometry nodes are referenced wherever a child points to a non-NiNode index.

```
- [0] spearman_fx.nif
  - [1] monk.nif
    - [2] monk.nif
      - [3] monk
        - [5] MD
          - [7] MD NonAccum
            - [9] BIP
              - [11] BIP Pelvis
                - [13] BIP Spine
                  - [15] BIP Spine1
                    - [17] BIP Neck
                      - [19] BIP Head
                      - [21] BIP L Clavicle → 23 → 25 → 27 (hand)
                      - [29] BIP R Clavicle → 31 → 33 → 35 (hand → NiTriShape 37)
                - [45] BIP L Thigh → 47 → 49
                - [51] BIP R Thigh → 53 → 55
```

- NiCollisionData blocks come in lock-step with that list: block indices `04, 06, 08, …, 56` all store a collision record whose `target_ref` equals the preceding NiNode. (Block 4 targets NiNode 3, block 6 targets NiNode 5, etc.) Each collision block begins with a single `BlockRef` pointing back to its NiNode, followed by two enums (`PropagationMode`, `CollisionMode`) and, when `Use ABV` is set, an inlined bounding volume definition. There is no other indirection—the header’s block index table plus these signed refs are all that link collision shapes to the bones.

## String handling

- `IMPORTANT_READ_ME.txt` notes that all strings are stored as `[uint32 length][bytes]` with no shared palette except for the very first header string. That is exactly what we see: the “name” field of block 0 encodes `spearman_fx.nif` inline, and later blocks (e.g. another `NiNode` around offset `0x1FE`) do the same for `monk.nif`.
- Because `Num Strings` is zero, the `StringIndex` type described in `doc/string.html` effectively degenerates into an immediate sized string for this file family. Any parser for Civ 4 assets must therefore ignore the header string table (it is empty) and read string fields as `[uint32 length][latin1/ASCII bytes]`.

## Other 20.0.0.4 samples

- The repository now includes 26 additional Civ 4 NIFs in `reference_projects/nif-main/tests`. Their headers are byte-for-byte compatible with `Svart_Monk.nif`: header string → version → endian → user version → block/count tables → block type indices → a single `uint32` placeholder (always zero). None of them reintroduce the `Block Size`, `Strings`, or `Group` tables that the generic documentation describes.
- Block payloads begin immediately after that placeholder. Files whose first block is a named NiNode start with `0x0A 00 00 00` (`10` characters) followed by the ASCII bytes for `Scene Root`; files such as `4.nif` and `5.nif` show four zero bytes instead because the first NiNode has an empty name, but the encoding is still the same sized-string format.
- Taken together with `Svart_Monk.nif`, this suggests that the Civ 4 exporter never filled the optional header arrays for any of the files we have, so parsers can switch directly from block type indices to block payloads for version 20.0.0.4.

## Mesh extraction cheat-sheet

The goal is to feed a renderer or exporter (like `reference_projects/nif-main`) with all the data needed to rebuild meshes. The relevant blocks and relationships are:

1. **NiTriShape (mesh container)**  
   - Fields of interest (see `research/parse_civ4_nif.py`, `parse_node_geometry`):  
     `name`, `translation/rotation/scale`, `properties` (list of block refs), `data_ref` (index of `NiTriShapeData`), `skin_instance_ref` (optional `NiSkinInstance`).  
   - Each entry under `properties` points to materials: e.g. `NiTexturingProperty`, `NiMaterialProperty`, `NiAlphaProperty`, `NiStencilProperty`.

2. **NiTriShapeData (geometry payload)**  
   - Parsed via `parse_node_tri_data`:  
     - `vertices`: list of 3-float vectors when `HasVertices != 0`.  
     - `normals`, `tangents`, `bitangents` if the corresponding flags are set.  
     - `uv_sets`: `DataFlags % 4` UV arrays; each entry is `[TexCoord] * vertex_count`.  
     - `triangles`: produced from the trailing `TrianglePointsCount`, `TrianglesCount`, and `HasTriangles` section (three `uint16` per face).  
   - The `research/parse_civ4_nif.py --mesh-summary` command resolves each NiTriShape to its data block, printing vertex/triangle counts and UV-layer availability.

3. **NiTexturingProperty → NiSourceTexture (texture/material assignment)**  
   - `properties` on the NiTriShape include a `NiTexturingProperty`. The parser keeps the ordered texture slots (Base, Dark, Detail, Gloss, Glow, Bump, Decal0).  
   - Each slot stores a `Texture` struct with `source` (block index), sampling options, and an optional UV transform. The `source` points at a `NiSourceTexture`, which holds the file path (`file_path`) and mip/alpha flags.  
   - The same property list usually references `NiMaterialProperty` (ambient/diffuse/specular/emissive colors and glossiness) and `NiAlphaProperty` (threshold), which a renderer can consume alongside the texture map associations.

4. **NiSkinInstance / NiSkinData (skinned meshes)**  
   - `NiTriShape.skin_instance_ref` → `NiSkinInstance`: identifies the skeleton root and the list of bone block refs.  
   - `NiSkinInstance.data_ref` → `NiSkinData`: per-bone bind pose, optional vertex weights (`VertexWeightIndexes` + `VertexWeights`), and bounding spheres. This is only needed for meshes that animate; static props simply omit the skin fields.

### Scripted extraction example

Run the parser’s helper to inspect meshes in any 20.0.0.4 file:

```
python3 research/parse_civ4_nif.py --mesh-summary Svart_Monk.nif
```

Output snippet:

```
[37] Editable Mesh
    Data block: 44 (38 verts, 44 faces, 1 UV sets)
    Normals: 38
    Properties:
        [42] NiMaterialProperty
        [41] NiAlphaProperty
        [38] NiTexturingProperty
            Base: block 39 TeamColor.bmp
            Decal0: block 40 Settler_child_128.dds
[57] SumerianVulture_Body
    Data block: 62 (597 verts, 792 faces, 1 UV sets)
    Normals: 597
    Skin instance: block 63
    Properties:
        [58] NiStencilProperty
        [59] NiMaterialProperty
        [60] NiTexturingProperty
            Base: block 61 svartalfar1 kopie.dds
```

This ties together every requirement:
- **Vertices / Vertex info** – `NiTriShapeData.vertices`, `normals`, optional tangents/bitangents, plus transforms on the owning NiTriShape.  
- **Faces** – `NiTriShapeData.triangles`.  
- **UV mapping** – `NiTriShapeData.uv_sets` (one list per UV set).  
- **Material ↔ mesh linkage** – NiTriShape `properties` → `NiTexturingProperty`/`NiMaterialProperty`/`NiSourceTexture`.
