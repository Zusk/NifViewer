using System.IO;
using OpenTK.Mathematics;

/// <summary>
/// Standard Civ4 material: ambient, diffuse, specular, emissive, shininess, alpha.
/// </summary>
public sealed class NiMaterialProperty : NiObject
{
    public Vector3 Ambient, Diffuse, Specular, Emissive;
    public float Shininess;
    public float Alpha;

    public override void Read(BinaryReader br, NifContext ctx)
    {
        base.Read(br, ctx);

        Ambient  = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        Diffuse  = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        Specular = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        Emissive = new Vector3(br.ReadSingle(), br.ReadSingle(), br.ReadSingle());
        Shininess = br.ReadSingle();
        Alpha     = br.ReadSingle();
    }
}

