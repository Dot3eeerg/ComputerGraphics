﻿namespace _2lab;

using Shaders;
using BufferObjects;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class Object
{
    private VertexBufferObject _vbo;
    private VertexArrayObject _vao;
    
    private VertexBufferObject _vboPoints;
    private VertexArrayObject _vaoPoints;
    
    private float[] _vertices;
    private uint[] _indices;
    
    private Shader _shader;
    
    private Vector4 _color;

    public Object(Vector4 color)
    {
        _vertices = Array.Empty<float>();
        _indices = Array.Empty<uint>();
        _vbo = new VertexBufferObject(_vertices);
        _vao = new VertexArrayObject();
        
        _vboPoints = new VertexBufferObject(_vertices);
        _vaoPoints = new VertexArrayObject();
        
        _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
        _shader.Use();

        _color = color;
    }
    
    public Object()
    {
        _vertices = Array.Empty<float>();
        _indices = Array.Empty<uint>();
        _vbo = new VertexBufferObject(_vertices);
        _vao = new VertexArrayObject();
        
        _vboPoints = new VertexBufferObject(_vertices);
        _vaoPoints = new VertexArrayObject();
        
        _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
        _shader.Use();

        _color = new Vector4();
    }

    public void Render(float pointSize)
    {
        GL.PointSize(pointSize);
        GL.LineWidth(2.0f);
        
        _shader.Use();

        int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "Color");
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

    public void OnChangeObject()
    {
        _vao.Bind();
        
        _vaoPoints.Bind();
        
        _vbo.Update(_vertices);
        _vao.Update();
        
        _vboPoints.Update(_vertices);
        _vaoPoints.Update();
    }

    public void UpdateBuffers()
    {
        _vbo.Update(_vertices);
        _vao.Bind();
        
        _vboPoints.Update(_vertices);
        _vaoPoints.Bind();
    }

    public void UpdateColor(Vector3 color)
    {
        _color = new Vector4(color, 1.0f);
    }

    public void UpdateVerticesCoordinates(int i, float x, float y)
    {
        _vertices[i] = x;
        _vertices[i + 1] = y;
    }

    public float[] GetVertices()
        => _vertices;

    public Vector3 GetColor()
        => _color.Xyz;

    public void Dispose()
    {
        _vao.Dispose();
        _vbo.Dispose();
        
        _vaoPoints.Dispose();
        _vboPoints.Dispose();

        _vertices = Array.Empty<float>();
        _indices = Array.Empty<uint>();
        
        GC.SuppressFinalize(this);
    }
}