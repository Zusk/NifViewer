using System;
using System.Collections.Generic;

/// <summary>
/// Factory for block creation based on the block type string.
/// </summary>
public static class NifRegistry
{
    private static readonly Dictionary<string, Func<NiObject>> Registry =
        new(StringComparer.Ordinal)
        {
            ["NiNode"] = () => new NiNode(),
            ["NiTriShape"] = () => new NiTriShape(),
            ["NiTriShapeData"] = () => new NiTriShapeData(),

            ["NiMaterialProperty"] = () => new NiMaterialProperty(),
            ["NiTexturingProperty"] = () => new NiTexturingProperty(),
            ["NiSourceTexture"] = () => new NiSourceTexture(),

            // simple Civ4 stubs
            ["NiCollisionData"] = () => new NiCollisionData(),
            ["NiAlphaProperty"] = () => new NiAlphaProperty(),
            ["NiAlphaController"] = () => new NiAlphaController(),
            ["NiStencilProperty"] = () => new NiStencilProperty(),
            ["NiSkinInstance"] = () => new NiSkinInstance(),
            ["NiSkinData"] = () => new NiSkinData(),
        };

    public static NiObject Create(string typeName, int index)
    {
        var obj = Registry.TryGetValue(typeName, out var ctor)
            ? ctor()
            : new NiUnknown();

        obj.TypeName = typeName;
        obj.BlockIndex = index;
        return obj;
    }
}

