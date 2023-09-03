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
using OpenTK.Windowing.GraphicsLibraryFramework;

public class Window : GameWindow
{
    ImGuiController _controller;

    private GL _context;

    public Window() : base(GameWindowSettings.Default, new NativeWindowSettings(){ Size = new Vector2i(1600, 900), APIVersion = new Version(3, 3) })
    { }
    
    private List<Object> _objects;
    
    private bool _firstMove = true;
    private Vector2 _lastPos;

    private int _currentObject;
    
    private bool _canEdit = true;

    private GUI.GUI _gui;

    protected override void OnLoad()
    {
        base.OnLoad();

        GL.ClearColor(0.2f, 0.3f, 0.3f, 1.0f);
        
        _controller = new ImGuiController(ClientSize.X, ClientSize.Y);

        _objects = new List<Object>();
        _objects.Add(new Object());
        _currentObject = 0;
        _gui = new GUI.GUI(_controller);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);

        _controller.Update(this, (float)e.Time);
        
        GL.Clear(ClearBufferMask.ColorBufferBit);
        
        //ImGui.ShowDemoWindow();

        foreach (var kek in _objects)
        {
            kek.Render();
        }
        
        _gui.DrawGui(_objects);
        
        _controller.Render();
        
        ImGuiController.CheckGLError("End of frame");
        
        SwapBuffers();
    }

    protected override void OnUpdateFrame(FrameEventArgs e)
    {
        base.OnUpdateFrame(e);

        if (!IsFocused)
        {
            return;
        }

        var input = KeyboardState;

        if (input.IsKeyDown(Keys.E))
        {
            _canEdit = true;
        }

        if (input.IsKeyDown(Keys.R))
        {
            _canEdit = false;
        }

        var mouse = MouseState;

        // create line strip
        if (mouse.IsButtonPressed(MouseButton.Left))
        {
            if (_canEdit)
            {
                var x = (2.0f * mouse.X) / ClientSize.X - 1.0f;
                var y = 1.0f - (2.0f * mouse.Y) / ClientSize.Y;
                _objects[_currentObject].UpdateVertices(x, y);
            }
        }

        // create new layer
        if (mouse.IsButtonPressed(MouseButton.Right))
        {
            if (_canEdit)
            {
                _currentObject++;
                _objects.Add(new Object());
            }
        }
    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        
        GL.Viewport(0, 0, e.Width, e.Height);
        
        _controller.WindowResized(ClientSize.X, ClientSize.Y);
    }

    protected void OnDrawGUI()
    {
        
    }

    public void ChangeObject(int id)
    {
        _currentObject = id;
    }
}
