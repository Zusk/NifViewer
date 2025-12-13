# Data contract: NIF meshes, skeletons, and animation data

This project currently represents NIF data through a few key structures so an animation component knows what shape of data to expect:

## Parsed NIF graph (`NifContext`)
- `NifContext` stores the header metadata (versions, endianness, block counts), the string palette, and the full `object[] Blocks` array that mirrors the NIF block list; loaders resolve typed blocks through `GetBlock<T>(index)` (`NifContext.cs:1-43`).
- `Civ4NifLoader` extends that graph with small typed wrappers such as `NiTriShapeBlock`, `NiTriStripsBlock`, `NiTriShapeDataBlock`, and controller blocks further down in the file; these wrappers expose translation/rotation/scale, property indices, and payload pointers that are referenced from `NifContext.Blocks` (`Civ4NifLoader.cs:1341-1576`).
- Only the geometry blocks (`NiTriShapeBlock`, `NiTriStripsBlock`) and their `DataRef` entries are consumed today; the loader still records shader/material references, collision skins, and (unconsumed) controller/SkinInstance indices so you can graft animation handling on top later (`Civ4NifLoader.cs:39-188`, `Civ4NifLoader.cs:1341-1576`).

## Geometry payloads
- `NiTriShapeDataBlock` carries flat arrays of vertices, normals, tangents, bitangents, vertex colors, UV sets, and the triangle index list that define the mesh (`Civ4NifLoader.cs:1495-1548`).
- `NiTriStripsDataBlock` keeps similar arrays but also includes strip lengths/points so the loader can convert strips to triangle lists before building a GPU mesh (`Civ4NifLoader.cs:1550-1577`).
- Triangles are represented as `struct Triangle { ushort A, B, C; }` (`Civ4NifLoader.cs:1578-1589`), matching the 0-based index layout expected by OpenGL.

## Runtime mesh/material representation
- `Model` aggregates `Mesh` + `Material` pairs; each `Mesh` owns interleaved `(position, normal, uv)` data plus the VAO/VBO/EBO handles required by OpenGL, while `Material` stores Phong colors and an optional `Texture` and exposes `ApplyToShader` before a draw (`Model.cs:1-46`, `Mesh.cs:4-122`, `Material.cs:1-38`).
- `Texture.Load` finds the actual image on disk (preferring DDS, falling back to STB) and uploads it with mipmaps so shader samplers can bind it (`Texture.cs:1-93`).

## Transform data
- The loader optionally precomputes every block’s world transform via `BuildWorldTransforms` and `TraverseNode`; each `NiNodeBlock` contributes translation/rotation/scale, and the resulting dictionary is used to bake transforms into vertex positions/normals when `bakeTransforms` is enabled (`Civ4NifLoader.cs:96-178`).
- The `Model` retains the original vertex positions/normals (stored separately from the interleaved GPU buffer) so a future animation system can recompute deformed positions if necessary.

## Skeleton & animation placeholders
- Although `NiSkinInstance`/`NiSkinData` follow the Gamebryo pattern of weighted bones, the current loader only records references such as `SkinInstanceRef` from `NiTriShapeBlock` and never materializes bones (`Civ4NifLoader.cs:1341-1375`).
- Animation controllers are present in the block graph (`NiTransformControllerBlock`, `NiMultiTargetTransformControllerBlock`, etc. near `Civ4NifLoader.cs:1516-1550`), but they are unused right now. The reference research notes (`research/animation-reference.md` and `research/mesh-skeleton-animation.md`) describe how Gamebryo packs this data so a new component can hook into those blocks and the string-palettized controller attachments.

Any animation component you build should therefore expect: an `NifContext` with block pointers, `NiTriShapeDataBlock` payloads for raw geometry, per-node world transforms, and placeholder references to controllers/skin instances that can be expanded when hooking into `.kf` sequences.
