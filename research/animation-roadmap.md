# Animation system roadmap

This roadmap chains the research notes together into a concrete plan for implementing animation on top of the current viewer.

1. **Gather the base data**  
   - Ensure `Civ4NifLoader` exposes the `NifContext`/block array, the precomputed world-transforms, and the raw `NiTriShape`/`NiTriStrips` meshes rather than discarding them after baking (`research/data-contract.md:1-28`).  
   - Record the `SkinInstanceRef`/controller block references that already exist alongside each geometry block so bones and controllers can be looked up later (`research/data-contract.md:15-32`).

2. **Map the skeleton**  
   - Parse `NiSkinInstance`/`NiSkinData`/`NiSkinPartition` from the context so you have bone lists, bind transforms, and weight tables (see `research/mesh-skeleton-animation.md:1-16`).  
   - Build a runtime skeleton that links each bone to an `NiNode` from the `NifContext`, preserving the Gamebryo node names required by controllers (`research/architecture-overview.md:1-33`).

3. **Wire animation controllers**  
   - Use the controller blocks (NiTimeController, NiInterpController, NiSingleInterpController) already parsed in the context; document their fields in `research/animation-reference.md:13-37` and `reference_projects/OpenMW-Approach/openmw/components/nif/controller.hpp:11-194`.  
   - Load `.kf` sequences (per `research/animation-reference.md:25-37`) and attach them to the skeleton via `NiControllerSequence`, `ControlledBlock`, and `NiStringPalette` lookups.
   - Implement a `NiControllerManager` (OpenMW-style) to blend multiple sequences and aggregate priorities (`research/mesh-skeleton-animation.md:27-33`).

4. **Runtime integration**  
   - Create a new `ISceneObject` (or extend `NifModelSceneObject`) that owns the animation controller manager and updates it every frame (`research/integration-guide.md:1-30`).  
   - Before invoking `Model.Draw`, apply the current skeleton transforms either by updating world matrices (faster) or rebuilding the positional/normals buffers if you need per-vertex deformation (`research/integration-guide.md:31-40`).  
   - Keep the render loop unchanged: the animation scene object should still fit into `RenderWindow`’s `_sceneObjects` list (`research/architecture-overview.md:1-33`).

5. **Examples & validation**  
   - Reproduce the `spAttachKf` flow from NifSkope (matching `Accum Root Name` to nodes, attaching NiControllerSequence blocks) and the OpenMW controller manager sketch (`research/examples-animation.md:1-32`).  
   - Validate by playing a sample `.kf` sequence: ensure controlled nodes update, check missing nodes are reported, and compare against the baked (non-animated) fallback.

After these stages, the animation system will have a data-rich base, a working skeleton, a controller runner, and a renderer-aware scene object. Use the research documents as a reference at each step to keep the implementation aligned with the Gamebryo conventions already described in this repo.
