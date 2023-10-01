namespace _2lab;

using Shaders;
using BufferObjects;

using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;
using System.Diagnostics;
using OpenTK.Graphics.OpenGL4;

public class Lamp
{
    
    private Shader _shader;

    private VertexArrayObject _vao;
    private VertexBufferObject _vbo;

    private Vector3 _lightLocation;
    private float[] _vertices;

    public Lamp(Vector3 lightLocation, float[] vertices)
    {
        _lightLocation = lightLocation;
        _vertices = vertices;
        
        _shader = new Shader("Shaders/lampShader.vert", "Shaders/lampShader.frag");
        _shader.Use();

        var vertexLocation = _shader.GetAttribLocation("aPos");
        
        _vbo = new VertexBufferObject(_vertices);
        _vao = new VertexArrayObject(vertexLocation);
    }

    public void Render(Camera camera)
    {
        _vao.Bind();
        
        _shader.Use();

        Matrix4 lampMatrix = Matrix4.CreateScale(0.2f);
        lampMatrix = lampMatrix * Matrix4.CreateTranslation(_lightLocation);
        
        _shader.SetMatrix4("model", lampMatrix);
        _shader.SetMatrix4("view", camera.GetViewMatrix());
        _shader.SetMatrix4("projection", camera.GetProjectionMatrix());
        
        GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
    }
    
    public void UpdateBuffers()
    {
        _vbo.Update(_vertices);
        _vao.Bind();
    }
}