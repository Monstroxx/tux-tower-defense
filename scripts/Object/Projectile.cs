using Godot;
using System;

public partial class Projectile : Area2D
{
	public Vector2 Velocity;

	[Export]
	private VisibleOnScreenNotifier2D Notifier;

	public bool IsOnScreen {
		get { return Notifier.IsOnScreen(); }
	}
}
