using Godot;
using System;
using System.Collections.Generic;

public partial class UbuntuTower : Tower
{
    public override void _Ready()
    {
        Texture2D texture = GD.Load<Texture2D>("res://assets/tower/Ubuntu.png");
        this.SpriteTexture = texture;
    }
}