# Architecture overview

This viewer is split into four cooperating layers so a future animation component can attach where the data is already flowing:

## Application / window setup
- `Program.Main` parses CLI flags, picks a rendering mode, optionally runs the loader in "load-only" mode, and finally constructs a `RenderWindow` with the desired flags (`Program.cs:1-80`).
- The `RenderWindow` owns OpenGL state, shader setup, camera matrices, and the list of `ISceneObject`s that are updated/drawn each frame (`RenderWindow.cs:9-148`). It chooses between a debug cube and one or more NIF-derived scene objects during `OnLoad` and manages shader uniforms in `OnRenderFrame`.

## Scene/renderer contract
- `ISceneObject` defines the per-frame contract: `Update(float deltaTime)` for advancing animation state plus `Render(Shader shader, Matrix4 view, Matrix4 proj)` for issuing draw calls with the shared shader/camera (`ISceneObject.cs:1-16`).
- `NifModelSceneObject` implements that contract, loading a `Model` via `Civ4NifLoader`, auto-scaling it, and applying a rotating transform every frame before delegating to the shared shader (`NifModelSceneObject.cs:1-66`).
- Each scene object is responsible for disposing GPU resources when the window unloads (`RenderWindow.cs:127-147`).

## Loader / data pipeline
- `Civ4NifLoader.LoadModel` reads a `.nif` file into a `NifContext` and builds a `Model`, optionally baking world transforms and creating GPU-ready meshes (`Civ4NifLoader.cs:14-205`). The loader understands `NiTriShape`/`NiTriStrips` blocks plus their data, traverses the node tree, and attaches materials/textures for each geometry block.
- `NifContext` is the in-memory graph of parsed blocks, header metadata, string palettes, and typed block instances such as `NiTriShapeBlock`, `NiTriShapeDataBlock`, or controller blocks later in `Civ4NifLoader.cs` (`NifContext.cs:1-43`, `Civ4NifLoader.cs:1341-1576`).
- World transforms are computed per node via `BuildWorldTransforms`/`TraverseNode`, which accumulate local translation/rotation/scale before baking them into `Mesh` vertex data when requested (`Civ4NifLoader.cs:96-178`).

## GPU resources & materials
- `Model` shuttles its meshes and per-mesh materials to the renderer; each call to `Draw` applies the material uniforms, binds the optional texture, and renders through `Mesh.Render` (`Model.cs:1-46`, `Mesh.cs:1-122`).
- `Mesh` takes flattened position/normal/UV data, uploads it into a VAO/VBO/EBO, and wires simple attribute pointers so the shader can consume `(position, normal, uv)` tuples (`Mesh.cs:4-122`).
- `Material` keeps the Phong color properties plus an optional `Texture`; its `ApplyToShader` helper writes these values just before each mesh draw, while `Texture.Load` handles DDS or STB-backed textures (`Material.cs:1-39`, `Texture.cs:1-93`).

Putting these layers together: `Program` → `RenderWindow` → `NifModelSceneObject` → `Civ4NifLoader` → `NifContext/Model/Mesh/Material` ensures there is a single stream of NIF data that a future animation component can hook into at either the scene-object level (updating transform uniforms) or the loader level (salvaging bones/weights/controllers).
