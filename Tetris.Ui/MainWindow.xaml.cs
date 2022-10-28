using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Tetris.Ui;

public partial class MainWindow
{
	public MainWindow()
	{
		InitializeComponent();

		_game = new Game();
		_game.CreateNewShape();
		CalcBlockSize();
		RenderGame();
	}

	private readonly Game _game;
	private double _blockSize;

	private void MainWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
	{
		CalcBlockSize();
		RenderGame();
	}

	private void MainWindow_OnPreviewKeyDown(object sender, KeyEventArgs e)
	{
		switch (e.Key)
		{
			case Key.A:
				_game.TryMoveShape(new Vector(-1, 0));
				RenderGame();
				break;
			case Key.D:
				_game.TryMoveShape(new Vector(1, 0));
				RenderGame();
				break;
			case Key.S:
				_game.TryMoveShape(new Vector(0, 1));
				RenderGame();
				break;
			case Key.Q:
				_game.TryRotate(RotateDirection.Left);
				RenderGame();
				break;
			case Key.E:
				_game.TryRotate(RotateDirection.Right);
				RenderGame();
				break;
			case Key.Escape:
				_game.Reset();
				RenderGame();
				break;
			
			case Key.W:
				_game.TryMoveShape(new Vector(0, -1));
				RenderGame();
				break;
			
		}
	}

	private void CalcBlockSize()
	{
		_blockSize = Math.Min(ContainerGrid.ActualWidth / Game.Width, ContainerGrid.ActualHeight / Game.Height);
	}

	private void RenderGame()
	{
		Canvas.Children.Clear();
		Canvas.Width = Game.Width * _blockSize;
		Canvas.Height = Game.Height * _blockSize;
		Canvas.Background = Brushes.Black;
		
		//draw placed blocks
		for (var j = 0; j < Game.Height; j++)
		{
			for (var i = 0; i < Game.Width; i++)
			{
				var block = _game.Blocks[i, j];
				if (block.HasValue)
				{
					DrawBlock(i * _blockSize, j * _blockSize, _blockSize, _blockSize, 
						UiConstants.BlockTypeColors[block.Value], Brushes.Black, _blockSize / 16);
				}
				else
				{
					DrawBlock(i * _blockSize, j * _blockSize, _blockSize, _blockSize, Brushes.LightGray, Brushes.Black,
						1);
				}
			}
		}
		
		//draw shape to insert
		for (var j = 0; j < _game.Shape.Height; j++)
		{
			for (var i = 0; i < _game.Shape.Width; i++)
			{
				if (_game.Shape.Layout[i, j])
				{
					DrawBlock((i + _game.Shape.Position.X) * _blockSize, (j + _game.Shape.Position.Y) * _blockSize, 
						_blockSize, _blockSize, UiConstants.BlockTypeColors[_game.Shape.BlockType], Brushes.Black, _blockSize / 16);
				}
			}
		}
		
		//draw game over text
		GameOverLabel.Visibility = _game.State == GameState.GameOver 
			? Visibility.Visible 
			: Visibility.Collapsed;
	}

	private void DrawBlock(double x, double y, double width, double height, Brush fillColor,
		Brush strokeColor, double strokeThickness)
	{
		var rect = new Rectangle
		{
			Stroke = strokeColor,
			StrokeThickness = strokeThickness,
			Fill = fillColor,
			Width = width,
			Height = height
		};
		Canvas.SetLeft(rect, x);
		Canvas.SetTop(rect, y);
		Canvas.Children.Add(rect);
	}
}