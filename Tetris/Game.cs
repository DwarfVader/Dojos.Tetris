namespace Tetris;

public class Game
{
	public Game()
	{
		_random = new Random();
		_topRowCenterPosition = new Vector(Width / 2, 0);

		Reset();
	}

	private readonly Random _random;
	private readonly Vector _topRowCenterPosition;

	public GameState State { get; private set; }
	public BlockType?[,] Blocks { get; private set; } = null!;
	public Shape Shape { get; private set; } = null!;
	
	public static int Width => 10;
	public static int Height => 16;

	private bool IsValidShapePosition(Shape newShape)
	{
		for (var j = 0; j < newShape.Height; j++)
		{
			for (var i = 0; i < newShape.Width; i++)
			{
				if (!newShape.Layout[i, j])
				{
					continue;
				}

				if (newShape.Position.X + i < 0
					|| newShape.Position.X + i >= Width
					|| newShape.Position.Y + j < 0
					|| newShape.Position.Y + j >= Height
					|| Blocks[newShape.Position.X + i, newShape.Position.Y + j] != null)
				{
					return false;
				}
			}
		}

		return true;
	}

	private void InsertShape()
	{
		for (var j = 0; j < Shape.Height; j++)
		{
			for (var i = 0; i < Shape.Width; i++)
			{
				if (Shape.Layout[i, j])
				{
					Blocks[Shape.Position.X + i, Shape.Position.Y + j] = Shape.BlockType;
				}
			}
		}
	}

	private void RemoveFullRows()
	{
		for (var j = 0; j < Height; j++)
		{
			if (Enumerable.Range(0, Width).Any(i => Blocks[i, j] == null))
			{
				continue;
			}

			for (var i = 0; i < Width; i++)
			{
				Blocks[i, j] = null;
			}
		}
	}

	private void CloseVerticalGaps()
	{
		var currentEmptyRowCount = 0;
		
		for (var j = Height-1; j >= 0; j--)
		{
			if (Enumerable.Range(0, Width).All(i => Blocks[i, j] == null))
			{
				currentEmptyRowCount++;
				continue;
			}

			if (currentEmptyRowCount == 0)
			{
				continue;
			}

			for (var jj = j; jj >= 0; jj--)
			{
				SwapRows(jj, jj+currentEmptyRowCount);
			}

			currentEmptyRowCount = 0;
		}
	}

	private void SwapRows(int j1, int j2)
	{
		for (var i = 0; i < Width; i++)
		{
			(Blocks[i, j1], Blocks[i, j2]) = (Blocks[i, j2], Blocks[i, j1]);
		}
	}

	public void CreateNewShape()
	{
		var blockTypes = Enum.GetValues<BlockType>();
		var blockType = blockTypes[_random.Next() % blockTypes.Length];

		Shape = new Shape(blockType).Initialize(_topRowCenterPosition);

		if (!IsValidShapePosition(Shape))
		{
			State = GameState.GameOver;
		}
	}

	public void TryMoveShape(Vector deltaPosition)
	{
		if (State != GameState.RunningHandleInput)
		{
			return;
		}
		
		var newShape = Shape.Clone();
		newShape.Position += deltaPosition;
		if (!IsValidShapePosition(newShape))
		{
			//could not move down
			if (deltaPosition.Y > 0)
			{
				State = GameState.RunningIgnoreInput;
				
				InsertShape();
				RemoveFullRows();
				CloseVerticalGaps();
				CreateNewShape();

				if (State == GameState.RunningIgnoreInput)
				{
					State = GameState.RunningHandleInput;
				}
			}

			return;
		}

		Shape = newShape;
	}

	public void TryRotate(RotateDirection rotateDirection)
	{
		if (State != GameState.RunningHandleInput)
		{
			return;
		}
		
		var newShape = Shape.Clone();
		newShape.Rotate(rotateDirection);
		if (!IsValidShapePosition(newShape))
		{
			return;
		}

		Shape = newShape;
	}

	public void Reset()
	{
		Blocks = new BlockType?[Width, Height];
		CreateNewShape();
		
		State = GameState.RunningHandleInput;
	}
}