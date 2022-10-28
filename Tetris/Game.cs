﻿namespace Tetris;

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

	private bool IsValidShapePosition(Shape shape)
	{
		for (var j = 0; j < shape.Height; j++)
		{
			for (var i = 0; i < shape.Width; i++)
			{
				if (!shape.Layout[i, j])
				{
					continue;
				}

				if (shape.Position.X + i < 0
					|| shape.Position.X + i >= Width
					|| shape.Position.Y + j < 0
					|| shape.Position.Y + j >= Height
					|| Blocks[shape.Position.X + i, shape.Position.Y + j] != null)
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

		//TODO 1: iterate over all rows; clear row if each column has content
		
	}

	private void CloseVerticalGaps()
	{
		
		//TODO 3: determine empty rows and let all (non-empty) rows above drop to fill the gaps
		//TODO 3: hint: use SwapRows() method to swap current non-empty row with empty row
		
	}

	private void SwapRows(int row1, int row2)
	{
		
		//TODO 2: iterate over all columns and swap content of row1 with row2
		
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