namespace _2lab;

using Objects;
using GUI;

using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

public class Window : GameWindow
{
    private readonly float[] _normals =
    {
        -0.5f, -0.5f, -0.5f, -0.5f, -0.5f, -1.0f, // Front face
         0.5f, -0.5f, -0.5f, 0.5f, -0.5f, -1.0f,
         0.5f,  0.5f, -0.5f, 0.5f,  0.5f, -1.0f,
        -0.5f,  0.5f, -0.5f, -0.5f,  0.5f, -1.0f,
        
        -0.5f, -0.5f,  0.5f, -0.5f,  -0.5f,  1.0f, // Back face
         0.5f, -0.5f,  0.5f, 0.5f,  -0.5f,  1.0f,
         0.5f,  0.5f,  0.5f, 0.5f,  0.5f,  1.0f,
        -0.5f,  0.5f,  0.5f, -0.5f,  0.5f,  1.0f,
        
        -0.5f,  0.5f,  0.5f, -1.0f,  0.5f,  0.5f, // Left face
        -0.5f,  0.5f, -0.5f, -1.0f,  0.5f,  -0.5f,
        -0.5f, -0.5f, -0.5f, -1.0f,  -0.5f,  -0.5f,
        -0.5f, -0.5f,  0.5f, -1.0f, -0.5f,  0.5f,
        
         0.5f,  0.5f,  0.5f,  1.0f, 0.5f,  0.5f,  // Right face
         0.5f,  0.5f, -0.5f,  1.0f, 0.5f, -0.5f, 
         0.5f, -0.5f, -0.5f,  1.0f, -0.5f, -0.5f, 
         0.5f, -0.5f,  0.5f,  1.0f, -0.5f,  0.5f, 

        -0.5f, -0.5f, -0.5f, -0.5f, -1.0f, -0.5f, // Bottom face
         0.5f, -0.5f, -0.5f,  0.5f, -1.0f, -0.5f,
         0.5f, -0.5f,  0.5f,  0.5f, -1.0f,  0.5f,
        -0.5f, -0.5f,  0.5f, -0.5f, -1.0f,  0.5f,
        
        -0.5f,  0.5f, -0.5f, -0.5f,  1.0f, -0.5f, // Top face
         0.5f,  0.5f, -0.5f,  0.5f,  1.0f, -0.5f,
         0.5f,  0.5f,  0.5f,  0.5f,  1.0f,  0.5f,
        -0.5f,  0.5f,  0.5f, -0.5f,  1.0f,  0.5f,
    };
    
    private readonly float[] _vertices =
    {
         // Position          Normal
        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f, // Front face
         0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
         
         0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,

        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, // Back face
         0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
         
         0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,

        -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f, // Left face
        -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
        
        -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,

         0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f, // Right face
         0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
         
         0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,

        -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f, // Bottom face
         0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
         
         0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,

        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f, // Top face
         0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
         
         0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f
    };
    
    private readonly float[] _verticesTexture =
    {
        // Positions          Normals              Texture coords
        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,
         0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 0.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  1.0f, 1.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,  0.0f, 0.0f,

        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,
         0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  1.0f, 1.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,  0.0f, 0.0f,

        -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
        -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
        -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
        -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
        -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

         0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,
         0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 1.0f,
         0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  0.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,  1.0f, 0.0f,

        -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 1.0f,
         0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
         0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  1.0f, 0.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 0.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,  0.0f, 1.0f,

        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 1.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  1.0f, 0.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,  0.0f, 1.0f
    };
    
