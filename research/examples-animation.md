# Animation example sketches based on the reference projects

Use these sketches to translate the reference logic into your codebase once the architecture/data contract is in place.

## Example 1: Attach a `.kf` to the existing scene object (NifSkope-style)
1. Parse the `.nif` into `Model`/`NifContext` as usual, but keep the context around so you can resolve node names (`research/data-contract.md`).
2. Load a `.kf` file, iterate its `NiControllerSequence` roots, and match each sequence’s `Accum Root Name` to the target node in your `NifContext` by name (`research/animation-reference.md:25-33`).
3. For each `Controlled Block`, gather the node names (or palette offsets) and resolve those nodes in the scene graph; `spAttachKf`’s `findChildNode`/`findController` logic illustrates the name-based lookup you need (`reference_projects/nifskope-develop/src/spells/animation.cpp:57-170`).
4. Create or reuse `NiControllerManager` + `NiMultiTargetTransformController` objects attached to the root node, then move the `.kf` controller blocks into your current model and update the controller links so they point at the runtime nodes (see how `spAttachKf` attaches `NiControllerSequence` blocks to the existing `.nif`).
5. Update the controller manager every frame (inside your new `ISceneObject.Update`) so each Ni interpolator updates the bone transforms, and reapply those transforms to the meshes before drawing.

## Example 2: Build a controller manager + skin weights pipeline (OpenMW-style)
1. Inspect the `NifContext.Blocks` array for `NiSkinInstance`/`NiSkinData` entries (they currently exist as references in `NiTriShapeBlock.SkinInstanceRef`) and collect the bone indices + weight lists as described in `research/mesh-skeleton-animation.md:1-32`.
2. Build a lightweight `ControlledBlock` struct that mirrors OpenMW’s version (node name, interpolator reference, controller IDs, string palette offsets) using the layout documented in `research/animation-reference.md` and `reference_projects/OpenMW-Approach/openmw/components/nif/controller.hpp:11-194`.
3. Implement a `NiControllerManager` that can host multiple `NiControllerSequence` objects, honor their priorities, and blend transforms just like OpenMW’s manager (`reference_projects/OpenMW-Approach/openmw/components/nif/controller.cpp:107-166`).
4. Each animation frame, tick the active controllers, compute the per-bone matrices, and apply them to the skinned mesh either by updating the transform map passed to `Mesh` or by re-uploading the deformed vertex buffers.
5. Keep the mesh/animation separation explicit so you can reuse the same `Model` with different `.kf` sequences; this mirrors how OpenMW keeps animations external and attaches them via the controller/palette system.

By leaning on these examples, another agent can reproduce the reference workflows while wiring them into the existing renderer through the `ISceneObject` and `Civ4NifLoader` hooks.
