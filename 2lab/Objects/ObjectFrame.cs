﻿using _2lab.Objects;

namespace _2lab;

using Shaders;
using BufferObjects;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class ObjectFrame : IObject
{
    private VertexBufferObject _vbo;
    private VertexArrayObject _vao;
    
    private float[] _vertices;
    
    private Shader _shader;
    
    public ObjectFrame(float[] vertices)
    {
        _vertices = vertices;
        
        _shader = new Shader("Shaders/frameShader.vert", "Shaders/frameShader.frag");
        _shader.Use();

        var vertexLocation = _shader.GetAttribLocation("aPos");
        
        _vbo = new VertexBufferObject(_vertices);
        _vao = new VertexArrayObject(vertexLocation, 6);
        _vao.EnableArray(vertexLocation, 0);
    }
    
    public void Render(Camera camera, Vector3 lightPos)
    {
        _vao.Bind();
        
        _shader.Use();
        
        _shader.SetMatrix4("model", Matrix4.Identity);
        _shader.SetMatrix4("view", camera.GetViewMatrix());
        _shader.SetMatrix4("projection", camera.GetProjectionMatrix());
        
        GL.DrawArrays(PrimitiveType.Lines, 0, 48);
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