    private readonly float[] _verticesFrame = 
    {
         // Position          Normal
        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f, // Front face
         0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
         0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  0.0f, -1.0f,
        -0.5f, -0.5f, -0.5f,  0.0f,  0.0f, -1.0f,

        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f, // Back face
         0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
         0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  0.0f,  1.0f,
        -0.5f, -0.5f,  0.5f,  0.0f,  0.0f,  1.0f,

        -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f, // Left face
        -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f,  0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f, -0.5f, -0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f, -0.5f,  0.5f, -1.0f,  0.0f,  0.0f,
        -0.5f,  0.5f,  0.5f, -1.0f,  0.0f,  0.0f,

         0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f, // Right face
         0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
         0.5f,  0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  0.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  1.0f,  0.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  0.0f,  0.0f,

        -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f, // Bottom face
         0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
         0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
         0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
        -0.5f, -0.5f,  0.5f,  0.0f, -1.0f,  0.0f,
        -0.5f, -0.5f, -0.5f,  0.0f, -1.0f,  0.0f,

        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f, // Top face
         0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
         0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
         0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
        -0.5f,  0.5f,  0.5f,  0.0f,  1.0f,  0.0f,
        -0.5f,  0.5f, -0.5f,  0.0f,  1.0f,  0.0f
    };
    
    private readonly float[] _normalsSmoothed =
    {
        -0.5f, -0.5f, -0.5f, -1.0f, -1.0f, -1.0f,
         0.5f, -0.5f, -0.5f, 1.0f, -1.0f, -1.0f,
        -0.5f,  0.5f, -0.5f, -1.0f, 1.0f, -1.0f,
         0.5f,  0.5f, -0.5f, 1.0f,  1.0f, -1.0f,
         
        -0.5f, -0.5f, 0.5f, -1.0f, -1.0f, 1.0f,
         0.5f, -0.5f, 0.5f, 1.0f, -1.0f, 1.0f,
        -0.5f,  0.5f, 0.5f, -1.0f, 1.0f, 1.0f,
         0.5f,  0.5f, 0.5f, 1.0f,  1.0f, 1.0f,
    };
    
    
    private readonly float[] _verticesSmoothed =
    {
         // Position          Normal
        -0.5f, -0.5f, -0.5f,  -1.0f,  -1.0f, -1.0f, // Front face
         0.5f, -0.5f, -0.5f,  1.0f,  -1.0f, -1.0f,
         0.5f,  0.5f, -0.5f,  1.0f,  1.0f, -1.0f,
         
         0.5f,  0.5f, -0.5f,  1.0f,  1.0f, -1.0f,
        -0.5f,  0.5f, -0.5f,  -1.0f,  1.0f, -1.0f,
        -0.5f, -0.5f, -0.5f,  -1.0f,  -1.0f, -1.0f,

        -0.5f, -0.5f,  0.5f,  -1.0f,  -1.0f,  1.0f, // Back face
         0.5f, -0.5f,  0.5f,  1.0f,  -1.0f,  1.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,  1.0f,
         
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,  1.0f,
        -0.5f,  0.5f,  0.5f,  -1.0f,  1.0f,  1.0f,
        -0.5f, -0.5f,  0.5f,  -1.0f,  -1.0f,  1.0f,

        -0.5f,  0.5f,  0.5f, -1.0f,  1.0f,  1.0f, // Left face
        -0.5f,  0.5f, -0.5f, -1.0f,  1.0f,  -1.0f,
        -0.5f, -0.5f, -0.5f, -1.0f,  -1.0f,  -1.0f,
        
        -0.5f, -0.5f, -0.5f, -1.0f,  -1.0f,  -1.0f,
        -0.5f, -0.5f,  0.5f, -1.0f,  -1.0f,  1.0f,
        -0.5f,  0.5f,  0.5f, -1.0f,  1.0f,  1.0f,

         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,  1.0f, // Right face
         0.5f,  0.5f, -0.5f,  1.0f,  1.0f,  -1.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  -1.0f,  -1.0f,
         
         0.5f, -0.5f, -0.5f,  1.0f,  -1.0f,  -1.0f,
         0.5f, -0.5f,  0.5f,  1.0f,  -1.0f,  1.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,  1.0f,

        -0.5f, -0.5f, -0.5f,  -1.0f, -1.0f,  -1.0f, // Bottom face
         0.5f, -0.5f, -0.5f,  1.0f, -1.0f,  -1.0f,
         0.5f, -0.5f,  0.5f,  1.0f, -1.0f,  1.0f,
         
         0.5f, -0.5f,  0.5f,  1.0f, -1.0f,  1.0f,
        -0.5f, -0.5f,  0.5f,  -1.0f, -1.0f,  1.0f,
        -0.5f, -0.5f, -0.5f,  -1.0f, -1.0f,  -1.0f,

        -0.5f,  0.5f, -0.5f,  -1.0f,  1.0f,  -1.0f, // Top face
         0.5f,  0.5f, -0.5f,  1.0f,  1.0f,  -1.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,  1.0f,
         
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,  1.0f,
        -0.5f,  0.5f,  0.5f,  -1.0f,  1.0f,  1.0f,
        -0.5f,  0.5f, -0.5f,  -1.0f,  1.0f,  -1.0f
    };
    
