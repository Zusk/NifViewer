using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;
using StbImageSharp;

public class Texture
{
    public int Handle { get; private set; }

    private Texture(int handle)
    {
        Handle = handle;
    }

    /// <summary>
    /// Tries each path in order and loads the first one that exists.
    /// Decodes using StbImageSharp into RGBA8 and uploads to OpenGL.
    /// </summary>
    public static Texture Load(params string[] possiblePaths)
    {
        string? pathFound = null;

        foreach (var path in possiblePaths)
        {
            if (!string.IsNullOrWhiteSpace(path) && File.Exists(path))
            {
                pathFound = path;
                break;
            }
        }

        if (pathFound == null)
            throw new FileNotFoundException("Texture file not found.", string.Join(", ", possiblePaths));

        // Flip vertically so textures appear with the expected orientation
        StbImage.stbi_set_flip_vertically_on_load(1);

        ImageResult image;
        using (var stream = File.OpenRead(pathFound))
        {
            image = ImageResult.FromStream(stream, ColorComponents.RedGreenBlueAlpha);
        }

        int handle = GL.GenTexture();
        GL.BindTexture(TextureTarget.Texture2D, handle);

        GL.TexImage2D(TextureTarget.Texture2D,
                      level: 0,
                      internalformat: PixelInternalFormat.Rgba,
                      width: image.Width,
                      height: image.Height,
                      border: 0,
                      format: PixelFormat.Rgba,
                      type: PixelType.UnsignedByte,
                      pixels: image.Data);

        GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

        // Basic filtering and wrap
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
        GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

        return new Texture(handle);
    }

    public void Bind(TextureUnit unit = TextureUnit.Texture0)
    {
        GL.ActiveTexture(unit);
        GL.BindTexture(TextureTarget.Texture2D, Handle);
    }
}
