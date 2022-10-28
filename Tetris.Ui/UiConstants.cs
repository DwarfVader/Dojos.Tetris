using System.Collections.Generic;
using System.Windows.Media;

namespace Tetris.Ui;

public static class UiConstants
{
	static UiConstants()
	{
		BlockTypeColors = new Dictionary<BlockType, SolidColorBrush>
		{
			{BlockType.I, Brushes.Red},
			{BlockType.O, Brushes.Yellow},
			{BlockType.J, Brushes.LimeGreen},
			{BlockType.L, Brushes.DodgerBlue},
			{BlockType.Z, Brushes.SkyBlue},
			{BlockType.S, Brushes.Magenta},
			{BlockType.T, Brushes.Orange},
		};
	}

	public static Dictionary<BlockType, SolidColorBrush> BlockTypeColors;
}