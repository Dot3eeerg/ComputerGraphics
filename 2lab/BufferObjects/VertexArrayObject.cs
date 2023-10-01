namespace _2lab.BufferObjects;

using OpenTK.Graphics.OpenGL4;

public class VertexArrayObject : IDisposable
{
    private int _handle;

    public VertexArrayObject()
    {
        _handle = GL.GenVertexArray();
        Bind();
        GL.EnableVertexAttribArray(0);
        Update();
    }
    
    public VertexArrayObject(int vertexLocation)
    {
        _handle = GL.GenVertexArray();
        Bind();
        GL.EnableVertexAttribArray(vertexLocation);
        Update(vertexLocation);
    }

    public void Handle()
    {
        Dispose();
        _handle = GL.GenVertexArray();
    }

    public void Bind()
    {
        GL.BindVertexArray(_handle);
    }

    public void Update()
    {
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
    }
    
    public void Update(int vertexLocation)
    {
        GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, 6 * sizeof(float), 0);
    }

    public void Dispose()
    {
        GL.DeleteVertexArray(_handle);
    }
}