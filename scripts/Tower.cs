using Godot;
using System;
using System.Collections.Generic;

public partial class Tower : StaticBody2D
{
	private Timer attackTimer = new();

	public Texture2D SpriteTexture
	{
		set {sprite.Texture = value;}
	}

	public TowerData data;

	[Export]
	private Sprite2D sprite;

	public override void _Ready()
	{
		this.attackTimer.Ready += this.shootTimerUp;

		if (data == null) {GD.PrintErr($"TowerData of {this} is not set!");}
		if (sprite == null) {GD.PrintErr($"Sprite of {this} is not set!"); sprite = GetNode<Sprite2D>("Sprite");}
	}

	public override void _Process(double delta)
	{
		
	}

	public virtual void shootTimerUp()
	{
		
	}
}