    private readonly float[] _verticesTextureSmoothed =
    {
        // Positions          Normals              Texture coords
        -0.5f, -0.5f, -0.5f,  -1.0f,  -1.0f, -1.0f,  0.0f, 0.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  -1.0f, -1.0f,  1.0f, 0.0f,
         0.5f,  0.5f, -0.5f,  1.0f,  1.0f, -1.0f,  1.0f, 1.0f,
         0.5f,  0.5f, -0.5f,  1.0f,  1.0f, -1.0f,  1.0f, 1.0f,
        -0.5f,  0.5f, -0.5f,  -1.0f,  1.0f, -1.0f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f,  -1.0f,  -1.0f, -1.0f,  0.0f, 0.0f,

        -0.5f, -0.5f,  0.5f,  -1.0f,  -1.0f,  1.0f,  0.0f, 0.0f,
         0.5f, -0.5f,  0.5f,  1.0f,  -1.0f,  1.0f,  1.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,  1.0f,  1.0f, 1.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,  1.0f,  1.0f, 1.0f,
        -0.5f,  0.5f,  0.5f,  -1.0f,  1.0f,  1.0f,  0.0f, 1.0f,
        -0.5f, -0.5f,  0.5f,  -1.0f,  -1.0f,  1.0f,  0.0f, 0.0f,

        -0.5f,  0.5f,  0.5f, -1.0f,  -1.0f,  1.0f,  1.0f, 0.0f,
        -0.5f,  0.5f, -0.5f, -1.0f,  -1.0f,  1.0f,  1.0f, 1.0f,
        -0.5f, -0.5f, -0.5f, -1.0f,  -1.0f,  -1.0f,  0.0f, 1.0f,
        -0.5f, -0.5f, -0.5f, -1.0f,  -1.0f,  -1.0f,  0.0f, 1.0f,
        -0.5f, -0.5f,  0.5f, -1.0f,  -1.0f,  1.0f,  0.0f, 0.0f,
        -0.5f,  0.5f,  0.5f, -1.0f,  1.0f,  1.0f,  1.0f, 0.0f,

         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,  1.0f,  1.0f, 0.0f,
         0.5f,  0.5f, -0.5f,  1.0f,  1.0f,  -1.0f,  1.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  -1.0f,  -1.0f,  0.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  1.0f,  -1.0f,  -1.0f,  0.0f, 1.0f,
         0.5f, -0.5f,  0.5f,  1.0f,  -1.0f,  1.0f,  0.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,  1.0f,  1.0f, 0.0f,

        -0.5f, -0.5f, -0.5f,  -1.0f, -1.0f,  -1.0f,  0.0f, 1.0f,
         0.5f, -0.5f, -0.5f,  1.0f, -1.0f,  -1.0f,  1.0f, 1.0f,
         0.5f, -0.5f,  0.5f,  1.0f, -1.0f,  1.0f,  1.0f, 0.0f,
         0.5f, -0.5f,  0.5f,  1.0f, -1.0f,  1.0f,  1.0f, 0.0f,
        -0.5f, -0.5f,  0.5f,  -1.0f, -1.0f,  1.0f,  0.0f, 0.0f,
        -0.5f, -0.5f, -0.5f,  -1.0f, -1.0f,  -1.0f,  0.0f, 1.0f,

        -0.5f,  0.5f, -0.5f,  -1.0f,  1.0f,  -1.0f,  0.0f, 1.0f,
         0.5f,  0.5f, -0.5f,  1.0f,  1.0f,  -1.0f,  1.0f, 1.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,  1.0f,  1.0f, 0.0f,
         0.5f,  0.5f,  0.5f,  1.0f,  1.0f,  1.0f,  1.0f, 0.0f,
        -0.5f,  0.5f,  0.5f,  -1.0f,  1.0f,  1.0f,  0.0f, 0.0f,
        -0.5f,  0.5f, -0.5f,  -1.0f,  1.0f,  -1.0f,  0.0f, 1.0f
    };
    
