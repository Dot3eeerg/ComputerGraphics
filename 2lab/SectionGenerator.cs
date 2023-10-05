namespace _2lab;

public class SectionGenerator
{
    private readonly float[] _verticesMaterial;
    private readonly float[] _verticesTexture;
    private readonly float[] _verticesFrame;
    private readonly float _scale;
    private readonly float[] _sectionVertices;

    private readonly int _verticesCount;

    public float[] VerticesMaterial => _verticesMaterial;
    public float[] VerticesTexture => _verticesTexture;
    public float[] VerticesFrame=> _verticesFrame;
    public float Scale => _scale;

    public SectionGenerator(string pathSection, string pathReplication)
    {
        using (var sr = new StreamReader(pathSection))
        {
            string[] data;
            _verticesCount = Convert.ToInt32(sr.ReadLine());

            _sectionVertices = new float[3 * _verticesCount];
            _verticesMaterial = new float[((_verticesCount - 2) * 2 + _verticesCount * 2) * 3 * 6];

            for (int i = 0; i < _verticesCount; i++)
            {
                data = sr.ReadLine()!.Split(" ").ToArray();

                _sectionVertices[i * 3] = Convert.ToSingle(data[0]);
                _sectionVertices[i * 3 + 1] = Convert.ToSingle(data[1]);
                _sectionVertices[i * 3 + 2] = Convert.ToSingle(data[2]);
            }
        }
    }
}