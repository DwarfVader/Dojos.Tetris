namespace Tetris;

public class Shape
{
	public Shape(BlockType blockType)
	{
		BlockType = blockType;
		Layout = (bool[,]) Constants.Shapes[blockType].Clone();
	}

	public Vector Position { get; set; } = null!;
	public BlockType BlockType { get; }
	public bool[,] Layout { get; private set; }

	public int Width => Layout.GetLength(0);
	public int Height => Layout.GetLength(1);

	public Shape Initialize(Vector topRowCenterPosition)
	{
		Position = new Vector(topRowCenterPosition.X - Layout.GetLength(0) / 2, topRowCenterPosition.Y);
		return this;
	}
	
	public void Rotate(RotateDirection rotateDirection)
	{
		var newLayout = new bool[Height, Width];
		for (var j = 0; j < Height; j++)
		{
			for (var i = 0; i < Width; i++)
			{
				switch (rotateDirection)
				{
					case RotateDirection.Left:
						newLayout[j, Width - 1 - i] = Layout[i, j];
						break;
					case RotateDirection.Right:
						newLayout[Height - 1 - j, i] = Layout[i, j];
						break;
				}
			}
		}

		//try keep rotated layout horizontally centered around old layout
		Position.X += (Width - Height) / 2;
		Layout = newLayout;
	}

	public Shape Clone()
	{
		return new Shape(BlockType)
		{
			Position = new Vector(Position),
			Layout = (bool[,])Layout.Clone()
		};
	}
}