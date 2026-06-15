using Godot;
using System;
using System.Collections.Generic;

public partial class FedoraTower : Tower
{
    public override void _Ready()
    {
        Texture2D texture = GD.Load<Texture2D>("res://assets/tower/Fedora.png");
        this.SpriteTexture = texture;
        
        base._Ready();
    }
}
