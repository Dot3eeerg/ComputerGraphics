namespace _1lab;

public record struct Vertex
{
    public float X { get; set; }
    public float Y { get; set; }

    public Vertex(float x, float y)
    {
        X = x;
        Y = y;
    }
}