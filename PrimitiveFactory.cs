using OpenTK.Mathematics;

public static class PrimitiveFactory
{
    // Factory for a simple cube model used for testing/debugging.
    public static Model CreateTestCubeModel()
    {
        var (vertices, indices) = BuildCubeGeometry();
        var mesh = new Mesh(vertices, indices);

        var mat = new Material
        {
            Diffuse  = new Vector3(1f, 1f, 1f),
            Specular = new Vector3(1f, 1f, 1f),
            Shininess = 32f
        };

        mat.Texture = Texture.Load("Content/texture.dds", "Content/texture.png");

        var model = new Model();
        model.AddMesh(mesh, mat);
        return model;
    }

    public static Model CreateMarkerCubeModel()
    {
        var (vertices, indices) = BuildCubeGeometry();
        var mesh = new Mesh(vertices, indices);

        var mat = new Material
        {
            Diffuse = new Vector3(1f, 0.2f, 0.2f),
            Specular = new Vector3(0.3f, 0.3f, 0.3f),
            Shininess = 8f
        };

        var model = new Model();
        model.AddMesh(mesh, mat);
        return model;
    }

    private static (float[] vertices, uint[] indices) BuildCubeGeometry()
    {
        float[] vertices =
        {
            // FRONT (+Z)
            -0.5f,-0.5f, 0.5f,   0f,0f,1f,   0f,0f,
             0.5f,-0.5f, 0.5f,   0f,0f,1f,   1f,0f,
             0.5f, 0.5f, 0.5f,   0f,0f,1f,   1f,1f,
            -0.5f, 0.5f, 0.5f,   0f,0f,1f,   0f,1f,

            // BACK (-Z)
            -0.5f,-0.5f,-0.5f,   0f,0f,-1f,  1f,0f,
             0.5f,-0.5f,-0.5f,   0f,0f,-1f,  0f,0f,
             0.5f, 0.5f,-0.5f,   0f,0f,-1f,  0f,1f,
            -0.5f, 0.5f,-0.5f,   0f,0f,-1f,  1f,1f,

            // LEFT (-X)
            -0.5f,-0.5f,-0.5f,  -1f,0f,0f,   0f,0f,
            -0.5f,-0.5f, 0.5f,  -1f,0f,0f,   1f,0f,
            -0.5f, 0.5f, 0.5f,  -1f,0f,0f,   1f,1f,
            -0.5f, 0.5f,-0.5f,  -1f,0f,0f,   0f,1f,

            // RIGHT (+X)
             0.5f,-0.5f,-0.5f,   1f,0f,0f,   1f,0f,
             0.5f,-0.5f, 0.5f,   1f,0f,0f,   0f,0f,
             0.5f, 0.5f, 0.5f,   1f,0f,0f,   0f,1f,
             0.5f, 0.5f,-0.5f,   1f,0f,0f,   1f,1f,

            // BOTTOM (-Y)
            -0.5f,-0.5f,-0.5f,   0f,-1f,0f,  0f,1f,
             0.5f,-0.5f,-0.5f,   0f,-1f,0f,  1f,1f,
             0.5f,-0.5f, 0.5f,   0f,-1f,0f,  1f,0f,
            -0.5f,-0.5f, 0.5f,   0f,-1f,0f,  0f,0f,

            // TOP (+Y)
            -0.5f, 0.5f,-0.5f,   0f,1f,0f,   0f,0f,
             0.5f, 0.5f,-0.5f,   0f,1f,0f,   1f,0f,
             0.5f, 0.5f, 0.5f,   0f,1f,0f,   1f,1f,
            -0.5f, 0.5f, 0.5f,   0f,1f,0f,   0f,1f,
        };

        uint[] indices =
        {
            0,1,2,  2,3,0,
            4,5,6,  6,7,4,
            8,9,10, 10,11,8,
            12,13,14,14,15,12,
            16,17,18,18,19,16,
            20,21,22,22,23,20
        };

        return (vertices, indices);
    }
}
