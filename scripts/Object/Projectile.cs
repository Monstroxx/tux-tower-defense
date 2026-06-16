using Godot;
using System;

public partial class Projectile : Area2D
{
	public Vector2 Velocity;

	[Export]
	public VisibleOnScreenNotifier2D Notifier;

	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		// Position += Velocity * (float)delta;

		// if (!Notifier.IsOnScreen())
		// {
		// 	QueueFree();
		// }
	}
}
