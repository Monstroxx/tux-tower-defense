using Godot;
using System;
using System.Collections.Generic;

public partial class Tower : Area2D
{
	private Timer attackTimer = new();

	// Liste erstellen, um die Projektile zu verwalten
	public List<Projectile> projectiles = [];

	public bool Enabled = false;

	public TowerData data;

	[Export]
	private Sprite2D sprite;

	public void SetTexture(Texture2D texture)
	{
		if (sprite != null)
		{
			sprite.Texture = texture;
		} else
		{
			GD.Print($"Sprite of {this} is not set");
		}
	}

	public override void _Ready()
	{
		attackTimer.Timeout += ShootTimerUp;
		attackTimer.WaitTime = data.AttackSpeed;
		attackTimer.Autostart = true;
		AddChild(attackTimer);
		attackTimer.Start();

		if (data == null) {GD.PrintErr($"TowerData of {this} is not set!");}
		if (sprite == null) {GD.PrintErr($"Sprite of {this} is not set!"); sprite = GetNode<Sprite2D>("Sprite");}
	}

	// Listen Verwaltung der Projektile
	public override void _Process(double delta)
	{
		var projectilesToRemove = new List<Projectile>();
		foreach (var projectile in projectiles)
		{
			if (!projectile.IsVisible)
			{
				projectilesToRemove.Add(projectile);
			}
		}
		foreach (var projectile in projectilesToRemove)
		{
			projectiles.Remove(projectile);
			projectile.QueueFree();
			continue;
		}
		foreach (var projectile in projectiles)
		{
			projectile.Position += projectile.Velocity * (float)delta;
		}
	}

	public void ShootTimerUp()
	{
		foreach (var node in GetOverlappingAreas())
		{
			if (node.IsInGroup("Enemie") && node.GetParent() is Enemy enemie)
			{
				ShootAt(enemie);
				break;
			}
		}
	}

	public virtual void ShootAt(Enemy enemie)
	{
		GD.Print($"Pew to {enemie}");
	}
}
