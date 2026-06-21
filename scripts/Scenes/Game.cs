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

	private static int coins = 100;
	private static int health = 100;

	// Getter und Setter
	public static int Coins
	{
		get {return coins;}
		set
		{
			if (value >= 0 && value <= 9999)
				coins = value;
		}
	}
	public static int Health
	{
		get {return health;}
		set
		{
			if (value >= 0 && value <= 9999)
				health = value;
			if (value <= 0)
			{
				IsPlaying = false;
				GameOver = true;
			}
		}
	}

	public static bool IsPlaying = false;
	public static bool GameOver = false;

	[Export]
	public float StartFrequency = 3;

	[Export]
	public int EnemysInFirstRound = 5;

	[Export]
	public float EnemyMultiplyer = 1.15f;

	[Export]
	public Path2D Path;

	[Export]
	public Ui Ui;

	[Export]
	public Node2D TowerNode;

	[Export]
	public VBoxContainer TowerSelection;

	[Export]
	public VBoxContainer Endscreen;

	private float maxEnemyCount;
	private float frequency;

	public static List<Enemy> Enemies = [];
	public static List<Tower> Towers = [];

	private readonly Timer enemieSpawnTimer = new();

	public override void _Ready()
	{
		maxEnemyCount = EnemysInFirstRound;
		frequency = StartFrequency;

		TowerSelection.Hide();
		Endscreen.Hide();
		if (!IsPlaying)
		{
			Ui.Hide();   
		}

		// EnemieSpawnerTimer
		enemieSpawnTimer.WaitTime = frequency;

		enemieSpawnTimer.Autostart = true;
		enemieSpawnTimer.Timeout += SpawnEnemy;

		AddChild(enemieSpawnTimer);

		enemieSpawnTimer.Start();

		Endscreen.Hide();
	}
	
	public override void _Process(double delta)
	{
		foreach (var tower in Towers)
		{
			tower.MoveProjectiles(delta);
		}

		foreach (var enemy in Enemies)
		{
			enemy.Move(delta);
		}

		if (currentDragingTower != null)
		{
			currentDragingTower.GlobalPosition = GetGlobalMousePosition();

			if (Input.IsMouseButtonPressed(MouseButton.Left))
			{
				if (alreadyClicked) {
					Towers.Add(currentDragingTower);

					currentDragingTower.Enabled = true;
					currentDragingTower = null;

					TowerSelection.Hide();
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

		Ui.CoinLabel.Text = $"Coins: {Coins}";
		Ui.HealthBar.Value = health;

		if (GameOver)
		{
			Endscreen.Show();
		}
	}

	public void SpawnEnemy()
	{
		//GD.Print($"Enemie count: {Path.GetChildren().Count}");
		if (!IsPlaying) return;

		Random rn = new();
		var enemy = possibleEnemies[rn.NextInt64(0,possibleEnemies.Length - 1)].Instantiate<Enemy>();
		SpawnObject(enemy);
	}

	public void EnemieFinished(Enemy enemy)
	{
		//GD.Print("Enemy got through");
		if (!IsPlaying) return;

		Path.RemoveChild(enemy);
		enemy.QueueFree();
		Health -= 15;
	}

	public void _on_open_towers_button_pressed()
	{
		TowerSelection.Visible = !TowerSelection.Visible;
	}

	public void _on_ubuntu_tower_button_pressed()
	{
		if (Coins >= 50)
		{
			var tower = ubuntuTowerScene.Instantiate<UbuntuTower>();
			SpawnObject(tower, 50);
		}
	}

	public void _on_fedora_tower_button_pressed()
	{
		if (Coins >= 80)
		{
			var tower = fedoraTowerScene.Instantiate<FedoraTower>();
			SpawnObject(tower, 80);
		}
	}

	// Überladen
	public void SpawnObject(Tower tower, int costs)
	{
		AddChild(tower);
		currentDragingTower = tower;
		Coins -= costs;
	}

	public void SpawnObject(Enemy enemy)
	{
		Path.AddChild(enemy);
		enemieSpawnTimer.WaitTime = Math.Max(0.1, enemieSpawnTimer.WaitTime * 0.98);
		Enemies.Add(enemy);
	}


	public void _on_enscreen_menue_pressed()
	{
		GetTree().ChangeSceneToFile("scenes/main_menue.tscn");
	}

	public void _on_enscreen_restart_pressed()
	{
		GameOver = false;
		Health = 100;
		Coins = 500; // Or whatever your starting coin amount is
		IsPlaying = true;

		// Remove all existing enemies and projectiles before reloading
		foreach (Node enemy in Path.GetChildren())
		{
			enemy.QueueFree();
		}
		foreach (Node tower in GetChildren())
		{
			if (tower is Tower)
			{
				tower.QueueFree();
			}
		}

		GetTree().ReloadCurrentScene();
	}
}
