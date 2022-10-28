namespace Tetris;

public class Block
{
	public Block(BlockType type)
	{
		Type = type;
	}

	public BlockType Type { get; }
}