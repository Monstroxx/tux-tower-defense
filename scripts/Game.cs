using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Node2D
{
    private PackedScene[] possibleEnemies = [GD.Load<PackedScene>("res://scenes/enemies/enemie.tscn")];

    public static int Coins = 10;

    [Export]
    public float StartFrequency = 3;

    [Export]
    public int EnemysInFirstRound = 5;

    [Export]
    public float EnemyMultiplyer = 1.15f;

    [Export]
    public Path2D Path;

    private float maxEnemyCount;
    private float frequency;

    private readonly List<Enemie> enemies = [];

    private readonly Timer enemieSpawnTimer = new();

	public override void _Ready()
    {
        maxEnemyCount = EnemysInFirstRound;
        frequency = StartFrequency;

        enemieSpawnTimer.WaitTime = frequency;

        enemieSpawnTimer.Autostart = true;
        enemieSpawnTimer.Ready += SpawnEnemy;
    }
    
    public override void _Process(double delta)
    {
        
    }

    public void SpawnEnemy()
    {
        Random rn = new();

        var enemy = possibleEnemies[rn.NextInt64(0,possibleEnemies.Length - 1)].Instantiate();

        Path.AddChild(enemy);
    }
}