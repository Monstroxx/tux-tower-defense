using Godot;
using System;
using System.Collections.Generic;

public partial class Game : Node2D
{
    private PackedScene[] possibleEnemies = [GD.Load<PackedScene>("res://scenes/enemies/enemie.tscn")];

    private PackedScene ubuntuTowerScene = GD.Load<PackedScene>("res://scenes/tower/ubuntu_tower.tscn");
    private PackedScene fedoraTowerScene = GD.Load<PackedScene>("res://scenes/tower/fedora_tower.tscn");

    #nullable enable
    private Tower? currentDragingTower;
    private bool alreadyClicked = false;

    public static int Coins = 10;
    public static int Health = 100;

    public static bool IsPlaying = false;

    [Export]
    public float StartFrequency = 3;

    [Export]
    public int EnemysInFirstRound = 5;

    [Export]
    public float EnemyMultiplyer = 1.15f;

    [Export]
    public Path2D Path;

    [Export]
    public Label HealthLabel;

    [Export]
    public Label CoinLabel;

    [Export]
    public VBoxContainer TowerSelection;

    private float maxEnemyCount;
    private float frequency;

    private readonly List<Enemie> enemies = [];

    private readonly Timer enemieSpawnTimer = new();

	public override void _Ready()
    {
        maxEnemyCount = EnemysInFirstRound;
        frequency = StartFrequency;

        TowerSelection.Hide();

        // EnemieSpawnerTimer
        enemieSpawnTimer.WaitTime = frequency;

        enemieSpawnTimer.Autostart = true;
        enemieSpawnTimer.Timeout += SpawnEnemy;

        AddChild(enemieSpawnTimer);

        enemieSpawnTimer.Start();

        // Labels
        HealthLabel.Text = $"Health: {Health}";
        CoinLabel.Text = $"Coins: {Coins}";
    }
    
    public override void _Process(double delta)
    {
        
        if (currentDragingTower != null)
        {
            currentDragingTower.GlobalPosition = GetGlobalMousePosition();

            if (Input.IsMouseButtonPressed(MouseButton.Left))
            {
                if (alreadyClicked) {
                    currentDragingTower.Enabled = true;
                    currentDragingTower = null;
                }
                else
                {
                    alreadyClicked = true;
                }
            }
            
        }
        else if (alreadyClicked)
        {
            alreadyClicked = false;
        }
    }

    public void SpawnEnemy()
    {
        GD.Print($"Enemie count: {Path.GetChildren().Count}");
        if (!IsPlaying) return;

        Random rn = new();
        var enemy = possibleEnemies[rn.NextInt64(0,possibleEnemies.Length - 1)].Instantiate();
        Path.AddChild(enemy);
    }

    public void EnemieFinished(Enemie enemie)
    {
        GD.Print("Enemie got through");
        if (!IsPlaying) return;

        Path.RemoveChild(enemie);
        enemie.QueueFree();
        Health -= 15;
        HealthLabel.Text = $"Health: {Health}";

        if (Health <= 0)
        {
            IsPlaying = false;
            GetTree().ChangeSceneToFile("res://scenes/main_menue.tscn");
        }
    }

    public void _on_open_towers_button_pressed()
    {
        TowerSelection.Visible = !TowerSelection.Visible;
    }

    public void _on_ubuntu_tower_button_pressed()
    {
        if (Coins >= 10)
        {
            var tower = ubuntuTowerScene.Instantiate<Tower>();
            AddChild(tower);
            currentDragingTower = tower;

            Coins -= 10;
            CoinLabel.Text = $"Coins: {Coins}";
        }
    }

    public void _on_fedora_tower_button_pressed()
    {
        if (Coins >= 20)
        {
            var tower = fedoraTowerScene.Instantiate<Tower>();
            AddChild(tower);
            currentDragingTower = tower;

            Coins -= 20;
            CoinLabel.Text = $"Coins: {Coins}";
        }
    }
}
