using OpenTK.Mathematics;

public class Material
{
    public Vector3 Ambient { get; set; }
    public Vector3 Diffuse { get; set; }
    public Vector3 Specular { get; set; }
    public Vector3 Emissive { get; set; }

    public float Shininess { get; set; }
    public float Alpha { get; set; }

    public Texture? Texture { get; set; }

    public Material()
    {
        // Gamebryo / Civ4 defaults
        Ambient  = new Vector3(0.2f);
        Diffuse  = new Vector3(0.8f);
        Specular = new Vector3(1f);
        Emissive = new Vector3(0f);

        Shininess = 32f; // common specular exponent
        Alpha = 1f;
    }

    /// <summary>
    /// Sends the material colors & shininess to the shader.
    /// The shader must already be bound.
    /// </summary>
    public void ApplyToShader(Shader s)
    {
        s.SetVector3("uAmbientColor",  Ambient);
        s.SetVector3("uDiffuseColor",  Diffuse);
        s.SetVector3("uSpecularColor", Specular);
        s.SetFloat("uShininess", Shininess);
        // Emissive can be added to final color later if desired
    }
}
