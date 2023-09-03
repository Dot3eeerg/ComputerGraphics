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

    private ImGuiNET.ImGuiWindowFlags _windowFlags =
        ImGuiWindowFlags.NoDecoration |
        ImGuiWindowFlags.AlwaysAutoResize |
        ImGuiWindowFlags.NoNav |
        ImGuiWindowFlags.NoSavedSettings |
        ImGuiWindowFlags.NoFocusOnAppearing |
        ImGuiWindowFlags.NoMove;

    public GUI(ImGuiController controller)
    {
        _controller = controller;
    }

    public void DrawGui(List<Object> objects)
    {
        if (ImGui.Begin("Variety"))
        {
            if (ImGui.TreeNode("Objects"))
            {
                foreach (var obj in objects)
                {
                    ImGui.PushID(objects.IndexOf(obj));
                    
                    if (ImGui.TreeNode(objects.IndexOf(obj).ToString()))
                    {
                        ImGui.Text("Color");
                        Vector3 color = obj.GetColor();
                        System.Numerics.Vector3 kek = new System.Numerics.Vector3(color.X, color.Y, color.Z);

                        ImGui.ColorEdit3("color", ref kek);
                        color.X = kek.X;
                        color.Y = kek.Y;
                        color.Z = kek.Z;
                        obj.UpdateColor(color);
                        
                        ImGui.Text("Position");
                        var vertices = obj.GetVertices();
                        for (int i = 0; i < vertices.Length; i += 3)
                        {
                            if (ImGui.TreeNode((i / 3).ToString()))
                            {
                                System.Numerics.Vector2
                                    vert = new System.Numerics.Vector2(vertices[i], vertices[i + 1]);

                                ImGui.DragFloat2("Position", ref vert, 0.005f);
                                obj.UpdateVertices(i, vert[0], vert[1]);
                                
                                ImGui.TreePop();
                            }
                        }
                        
                        ImGui.TreePop();
                    }
                    
                    ImGui.PopID();
                }
                
                ImGui.TreePop();
            }
            
            ImGui.End();
        }
    }
}