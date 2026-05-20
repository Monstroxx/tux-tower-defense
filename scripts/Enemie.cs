using Godot;
using System;

public partial class Enemie : Area2D
{
	public Path2D path;
	private int nextTarget = 0;

	public float speed = 1;

	public override void _Ready()
	{
	}

	public override void _Process(double delta)
	{
		
	}
}
