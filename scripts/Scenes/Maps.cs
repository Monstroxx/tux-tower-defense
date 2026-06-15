using Godot;
using System;

public partial class Maps : Node2D
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void _on_play_pressed()
	{
		GD.Print("Play pressed");
		Game.IsPlaying = true;
		GetTree().ChangeSceneToFile("res://scenes/maps/touch_grass.tscn");
	}
}
