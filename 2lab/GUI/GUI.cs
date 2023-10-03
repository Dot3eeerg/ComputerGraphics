namespace _2lab.GUI;

using ImGuiNET;
using OpenTK.Mathematics;

public class GUI
{
    private ImGuiController _controller;

    private Window _window;
    
    private ImGuiWindowFlags _windowFlags =
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
        
        ImGui.PushStyleColor(ImGuiCol.Text, new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 1.0f));
        ImGui.PushStyleColor(ImGuiCol.FrameBg, new System.Numerics.Vector4(0.1f, 0.1f, 0.1f, 0.8f));
        ImGui.PushStyleColor(ImGuiCol.ChildBg, new System.Numerics.Vector4(0.0f, 0.0f, 0.0f, 1.0f));
        ImGui.PushStyleColor(ImGuiCol.WindowBg, new System.Numerics.Vector4(0.0f, 0.0f, 0.0f, 0.6f));
        ImGui.PushStyleColor(ImGuiCol.CheckMark, new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 0.6f)); 
    }

    public void Draw()
    {
        ImGui.SetNextWindowBgAlpha(1.0f);
        ImGui.SetNextWindowPos(new System.Numerics.Vector2(1400.0f, 40.0f));
        if (ImGui.Begin("View model"))
        {
            ImGui.Text("kek");
            if (ImGui.Button("Material model"))
            {
                _window.ChangeToMaterialModel();
            }
            
            if (ImGui.Button("Texture model"))
            {
                _window.ChangeToTextureObject();
            }
            
            if (ImGui.Button("Frame model"))
            {
                _window.ChangeToFrameObject();
            }
            
            ImGui.End();
        }
    }
}