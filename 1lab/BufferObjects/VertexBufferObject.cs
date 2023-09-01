namespace _1lab.BufferObjects;

using OpenTK.Graphics.OpenGL4;

public class VertexBufferObject : IDisposable
{
    private int _handle;
    
    public VertexBufferObject(List<float> vertices)
    {
        _handle = GL.GenBuffer();
        Bind();
        Update(vertices);
    }

    public void Bind()
    {
        GL.BindBuffer(BufferTarget.ArrayBuffer, _handle);
    }

    public void Update(List<float> vertices)
    {
        GL.BufferData(BufferTarget.ArrayBuffer, vertices.Count * sizeof(float), vertices.ToArray(),
            BufferUsageHint.DynamicDraw);
    }

    public void Dispose()
    {
        GL.DeleteBuffer(_handle);
    }
}