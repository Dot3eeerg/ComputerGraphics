namespace _1lab.BufferObjects;

using OpenTK.Graphics.OpenGL4;

public class VertexArrayObject
{
    private int _handle;

    public VertexArrayObject()
    {
        _handle = GL.GenVertexArray();
        Bind();
        Update();
        GL.EnableVertexAttribArray(0);
    }

    public void Bind()
    {
        GL.BindVertexArray(_handle);
    }

    public void Update()
    {
        GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 3 * sizeof(float), 0);
    }

    public void Dispose()
    {
        GL.DeleteVertexArray(_handle);
    }
}