    private Vector3 _lightPos = new Vector3(1.2f, 1.0f, 2.0f);
    private Vector3 _cubePosition;

    private float _scale;
    
    private Object _object;
    private ObjectTexture _objectTexture;
    private ObjectFrame _objectFrame;
    private ObjectNormal _objectNormal;
    private Lamp _lamp;
    
    private Object _objectSmoothed;
    private ObjectTexture _objectTextureSmoothed;
    private ObjectNormal _objectNormalSmoothed;
    
    private IObject _currentObject;
    private IObject _currentObjectSmoothed;

    private Camera _camera;

    private Vector2 _lastPos;

    private GUI.GUI _gui;
    private ImGuiController _controller;

    private bool _canMove;
    private bool _smoothedNormals;
    private bool _firstMove = true;
    private bool _renderNormals;
    private bool _spotLightSource = true;
    
    private enum AppMode
    {
        MovementMode,
        CursorMode
    }

    public int CurrentAppMode = (int)AppMode.CursorMode;

    public Window(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings)
        : base(gameWindowSettings, nativeWindowSettings)
    {
    }

    protected override void OnLoad()
    {
        base.OnLoad();

        using (var sr = new StreamReader("Input/Section"))
        {
            string[] data;

            data = sr.ReadLine()!.Split(" ").ToArray();

            _cubePosition = new Vector3(Convert.ToSingle(data[0]), Convert.ToSingle(data[1]), Convert.ToSingle(data[2]));
            
            data = sr.ReadLine()!.Split(" ").ToArray();

            _cubePosition += (Convert.ToSingle(data[0]), Convert.ToSingle(data[1]), Convert.ToSingle(data[2]));
            _cubePosition /= 2;
            
            data = sr.ReadLine()!.Split(" ").ToArray();

            Vector3 kek2 = new Vector3(Convert.ToSingle(data[0]), Convert.ToSingle(data[1]), Convert.ToSingle(data[2]));
            
            data = sr.ReadLine()!.Split(" ").ToArray();

            kek2 += (Convert.ToSingle(data[0]), Convert.ToSingle(data[1]), Convert.ToSingle(data[2]));
            kek2 /= 2;

            _cubePosition += kek2;
            _cubePosition /= 2;
        }

        using (var sr = new StreamReader("Input/Replication"))
        {
            string[] data;

            _scale = Convert.ToSingle(sr.ReadLine());

            data = sr.ReadLine()!.Split(" ").ToArray();
            _cubePosition += (Convert.ToSingle(data[0]), Convert.ToSingle(data[1]), Convert.ToSingle(data[2]));
        }
        
        GL.ClearColor(0.1f, 0.1f, 0.1f, 1.0f);
        
        GL.Enable(EnableCap.DepthTest);

        _object = new Object(_vertices, _cubePosition, _scale);
        _objectSmoothed = new Object(_verticesSmoothed, _cubePosition, _scale);
        
        _objectTexture = new ObjectTexture(_verticesTexture, _cubePosition, _scale);
        _objectTextureSmoothed = new ObjectTexture(_verticesTextureSmoothed, _cubePosition, _scale);
        
        _objectFrame = new ObjectFrame(_verticesFrame, _cubePosition, _scale);
        
        _objectNormal = new ObjectNormal(_normals, _cubePosition, _scale);
        _objectNormalSmoothed = new ObjectNormal(_normalsSmoothed, _cubePosition, _scale);
        
        _lamp = new Lamp(_lightPos, _vertices);

        _currentObject = _object;
        _currentObjectSmoothed = _objectSmoothed;

        _controller = new ImGuiController(ClientSize.X, ClientSize.Y);
        _gui = new GUI.GUI(_controller, this);
        
        _camera = new Camera(Vector3.UnitZ * 3, Size.X / (float)Size.Y, Size.X, Size.Y);
    }

