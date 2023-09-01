namespace _1lab;

using OpenTK.Graphics.OpenGL4;

public class ElementBufferObject
{
    private int _handle;

    public ElementBufferObject(List<uint> indices)
    {
        _handle = GL.GenBuffer();
        Bind();
        Update(indices);
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ElementArrayBuffer, _handle);
    }

    public void Update(List<uint> indices)
    {
        GL.BufferData(BufferTarget.ElementArrayBuffer, indices.Count * sizeof(uint), indices.ToArray(),
            BufferUsageHint.DynamicDraw);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_handle);
    }
}