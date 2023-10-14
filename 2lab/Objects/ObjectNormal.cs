namespace _2lab.Objects;

using _2lab.BufferObjects;
using _2lab.Shaders;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class ObjectNormal
{
    private VertexBufferObject _vbo;
    private VertexArrayObject _vao;
    
    private float[] _vertices;
    
    private Shader _shader;

    private Vector3 _position;
    private float _scale;
    
    
    public ObjectNormal(float[] vertices, Vector3 position, float scale)
    {
        _vertices = vertices;
        _position = position;
        _scale = scale;
        
        _shader = new Shader("Shaders/normalShader.vert", "Shaders/normalShader.frag");
        _shader.Use();

        var vertexLocation = _shader.GetAttribLocation("aPos");
        
        _vbo = new VertexBufferObject(_vertices);
        _vao = new VertexArrayObject(vertexLocation, 3);
        _vao.EnableArray(vertexLocation, 0, 3);
    }
    
    public void Render(Camera camera, Vector3 lightPos, Vector3 position, float angle)
    {
        _vao.Bind();
        
        _shader.Use();
        
        Matrix4 model = Matrix4.CreateTranslation(position);
        model *= Matrix4.CreateFromAxisAngle(new Vector3(1.0f, 0.3f, 0.5f), angle);
        _shader.SetMatrix4("model", model);
        _shader.SetMatrix4("view", camera.GetViewMatrix());
        _shader.SetMatrix4("projection", camera.GetProjectionMatrix());
        
        GL.DrawArrays(PrimitiveType.Lines, 0, 72);
    }

    public void UpdateBuffers()
    {
        _vbo.Update(_vertices);
        _vao.Bind();
    }

    public void Dispose()
    {
        _vao.Dispose();
        _vbo.Dispose();
        
        _vertices = Array.Empty<float>();
        
        GC.SuppressFinalize(this);
    }
}