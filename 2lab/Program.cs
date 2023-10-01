using _2lab;
using OpenTK.Mathematics;
using OpenTK.Windowing.Desktop;

var nativeWindowSettings = new NativeWindowSettings()
{
    Size = new Vector2i(1920, 1080),
};

using (Window window = new Window(GameWindowSettings.Default, nativeWindowSettings))
{
    window.Run();
}