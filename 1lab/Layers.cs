using System.Reflection.Metadata.Ecma335;

namespace _1lab;

public class Layers
{
    public List<List<float>> Vertices;
    public int CurrentLayer;
    public List<List<uint>> Indices;

    public Layers()
    {
        CurrentLayer = 0;
        Vertices = new List<List<float>>();
        Vertices.Add(new List<float>());
        Indices = new List<List<uint>>();
        Indices.Add(new List<uint>());
    }

    public void Update(float x, float y)
    {
        Vertices[CurrentLayer].Add(x);
        Vertices[CurrentLayer].Add(y);
        Vertices[CurrentLayer].Add(0.0f);
        Indices[CurrentLayer].Add((uint) Indices[CurrentLayer].Count);
    }

    public List<float> GetVertices()
        => Vertices[CurrentLayer];

    public List<float> GetVertices(int i)
        => Vertices[i];
    
    public List<uint> GetIndices()
        => Indices[CurrentLayer];
    
    public List<uint> GetIndices(int i)
        => Indices[i];

    public void CreateNewLayer()
    {
        Vertices.Add(new List<float>());
        Indices.Add(new List<uint>());
        CurrentLayer = Vertices.Count - 1;
    }

    public void ChangeLayer(int layerID)
    {
        if (layerID <= Vertices.Count)
        {
            CurrentLayer = layerID;
        }

        else
        {
            throw new Exception("This layer ID doesn't exists");
        }
    }
}