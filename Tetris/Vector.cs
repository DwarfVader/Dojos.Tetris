namespace Tetris;

public class Vector
{
	public Vector(Vector other) : this(other.X, other.Y)
	{
	}

	public Vector(int x, int y)
	{
		X = x;
		Y = y;
	}

	public int X { get; set; }
	public int Y { get; set; }

	public static Vector operator +(Vector vector, Vector other)
	{
		return new Vector(vector.X + other.X, vector.Y + other.Y);
	}
}