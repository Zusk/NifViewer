using System;

// Backwards-compatible thin wrapper so code referring to the old
// `CubeWindow` type continues to work while the new `RenderWindow`
// is the canonical implementation.
class CubeWindow : RenderWindow
{
    public CubeWindow(OpenTK.Windowing.Desktop.GameWindowSettings gws, OpenTK.Windowing.Desktop.NativeWindowSettings nws)
        : base(gws, nws)
    {
    }

    public CubeWindow(OpenTK.Windowing.Desktop.GameWindowSettings gws, OpenTK.Windowing.Desktop.NativeWindowSettings nws, bool forceCube, bool forceModel)
        : base(gws, nws, forceCube, forceModel, true)
    {
    }
}
