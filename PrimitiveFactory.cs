using OpenTK.Mathematics;

public static class PrimitiveFactory
{
    // Reuses the exact cube layout you’re already using.
    public static Model CreateTestCubeModel()
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

        var mesh = new Mesh(vertices, indices);

        var mat = new Material
        {
            // Simple white material; texture will dominate.
            Diffuse  = new Vector3(1f, 1f, 1f),
            Specular = new Vector3(1f, 1f, 1f),
            Shininess = 32f
        };

        // Try using the same texture you already set up.
        // If missing, you can adjust or create a dummy material later.
        mat.Texture = Texture.Load("Content/texture.dds", "Content/texture.png");

        var model = new Model();
        model.AddMesh(mesh, mat);
        return model;
    }
}
