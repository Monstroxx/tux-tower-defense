using Godot;
using System;
using System.Security.Cryptography.X509Certificates;

public partial class Game : Node2D
{
	public static int Coins = 10;
	PathFollow2D path_follow_node;
	Node currentScene;
	public override void _Ready()
	{
		path_follow_node = GetNode<PathFollow2D>("Path2D/PathFollow2D");
		currentScene = GetTree().CurrentScene;
	}
	
	public override void _Process(double delta)
	{
		GetNode<PathFollow2D>("Path2D/PathFollow2D").Progress += 250 * (float)delta;

	}
	void spawn_at_point(PathFollow2D path_follow_node, PackedScene scene_to_spawn)
	{
		Node2D instance = (Node2D)scene_to_spawn.Instantiate();
		GetParent().AddChild(instance);
		instance.GlobalPosition = path_follow_node.GlobalPosition;
	}
}
