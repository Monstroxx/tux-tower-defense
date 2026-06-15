using Godot;
using System;
using System.Collections.Generic;

public partial class FedoraTower : Tower
{
    public PackedScene projectileScene = GD.Load<PackedScene>("res://scenes/tower/projectile.tscn");

    public override void _Ready()
    {
        Texture2D texture = GD.Load<Texture2D>("res://assets/tower/Fedora.png");
        this.SetTexture(texture);

        this.data = TowerData.FedoraTowerDate;

        base._Ready();
    }

    public override void ShootAt(Enemy enemie)
    {
        if (!Enabled) return;

        var projectile = projectileScene.Instantiate<Projectile>();
        projectile.Velocity = (enemie.GlobalPosition - GlobalPosition).Normalized() * 1500;
        projectile.Scale = new Vector2(2,2);
        AddChild(projectile);

        enemie.QueueFree();
        Game.Coins += 10;
    }
}
