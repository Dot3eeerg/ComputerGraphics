namespace _2lab.Objects;

using Shaders;
using BufferObjects;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;

public class Object : IObject
{
    private VertexBufferObject _vbo;
    private VertexArrayObject _vao;
    
    private float[] _vertices;
    
    private Shader _shader;
    
    private float[] _flashLightValues = {0.0f, 0.0f};
    private Vector3[,] _SpotLightValues =
    {
    };
    
    private Vector3 _position;
    private float _scale;
    
    public Object(float[] vertices, Vector3 position, float scale)
    {
        _vertices = vertices;
        _position = position;
        _scale = scale;
        
        _shader = new Shader("Shaders/lightShader.vert", "Shaders/lightShader.frag");
        _shader.Use();

        var vertexLocation = _shader.GetAttribLocation("aPos");
        
        _vbo = new VertexBufferObject(_vertices);
        _vao = new VertexArrayObject(vertexLocation, 6);
        _vao.EnableArray(vertexLocation, 0);
        
        var normalLocation = _shader.GetAttribLocation("aNormal");

        _vao.EnableArray(normalLocation, 3 * sizeof(float));
    }
    
    public void Render(Camera camera, Vector3 lightPos)
    {
        _vao.Bind();
        
        _shader.Use();
        
        Matrix4 model = Matrix4.CreateScale(_scale) * Matrix4.CreateTranslation(_position);
        _shader.SetMatrix4("model", model);
        _shader.SetMatrix4("view", camera.GetViewMatrix());
        _shader.SetMatrix4("projection", camera.GetProjectionMatrix());
        
        _shader.SetVector3("viewPos", camera.Position);
        
        _shader.SetVector3("material.ambient", new Vector3(1.0f, 0.5f, 0.31f));
        _shader.SetVector3("material.diffuse", new Vector3(1.0f, 0.5f, 0.31f));
        _shader.SetVector3("material.specular", new Vector3(0.5f, 0.5f, 0.5f));
        _shader.SetFloat("material.shininess", 32.0f);
        
        // Directional light
        _shader.SetVector3("dirLight.direction", new Vector3(-0.2f, -1.0f, -0.3f));
        _shader.SetVector3("dirLight.ambient", new Vector3(0.05f, 0.05f, 0.05f));
        _shader.SetVector3("dirLight.diffuse", new Vector3(0.4f, 0.4f, 0.4f));
        _shader.SetVector3("dirLight.specular", new Vector3(0.5f, 0.5f, 0.5f));

        // Point light
        _shader.SetVector3($"pointLights[0].position", lightPos);
        
        _shader.SetVector3($"pointLights[0].ambient", new Vector3(0.05f, 0.05f, 0.05f));
        _shader.SetVector3($"pointLights[0].diffuse", new Vector3(0.8f, 0.8f, 0.8f));
        _shader.SetVector3($"pointLights[0].specular", new Vector3(1.0f, 1.0f, 1.0f));
        
        _shader.SetFloat($"pointLights[0].constant", 1.0f);
        _shader.SetFloat($"pointLights[0].linear", 0.09f);
        _shader.SetFloat($"pointLights[0].quadratic", 0.032f);
        
        // Spot light
        _shader.SetVector3("spotLight.position", camera.Position);
        _shader.SetVector3("spotLight.direction", camera.Front);
        _shader.SetVector3("spotLight.ambient", new Vector3(0.0f, 0.0f, 0.0f));
        _shader.SetVector3("spotLight.diffuse", new Vector3(1.0f, 1.0f, 1.0f));
        _shader.SetVector3("spotLight.specular", new Vector3(1.0f, 1.0f, 1.0f));
        _shader.SetFloat("spotLight.constant", 1.0f);
        _shader.SetFloat("spotLight.linear", 0.09f);
        _shader.SetFloat("spotLight.quadratic", 0.032f);
        _shader.SetFloat("spotLight.cutOff", MathF.Cos(MathHelper.DegreesToRadians(_flashLightValues[0])));
        _shader.SetFloat("spotLight.outerCutOff", MathF.Cos(MathHelper.DegreesToRadians(_flashLightValues[1])));
        
        GL.DrawArrays(PrimitiveType.Triangles, 0, 36);
    }

    public void TurnOnFlashlight()
    {
        _flashLightValues[0] = 12.5f;
        _flashLightValues[1] = 17.5f;
    }
    
    public void TurnOffFlashlight()
    {
        _flashLightValues[0] = 0.0f;
        _flashLightValues[1] = 0.0f;
    }
    
    public void TurnOnSpotlight()
    {
    }
    
    public void TurnOffSpotlight()
    {
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