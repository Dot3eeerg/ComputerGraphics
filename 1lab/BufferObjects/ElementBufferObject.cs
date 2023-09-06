namespace _1lab.BufferObjects;

using OpenTK.Graphics.OpenGL4;

public class ElementBufferObject
{
    private int _handle;

    public ElementBufferObject(uint[] indices)
    {
        _handle = GL.GenBuffer();
        Bind();
        Update(indices);
    }
    
    public void Handle()
    {
        Dispose();
        _handle = GL.GenBuffer();
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _handle);
    }

    public void Update(uint[] indices)
    {
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Length * sizeof(uint), indices.ToArray(),
            BufferUsageHint.StaticDraw);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_handle);
    }
}