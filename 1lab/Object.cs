namespace _1lab;

using _1lab.BufferObjects;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class Object
{
    private VertexBufferObject _vbo;
    private VertexArrayObject _vao;
    private ElementBufferObject _ebo;
    
    private VertexBufferObject _vboPoints;
    private VertexArrayObject _vaoPoints;
    private ElementBufferObject _eboPoints;
    
    private float[] _vertices;
    private uint[] _indices;
    
    private Shader _shader;
    
    private Vector4 _color;

    public Object(Vector4 color)
    {
        _vertices = new float[0];
        _indices = new uint[0];
        _vbo = new VertexBufferObject(_vertices);
        _vao = new VertexArrayObject();
        _ebo = new ElementBufferObject(_indices);
        
        _vboPoints = new VertexBufferObject(_vertices);
        _vaoPoints = new VertexArrayObject();
        _eboPoints = new ElementBufferObject(_indices);
        
        _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
        _shader.Use();

        _color = color;
    }
    
    public Object()
        {
            _vertices = new float[0];
            _indices = new uint[0];
            _vbo = new VertexBufferObject(_vertices);
            _vao = new VertexArrayObject();
            _ebo = new ElementBufferObject(_indices);
            
            _vboPoints = new VertexBufferObject(_vertices);
            _vaoPoints = new VertexArrayObject();
            _eboPoints = new ElementBufferObject(_indices);
            
            _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
            _shader.Use();

            _color = new Vector4();
        }

    public void Render()
    {
        GL.PointSize(10.0f);
        GL.LineWidth(2.0f);
        
        _shader.Use();

        int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "Color");
        //GL.Uniform4(vertexColorLocation, 1.0f, 0.5f, 0.8f, 1.0f);
        GL.Uniform4(vertexColorLocation, _color);
        
        UpdateBuffers();
        
        GL.DrawElements(PrimitiveType.Points, _indices.Length, DrawElementsType.UnsignedInt, 0); 
        GL.DrawElements(PrimitiveType.LineStrip, _indices.Length, DrawElementsType.UnsignedInt, 0);
    }

    public void UpdateVertices(float x, float y)
    {
        Array.Resize(ref _vertices, _vertices.Length + 3);
        _vertices[^3] = x;
        _vertices[^2] = y;
        _vertices[^1] = 0.0f;
        
        Array.Resize(ref _indices, _indices.Length + 1);
        _indices[^1] = (uint) _indices.Length - 1;
    }

    private void UpdateBuffers()
    {
        _vbo.Update(_vertices);
        _vao.Bind();
        _ebo.Update(_indices);
        
        _vboPoints.Update(_vertices);
        _vaoPoints.Bind();
        _eboPoints.Update(_indices);
    }
}