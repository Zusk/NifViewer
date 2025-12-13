# Integration guide for a new animation component

To help another agent build animation on top of the existing viewer, sketch how and where the new code should plug into the current layers.

## Frame loop & scene-object hook
- `RenderWindow` keeps a list of `ISceneObject` instances and calls `Update` + `Render` for each object every `OnRenderFrame` (`RenderWindow.cs:85-147`). Your animation component should implement `ISceneObject` (see `ISceneObject.cs:1-16`) so the renderer can tick it automatically.
- The easiest approach is to wrap your animation state inside a subclass alongside or instead of `NifModelSceneObject`. `NifModelSceneObject` already loads the static model and rotates it (`NifModelSceneObject.cs:1-66`), so you can either extend that class with bone/controller support or create a new scene object that orchestrates the loader plus animation baking.

## Feeding skeleton data into the renderer
- `Civ4NifLoader` returns a `Model` that currently contains baked vertex data and materials (`Civ4NifLoader.cs:14-205`). Augment it to also expose the `NifContext` used during parsing, so you retain access to `NiNodeBlock`, `NiSkinInstance`, and controller references without re-parsing the file.
- Since the loader already computes world transforms (`Civ4NifLoader.cs:96-178`), keep that dictionary around so animation code can reapply controller-driven transforms on top before the mesh is drawn (pass them into a `MeshSkinner` or similar helper before `Model.Draw`).

## Connecting controllers & `.kf` logic
- Reference the research notes (`research/mesh-skeleton-animation.md` and `research/animation-reference.md`) for Gamebryo’s controller stack: `.kf` files contain `NiControllerSequence` roots, `ControlledBlock` entries, and `NiStringPalette` name offsets that point to `NiNode` objects. Use those docs as a map for reading `.kf` data and wiring it into the nodes you already have in `NifContext`.
- The integration path is typically: load the `.kf`, find each `NiControllerSequence`, resolve `Accum Root Name` to the matching node in your scene graph, then attach its interpolators/controllers to that node while tracking a `NiControllerManager`. `research/animation-reference.md:25-37` and `research/mesh-skeleton-animation.md:13-33` walk through how reference projects attach sequences at runtime.

## Rendering animated meshes
- Once controllers update node transforms, the animation component should either:
  1. bake the node’s transform into the associated mesh transform before calling `_model.Draw` (to reuse the existing renderer), or
  2. recompute deformed vertex positions/normals per frame and re-upload them before each draw (if you need vertex-level animation).
- Tie this transform application to the `Update` phase of your new `ISceneObject`. You already know `NifModelSceneObject.Update` increments `_time` (`NifModelSceneObject.cs:1-66`), so the animation component should follow the same pattern but drive skeleton transforms/populate controller timers.

## Suggested structure for the new component
1. **Loader extension** – modify `Civ4NifLoader` to return `(Model model, NifContext context, Dictionary<int, TransformData> bakedNodeTransforms)` so animation data is preserved.
2. **Skeleton cache** – create a skeleton map that links `NiNodeBlock` entries to bone indices and tracks the `NiSkinInstance` -> `NiSkinData` weight data described in `research/mesh-skeleton-animation.md:1-32`.
3. **Sequence runner** – implement a controller manager that consumes `NiControllerSequence` blocks (see `reference_projects/OpenMW-Approach/openmw/components/nif/controller.cpp:107-166`) and updates each bone transform based on interpolator data.
4. **Scene object** – wrap the above in an `ISceneObject` so `RenderWindow` can invoke it each frame; the scene object should update the controller manager and ensure the `Model`/`Mesh` draw call references the latest transforms.

## Documentation references
- Use the architecture overview (`research/architecture-overview.md`) to choose where to create new interfaces or data paths.
- The data contract doc (`research/data-contract.md`) describes exactly what data `Civ4NifLoader` already exposes, which helps you decide whether to extend `Model`, add a new `Skeleton` container, or reuse the existing `Mesh` structures.

When the animation component is wired in, the rendering loop remains unchanged—the logic simply injects animation state before each draw, guided by the `ISceneObject` contract.
