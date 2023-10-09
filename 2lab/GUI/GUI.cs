namespace _2lab.GUI;

using ImGuiNET;
using OpenTK.Mathematics;

public class GUI
{
    private ImGuiController _controller;

    private Window _window;

    private readonly string[] _modelType;
    private readonly string[] _modeName;

    private int _selectedModelType;
    private bool _isClicked = false;

    private int _appModes;
    
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

        _modelType = new string[3]
        {
            "Material model",
            "Texture model",
            "Frame model"
        };

        _modeName = new string[2]
        {
            "Movement mode",
            "Cursor mode"
        };
        
        ImGui.PushStyleColor(ImGuiCol.Text, new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 1.0f));
        ImGui.PushStyleColor(ImGuiCol.FrameBg, new System.Numerics.Vector4(0.1f, 0.1f, 0.1f, 0.8f));
        ImGui.PushStyleColor(ImGuiCol.ChildBg, new System.Numerics.Vector4(0.0f, 0.0f, 0.0f, 1.0f));
        ImGui.PushStyleColor(ImGuiCol.WindowBg, new System.Numerics.Vector4(0.0f, 0.0f, 0.0f, 0.6f));
        ImGui.PushStyleColor(ImGuiCol.CheckMark, new System.Numerics.Vector4(1.0f, 1.0f, 1.0f, 0.6f)); 
    }

    public void Draw()
    {
        ImGui.SetNextWindowBgAlpha(1.0f);
        ImGui.SetNextWindowPos(new System.Numerics.Vector2(1700.0f, 40.0f));
        if (ImGui.Begin("View model", _windowFlags))
        {
            bool selectionChanged = ImGui.Combo("", ref _selectedModelType, _modelType, _modelType.Length);

            if (selectionChanged)
            {
                switch (_selectedModelType)
                {
                    case 0:
                        _window.ChangeToMaterialModel();
                        break;
                        
                    case 1:
                        _window.ChangeToTextureObject();
                        break;
                        
                    case 2:
                        _window.ChangeToFrameObject();
                        break;
                }
            }
            
            ImGui.End();
        }

        if (_selectedModelType != 2)
        {
            ImGui.SetNextWindowBgAlpha(1.0f);
            ImGui.SetNextWindowPos(new System.Numerics.Vector2(1700.0f, 80.0f));
            if (ImGui.Begin("Flashlight", _windowFlags))
            {
                ImGui.Checkbox("On/Off", ref _isClicked);
                if (_isClicked)
                {
                    _window.TurnOnFlashlight();
                }

                else
                {
                    _window.TurnOffFlashlight();
                }
            }
        }
        
        ImGui.SetNextWindowBgAlpha(1.0f);
        ImGui.SetNextWindowPos(new System.Numerics.Vector2(960.0f, 40.0f));
        ImGui.Begin("Text", _windowFlags);
        {
            ImGui.Text(_modeName[_window.CurrentAppMode]);
            ImGui.End();
        }
    }
}