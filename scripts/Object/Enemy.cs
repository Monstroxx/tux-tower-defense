using Godot;
using System;

public partial class Enemy : PathFollow2D
{
	private float speed = 100;

	public bool IsFinished = false;

	public void Move(double delta)
	{
		var lastProgress = Progress;

		Progress += speed * (float)delta;

		if (Progress == lastProgress)
		{
			IsFinished = true;

			var gameNode = GetParent().GetParent();
			if (gameNode is Game game)
			{
				game.EnemieFinished(this);
			}
			
			QueueFree();
		}
	}
}
