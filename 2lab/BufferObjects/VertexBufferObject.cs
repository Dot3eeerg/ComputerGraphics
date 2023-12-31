﻿namespace _2lab.BufferObjects;

using OpenTK.Graphics.OpenGL4;

public class VertexBufferObject : IDisposable
{
    private int _handle;
    
    public VertexBufferObject(float[] vertices)
    {
        _handle = GL.GenBuffer();
        Bind();
        Update(vertices);
    }
    
    public void Handle()
    {
        Dispose();
        _handle = GL.GenBuffer();
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
    }

    public void Update(float[] vertices)
    {
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Length * sizeof(float), vertices,
            BufferUsageHint.StaticDraw);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_handle);
    }
}