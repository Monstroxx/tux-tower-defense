using Godot;
using System;

public partial class Enemie : PathFollow2D
{
	public float speed = 100;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		var lastProgress = Progress;

		Progress += speed * (float)delta;

		if (Progress == lastProgress)
		{
			var gameNode = GetParent().GetParent();
			if (gameNode is Game game)
			{
				game.EnemieFinished(this);
			}
			QueueFree();
		}
	}
}
