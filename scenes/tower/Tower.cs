using Godot;
using System;
using System.Collections.Generic;

public partial class Tower : StaticBody2D
{
	private readonly List<Node2D> projectiles = [];
	private double attackSpeed {
		get {return attackTimer.WaitTime;} 
		set {attackTimer.WaitTime = value;}
	}
	private int damage;
	private int range;
	private float stunTimer;

	private Timer attackTimer = new();

	[Export]
	public Sprite2D sprite;

	public Tower(double attackSpeed, int damage, int range, Texture2D texture)
	{
		this.attackSpeed = attackSpeed;
		this.damage = damage;
		this.range = range;

		sprite.Texture = texture;
	}

	public override void _Ready()
	{
		this.attackTimer.Ready += this.shootTimerUp;
	}

	public override void _Process(double delta)
	{
		
	}

	public virtual void shootTimerUp()
	{
		
	}
}
