using Godot;
using System;
using System.Collections.Generic;

public partial class TowerData
{
	private readonly List<Node2D> projectiles = [];
	public double AttackSpeed {
		get {return attackTimer.WaitTime;} 
		set {attackTimer.WaitTime = value;}
	}
	private int damage;
	private float stunTimer;

	private Timer attackTimer = new();

	public TowerData(double attackSpeed, int damage)
	{
		this.AttackSpeed = attackSpeed;
		this.damage = damage;
	}
}
