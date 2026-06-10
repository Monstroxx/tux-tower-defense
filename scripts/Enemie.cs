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
		Progress += speed * (float)delta;
	}
}
