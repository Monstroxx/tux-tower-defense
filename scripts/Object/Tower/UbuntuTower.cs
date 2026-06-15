using Godot;
using System;
using System.Collections.Generic;

public partial class UbuntuTower : Tower
{
    public PackedScene projectileScene = GD.Load<PackedScene>("res://scenes/tower/projectile.tscn");

    public override void _Ready()
    {
        Texture2D texture = GD.Load<Texture2D>("res://assets/tower/Ubuntu.png");
        this.SetTexture(texture);

        this.data = TowerData.UbuntuTowerData;

        base._Ready();
    }

    public override void ShootAt(Enemy enemie)
    {
        if (!Enabled) return;

        var projectile = projectileScene.Instantiate<Projectile>();
        projectile.Velocity = (enemie.GlobalPosition - GlobalPosition).Normalized() * 1000;
        projectile.Scale = new Vector2(4,4);
        AddChild(projectile);

        enemie.QueueFree();
        Game.Coins += 5;
    }
}
