using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using System;

public class Shader
{
    public int Handle { get; private set; }

    public Shader(string vertexSource, string fragmentSource)
    {
        int vShader = GL.CreateShader(ShaderType.VertexShader);
        GL.ShaderSource(vShader, vertexSource);
        GL.CompileShader(vShader);
        GL.GetShader(vShader, ShaderParameter.CompileStatus, out int vStatus);
        if (vStatus == 0)
            throw new Exception($"Vertex shader error:\n{GL.GetShaderInfoLog(vShader)}");

        int fShader = GL.CreateShader(ShaderType.FragmentShader);
        GL.ShaderSource(fShader, fragmentSource);
        GL.CompileShader(fShader);
        GL.GetShader(fShader, ShaderParameter.CompileStatus, out int fStatus);
        if (fStatus == 0)
            throw new Exception($"Fragment shader error:\n{GL.GetShaderInfoLog(fShader)}");

        Handle = GL.CreateProgram();
        GL.AttachShader(Handle, vShader);
        GL.AttachShader(Handle, fShader);
        GL.LinkProgram(Handle);
        GL.GetProgram(Handle, GetProgramParameterName.LinkStatus, out int linkStatus);
        if (linkStatus == 0)
            throw new Exception($"Shader link error:\n{GL.GetProgramInfoLog(Handle)}");

        GL.DeleteShader(vShader);
        GL.DeleteShader(fShader);
    }

    public void Use()
    {
        GL.UseProgram(Handle);
    }

    public void SetMatrix4(string name, ref Matrix4 matrix)
    {
        GL.UniformMatrix4(GL.GetUniformLocation(Handle, name), false, ref matrix);
    }

    public void SetFloat(string name, float value)
    {
        GL.Uniform1(GL.GetUniformLocation(Handle, name), value);
    }

    public void SetVector3(string name, Vector3 value)
    {
        GL.Uniform3(GL.GetUniformLocation(Handle, name), value);
    }

    public void SetInt(string name, int value)
    {
        GL.Uniform1(GL.GetUniformLocation(Handle, name), value);
    }
}
