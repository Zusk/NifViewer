# Animation reference from the sample projects

This note tracks how each reference project in `reference_projects/` treats mesh animation data, with special attention to `.kf` keyframe sequences and the `kfm` variant that Gamebryo/NetImmerse often produces alongside skinned geometry.

## Common .kf roots
- NetImmerse `.kf` files still use the `NiSequence` root and the usual block/controlled-block tables (`doc/NiSequence.html:7-24`).
- Gamebryo `.kf` files (versions ≥10.0.1.0) switch to `NiControllerSequence` as the root so they can own interpolators, controller links, and `NiTextKeyExtraData` markers (`doc/NiControllerSequence.html:1-32`).
- Even newer Gamebryo dumps (20.5.0.1+) push the same data onto `NiSequenceData` (`doc/NiSequenceData.html:1-32`).
- Names of interpolated targets are often kept in a `NiStringPalette` that is carved out from the animation root to avoid repeated strings (`doc/NiStringPalette.html:7-25`).

## niflib-develop
- The public C++ API exposes these roots and the helper objects that let you attach controllers vs. interpolators, down to explicit `AddController`, `AddInterpolator`, and `ControllerLink` helpers (`reference_projects/niflib-develop/include/obj/NiControllerSequence.h:33-155`).
- `NiStringPalette` is modelled as a collection of 0x00-separated names that animation exporters can reference when writing controller links (`reference_projects/niflib-develop/include/obj/NiStringPalette.h:26-104`).
- Named animation segments are captured via `NiTextKeyExtraData`, which is what the `.kf` root hands to any importer that needs sample labels or event notes (`reference_projects/niflib-develop/include/obj/NiTextKeyExtraData.h:22-91`).
- No explicit `.kfm` classes appear in the headers here, so `niflib` treats `.kfm` the same as any other `.kf` file—by reading whichever root appears in the stream (NiSequence, NiControllerSequence, or NiSequenceData) and translating its controller/target links into the library’s standard objects.

## nif-main
- `src/blocks/mod.rs` wires every Ni block type the parser currently knows about, including `NiTimeController`, `NiInterpController`, `NiSingleInterpController`, the various controller/interpolator data blocks, and the skin/mesh definitions used by animated meshes (`reference_projects/nif-main/src/blocks/mod.rs:1-185`).
- Each animation block is defined via `BinRead` structs; for example, `NiTimeController` carries timing/target refs (`reference_projects/nif-main/src/blocks/ni_main/ni_time_controller.rs:1-18`) while `NiSingleInterpController` plugs in an interpolator ref (`reference_projects/nif-main/src/blocks/ni_animation/ni_single_interp_controller.rs:1-17`).
- The block/target graph is stitched together through `BlockRef` (`reference_projects/nif-main/src/common/refs.rs:1-19`), ensuring that controllers live in the same block list as the rest of the scene.
- The collector modules (e.g., `collectors/single_mesh.rs`) demonstrate how the parsed nodes/meshes are transformed into GL-friendly data, even though animation blending is not yet implemented there (`reference_projects/nif-main/src/collectors/single_mesh.rs:1-127`).
- There is still no dedicated `.kfm` format handler, but the block definitions cover everything a `.kf` file would need, so a future `.kfm` parser could be built on this mesh of enums and controllers.

## NifSkope
- `spAttachKf` loads `.kf` files via `NifModel::load()` and insists that the root blocks are `NiControllerSequence` nodes before it matches their “Accum Root Name” values to nodes in the currently loaded `.nif` (`reference_projects/nifskope-develop/src/spells/animation.cpp:13-74`).
- It collects the `Controlled Blocks` names inside each sequence and wires them through a `NiMultiTargetTransformController`/`NiControllerManager` pair so the animation still has a controller and palette on the `.nif` side (`reference_projects/nifskope-develop/src/spells/animation.cpp:76-170`).
- Any missing controlled nodes are listed as errors, and once all sequences have been attached the spell moves the `.kf` blocks into the `.nif` file so they live alongside the mesh again (`reference_projects/nifskope-develop/src/spells/animation.cpp:132-170`).

## NifToOpenGL
- `NifNode.cs` implements the node hierarchy plus controller plumbing used inside Civ4 `.nif` readers. `NodeController` and its children read controller timing, flags, and the index of the target node so multiple controllers can link to the same object (`reference_projects/NifToOpenGL/NifNode.cs:66-133`).
- The same file exposes specific controller implementations (`NiTransformController`, `NiAlphaController`, `NiVisController`, etc.), which indicates the app consumes whatever controller data is baked into the NIF rather than relying on a separate `.kf` loader.
- The binary reader helpers in `NifMain.cs` decode bones, partitions, and vertex weights (`reference_projects/NifToOpenGL/NifMain.cs:95-195`), allowing it to render skinned meshes simply by replaying the controllers already embedded in the mesh blocks.

## OpenMW
- `OpenMW-Approach/openmw/components/nif/controller.hpp` defines `ControlledBlock` and all the usual Time/Interp controller variants, so the parser knows how to attach interpolators, palette offsets, and controller IDs to every controlled node (`reference_projects/OpenMW-Approach/openmw/components/nif/controller.hpp:11-194`).
- `NiControllerSequence::read()` follows the Gamebryo layout (weight, frequency, start/stop, manager, string palette, and text keys) before handing the data to the `NiControllerManager` so OpenMW can drive controller blending or export it elsewhere (`reference_projects/OpenMW-Approach/openmw/components/nif/controller.cpp:107-166`).
- The `NiInterpController`/`NiSingleInterpController` implementations keep reading the shared timing data and an interpolator pointer, so any `.kf` driven animation block is fully resolved before the renderer or physics system uses it (`reference_projects/OpenMW-Approach/openmw/components/nif/controller.cpp:144-166`).

## Reference gaps
- PyFFI no longer appears in the `reference_projects/` tree, so there are no `pyffi/formats/kf` or `pyffi/formats/kfm` XML definitions available here to compare.

## Summary
- `niflib-develop` is the most explicit `.kf` reference, with C++ objects that mirror the documented roots and helper objects.
- `nif-main` has the raw data definitions that would let it read controllers/interpolators from any `.kf` stream, and it already builds the block graph needed for animation linking.
- `NifSkope` demonstrates how to graft `.kf` sequences back onto a `.nif` scene via `NiControllerSequence`, `NiMultiTargetTransformController`, and `NiControllerManager`.
- `NifToOpenGL` focuses on controllers baked into `.nif` meshes (the `NodeController` classes and the bone reader) but lacks external `.kf/.kfm` loaders.
- `OpenMW` now ships real source for controller/sequence parsing and drives the set of Ni interpolators, palettes, and controller managers that a `.kf` file describes.
