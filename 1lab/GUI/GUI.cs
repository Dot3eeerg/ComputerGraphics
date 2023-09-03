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

           ImGui.PushID("Coords");
           for (int i = 0; i < vertices.Length; i += 3)
           {
               System.Numerics.Vector2
                   vert = new System.Numerics.Vector2(vertices[i], vertices[i + 1]);

               ImGui.DragFloat2((i / 3).ToString(), ref vert, 0.005f);
               objects[currentObject].UpdateVerticesCoordinates(i, vert.X, vert.Y);
           }
           ImGui.PopID();
            
           ImGui.End();
        }
        
        //if (ImGui.Begin("Variety"))
        //{
        //    if (ImGui.TreeNode("Objects"))
        //    {
        //        foreach (var obj in objects)
        //        {
        //            ImGui.PushID(objects.IndexOf(obj));
        //            
        //            if (ImGui.TreeNode(objects.IndexOf(obj).ToString()))
        //            {
        //                ImGui.Text("Color");
        //                Vector3 color = obj.GetColor();
        //                System.Numerics.Vector3 kek = new System.Numerics.Vector3(color.X, color.Y, color.Z);

        //                ImGui.ColorEdit3("color", ref kek);
        //                color.X = kek.X;
        //                color.Y = kek.Y;
        //                color.Z = kek.Z;
        //                obj.UpdateColor(color);
        //                
        //                ImGui.Text("Position");
        //                float[] vertices = obj.GetVertices();
        //                ImGui.PushID("Coords");
        //                for (int i = 0; i < vertices.Length; i += 3)
        //                {
        //                    System.Numerics.Vector2
        //                        vert = new System.Numerics.Vector2(vertices[i], vertices[i + 1]);

        //                    ImGui.DragFloat2((i / 3).ToString(), ref vert, 0.005f);
        //                    obj.UpdateVerticesCoordinates(i, vert.X, vert.Y);
        //                }
        //                
        //                ImGui.PopID();

        //                ImGui.TreePop();
        //            }
        //            
        //            ImGui.PopID();
        //        }
        //        
        //        ImGui.TreePop();
        //    }
        //    
        //    ImGui.End();
        //}
    }
}