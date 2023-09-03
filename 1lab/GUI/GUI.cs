using System.Reflection;

namespace _1lab.GUI;

using ImGuiNET;
using OpenTK.Mathematics;

public class GUI
{
    private ImGuiController _controller;
    private Vector3 _color;
    private Vector2 _position;
    private int _currentObject;
    private Window _window;

    private ImGuiNET.ImGuiWindowFlags _windowFlags =
        ImGuiWindowFlags.NoDecoration |
        ImGuiWindowFlags.AlwaysAutoResize |
        ImGuiWindowFlags.NoNav |
        ImGuiWindowFlags.NoSavedSettings |
        ImGuiWindowFlags.NoFocusOnAppearing |
        ImGuiWindowFlags.NoMove;

    public GUI(ImGuiController controller, Window window)
    {
        _controller = controller;
        _window = window;
    }

    public void DrawGui(List<Object> objects, int currentObject)
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("Help"))
            {
                if (ImGui.MenuItem("Keybindings", "F1"))
                {
                    if (ImGui.TreeNode("Mouse buttons"))
                    {
                        ImGui.Text("Left mouse button - create primitive in current object");
                        ImGui.Text("Right mouse button - create new object");
                        
                        ImGui.TreePop();
                    }

                    if (ImGui.TreeNode("Keyboard buttons"))
                    {
                        ImGui.Text("E - change to edit mode");
                        ImGui.Text("R - change to view mode (set by default)");
                    }
                }
                    
                
                ImGui.EndMenu();
            }
            
            ImGui.EndMainMenuBar();
        }
        
        ImGui.SetNextWindowBgAlpha(0.5f);
        
        if (ImGui.Begin("Kek"))
        {
            if (ImGui.BeginListBox("Objects"))
            {
                for (int i = 0; i < objects.Count; i++)
                {
                    bool isSelected = (currentObject == i);

                    if (ImGui.Selectable(i.ToString(), isSelected))
                    {
                        currentObject = i;
                        _window.ChangeObject(currentObject);
                    }
                }
                
                ImGui.EndListBox();
            }
            
            ImGui.End();
        }

        
        ImGui.SetNextWindowBgAlpha(0.5f);
        if (ImGui.Begin("Object properties"))
        {
            
           ImGui.Text("Color");
           Vector3 color = objects[currentObject].GetColor();
           System.Numerics.Vector3 kek = new System.Numerics.Vector3(color.X, color.Y, color.Z);

           ImGui.ColorEdit3("color", ref kek);
           color.X = kek.X;
           color.Y = kek.Y;
           color.Z = kek.Z;
           objects[currentObject].UpdateColor(color);

           ImGui.Text("Position");
           float[] vertices = objects[currentObject].GetVertices();

           for (int i = 0; i < vertices.Length; i += 3)
           {
               System.Numerics.Vector2
                   vert = new System.Numerics.Vector2(vertices[i], vertices[i + 1]);

               ImGui.DragFloat2((i / 3).ToString(), ref vert, 0.005f);
               objects[currentObject].UpdateVerticesCoordinates(i, vert.X, vert.Y);
           }

           bool deleteObject = ImGui.Button("Delete object");
           if (deleteObject)
           {
               _window.DeleteObject(currentObject);
           }
            
           ImGui.End();
        }
    }
}