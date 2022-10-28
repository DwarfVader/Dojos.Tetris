namespace Tetris;

public static class Constants
{
	static Constants()
	{
		Shapes = new()
		{
			{
				BlockType.I, new[,]
				{
					{true},
					{true},
					{true},
					{true}
				}
			},
			{
				BlockType.O, new[,]
				{
					{true, true},
					{true, true}
				}
			},
			{
				BlockType.J, new[,]
				{
					{false, true},
					{false, true},
					{true, true}
				}
			},
			{
				BlockType.L, new[,]
				{
					{true, false},
					{true, false},
					{true, true}
				}
			},
			{
				BlockType.Z, new[,]
				{
					{true, false},
					{true, true},
					{false, true}
				}
			},
			{
				BlockType.S, new[,]
				{
					{false, true},
					{true, true},
					{true, false}
				}
			},
			{
				BlockType.T, new[,]
				{
					{true, false},
					{true, true},
					{true, false}
				}
			}
		};
	}

	public static Dictionary<BlockType, bool[,]> Shapes { get; }
}