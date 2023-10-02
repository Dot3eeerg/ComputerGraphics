namespace _2lab;

using Shaders;
using BufferObjects;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class Object
{
    private VertexBufferObject _vbo;
    private VertexArrayObject _vao;
    
    private float[] _vertices;
    
    private Shader _shader;
    
    public Object(float[] vertices)
    {
        _vertices = vertices;
        
        _shader = new Shader("Shaders/lightShader.vert", "Shaders/lightShader.frag");
        _shader.Use();

        var vertexLocation = _shader.GetAttribLocation("aPos");
        
        _vbo = new VertexBufferObject(_vertices);
        _vao = new VertexArrayObject(vertexLocation);
        _vao.EnableArray(vertexLocation, 0);
        
        var normalLocation = _shader.GetAttribLocation("aNormal");

        _vao.EnableArray(normalLocation, 3 * sizeof(float));
    }
    
    public void Render(Camera camera, Vector3 lightPos)
    {
        _vao.Bind();
        
        _shader.Use();
        
        _shader.SetMatrix4("model", Matrix4.Identity);
        _shader.SetMatrix4("view", camera.GetViewMatrix());
        _shader.SetMatrix4("projection", camera.GetProjectionMatrix());
        
        _shader.SetVector3("viewPos", camera.Position);
        
        _shader.SetVector3("material.ambient", new Vector3(1.0f, 0.5f, 0.31f));
        _shader.SetVector3("material.diffuse", new Vector3(1.0f, 0.5f, 0.31f));
        _shader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
        _shader.SetFloat("material.shininess", 32.0f);
        
        _shader.SetVector3("light.position", lightPos);
        _shader.SetVector3("light.ambient",  new Vector3(0.2f, 0.2f, 0.2f));
        _shader.SetVector3("light.diffuse",  new Vector3(0.7f, 0.7f, 0.7f));
        _shader.SetVector3("light.specular", new Vector3(1.0f, 1.0f, 1.0f));
        
        GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
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