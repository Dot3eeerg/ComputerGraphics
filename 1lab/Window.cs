using OpenTK.Windowing.GraphicsLibraryFramework;

namespace _1lab;

using _1lab.GUI;
using _1lab.BufferObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImGuiNET;
using System.Drawing;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using System.Diagnostics;

public class Window : GameWindow
{
    ImGuiController _controller;

    private GL _context;

    public Window() : base(GameWindowSettings.Default, new NativeWindowSettings(){ Size = new Vector2i(1600, 900), APIVersion = new Version(3, 3) })
    { }
    
    private List<VertexBufferObject> _vbo;
    private List<VertexArrayObject> _vao;
    private List<ElementBufferObject> _ebo;
    
    private List<VertexBufferObject> _vboPoints;
    private List<VertexArrayObject> _vaoPoints;
    private List<ElementBufferObject> _eboPoints;
    
    private Shader _shader;

    private Layers _layers;

    private bool _firstMove = true;
    private Vector2 _lastPos;

    protected override void OnLoad()
    {
        base.OnLoad();

        _layers = new Layers();
        
        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        
        _controller = new ImGuiController(ClientSize.X, ClientSize.Y);

        _vbo = new List<VertexBufferObject>();
        _vbo.Add(new VertexBufferObject(_layers.GetVertices()));

        _vao = new List<VertexArrayObject>();
        _vao.Add(new VertexArrayObject());

        _ebo = new List<ElementBufferObject>();
        _ebo.Add(new ElementBufferObject(_layers.GetIndices()));
        
        
        _vboPoints = new List<VertexBufferObject>();
        _vboPoints.Add(new VertexBufferObject(_layers.GetVertices()));

        _vaoPoints = new List<VertexArrayObject>();
        _vaoPoints.Add(new VertexArrayObject());

        _eboPoints = new List<ElementBufferObject>();
        _eboPoints.Add(new ElementBufferObject(_layers.GetIndices()));
        

        _shader = new Shader("Shaders/shader.vert", "Shaders/shader.frag");
        _shader.Use();
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        
        GL.PointSize(10.0f);
        GL.LineWidth(2.0f);
        
        _controller.Update(this, (float)e.Time);
        
        GL.Clear(ClearBufferMask.ColorBufferBit);

        _shader.Use();

        int vertexColorLocation = GL.GetUniformLocation(_shader.Handle, "Color");
        
        GL.Uniform4(vertexColorLocation, 1.0f, 0.5f, 0.8f, 1.0f);
        
        _controller.Render();
        ImGuiController.CheckGLError("End of frame");
        
        UpdateBuffers();

        for (int i = 0; i < _vbo.Count; i++)
        {
            GL.DrawElements(PrimitiveType.Points, _layers.GetIndices().Count, DrawElementsType.UnsignedInt, 0);
            GL.DrawElements(PrimitiveType.LineStrip, _layers.GetIndices().Count, DrawElementsType.UnsignedInt, 0);
        }
        
        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        if (!IsFocused)
        {
            return;
        }

        var mouse = MouseState;

        if (mouse.IsButtonPressed(MouseButton.Left))
        {
            var x = (2.0f * mouse.X) / ClientSize.X - 1.0f;
            var y = 1.0f - (2.0f * mouse.Y) / ClientSize.Y;
            _layers.Update(x, y);
            UpdateBuffers();
        }
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        
        GL.Viewport(0, 0, e.Width, e.Height);
        
        _controller.WindowResized(ClientSize.X, ClientSize.Y);
    }

    private void UpdateBuffers()
    {
        for (int i = 0; i < _vbo.Count; i++)
        {
            _vbo[i].Update(_layers.GetVertices());
            _vao[i].Update();
            _ebo[i].Update(_layers.GetIndices());
            
            _vboPoints[i].Update(_layers.GetVertices());
            _vaoPoints[i].Update();
            _eboPoints[i].Update(_layers.GetIndices());
        }
    }
}