    protected override void OnRenderFrame(FrameEventArgs e)
    {
        base.OnRenderFrame(e);
        
        GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

        if (_smoothedNormals)
        {
            _currentObjectSmoothed.Render(_camera, _lightPos);
            
            if (_renderNormals)
            {
                _objectNormalSmoothed.Render(_camera, _lightPos);
            }
        }

        else
        {
            _currentObject.Render(_camera, _lightPos);

            if (_renderNormals)
            {
                _objectNormal.Render(_camera, _lightPos);
            }
        }
        
        _lamp.Render(_camera);
        
        _controller.Update(this, (float)e.Time);
        _gui.Draw();
        
        _controller.Render();
        
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
        var mouse = MouseState;

        if (input.IsKeyDown(Keys.Escape))
        {
            Close();
        }

        const float cameraSpeed = 1.5f;
        const float sensitivity = 0.2f;

        if (input.IsKeyDown(Keys.M))
        {
            _canMove = true;
            _lastPos = new Vector2(mouse.X, mouse.Y);
            
            CursorState = CursorState.Grabbed;
            CurrentAppMode = (int)AppMode.MovementMode;
        }

        if (input.IsKeyDown(Keys.N))
        {
            _canMove = false;
            
            CursorState = CursorState.Normal;
            CurrentAppMode = (int)AppMode.CursorMode;
        }

        if (input.IsKeyDown(Keys.P))
        {
            _camera.IsPerspective = true;
        }
        
        if (input.IsKeyDown(Keys.O))
        {
            _camera.IsPerspective = false;
        }

        if (_canMove)
        {
            if (input.IsKeyDown(Keys.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
            }
            if (input.IsKeyDown(Keys.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
            }
            if (input.IsKeyDown(Keys.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
            }
            if (input.IsKeyDown(Keys.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
            }
            if (input.IsKeyDown(Keys.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
            }
            if (input.IsKeyDown(Keys.LeftShift))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
            }

            if (_firstMove)
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity;
            }
        }

    }

    protected override void OnResize(ResizeEventArgs e)
    {
        base.OnResize(e);
        
        GL.Viewport(0, 0, Size.X, Size.Y);
    }

    public void ChangeToMaterialModel()
    {
        _currentObject = _object;
        _currentObjectSmoothed = _objectSmoothed;
    }

    public void ChangeToTextureObject()
    {
        _currentObject = _objectTexture;
        _currentObjectSmoothed = _objectTextureSmoothed;
    }

    public void ChangeToFrameObject()
    {
        _currentObject = _objectFrame;
        _currentObjectSmoothed = _objectFrame;
    }

    public void TurnOnFlashlight()
    {
        if (_currentObject != _objectFrame)
        {
            _currentObject.TurnOnFlashlight();
            _currentObjectSmoothed.TurnOnFlashlight();
        }
    }
    
    public void TurnOffFlashlight()
    {
        if (_currentObject != _objectFrame)
        {
            _currentObject.TurnOffFlashlight();
            _currentObjectSmoothed.TurnOffFlashlight();
        }
    }

    public void TurnOnNormals()
    {
        _renderNormals = true;
    }
    
    public void TurnOffNormals()
    {
        _renderNormals = false;
    }

    public void SmoothedNormals()
    {
        _smoothedNormals = true;
    }
    
    public void UnSmoothedNormals()
    {
        _smoothedNormals = false;
    }
}