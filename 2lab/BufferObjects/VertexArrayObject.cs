namespace _2lab.BufferObjects;

using OpenTK.Graphics.OpenGL4;

public class VertexArrayObject : IDisposable
{
    private int _handle;
    private int _stride;

    public VertexArrayObject()
    {
        _handle = GL.GenVertexArray();
        Bind();
        //GL.EnableVertexAttribArray(0);
        //Update();
    }
    
    public VertexArrayObject(int vertexLocation, int stride)
    {
        _handle = GL.GenVertexArray();
        Bind();
        _stride = stride;
        //GL.EnableVertexAttribArray(vertexLocation);
        //Update(vertexLocation);
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
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, _stride * sizeof(float), 0);
    }
    
    public void Update(int vertexLocation)
    {
        GL.VertexAttribPointer(vertexLocation, 3, VertexAttribPointerType.Float, false, _stride * sizeof(float), 0);
    }

    public void EnableArray(int location, int offset)
    {
        GL.EnableVertexAttribArray(location);
        GL.VertexAttribPointer(location, 3, VertexAttribPointerType.Float, false, _stride * sizeof(float), offset);
    }
    
    public void EnableArray(int location, int offset, int size)
    {
        GL.EnableVertexAttribArray(location);
        GL.VertexAttribPointer(location, size, VertexAttribPointerType.Float, false, _stride * sizeof(float), offset);
    }

    public void Dispose()
    {
        GL.DeleteVertexArray(_handle);
    }
}