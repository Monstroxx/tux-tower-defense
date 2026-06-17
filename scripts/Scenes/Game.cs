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



	private static int coins = 500; // TODO: Set coins back to 10 or 20
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
		}
	}

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
	public Ui Ui;

	[Export]
	public Node2D TowerNode;

	[Export]
	public VBoxContainer TowerSelection;

	[Export]
	public VBoxContainer endscreen;

	private float maxEnemyCount;
	private float frequency;

	private readonly List<Enemy> enemies = [];

	private readonly Timer enemieSpawnTimer = new();

	public override void _Ready()
	{
		maxEnemyCount = EnemysInFirstRound;
		frequency = StartFrequency;

		TowerSelection.Hide();
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
	}

	public void SpawnEnemy()
	{
		GD.Print($"Enemie count: {Path.GetChildren().Count}");
		if (!IsPlaying) return;

		Random rn = new();
		var enemy = possibleEnemies[rn.NextInt64(0,possibleEnemies.Length - 1)].Instantiate();
		Path.AddChild(enemy);

		enemieSpawnTimer.WaitTime *= 0.98;
	}

	public void EnemieFinished(Enemy enemie)
	{
		GD.Print("Enemie got through");
		if (!IsPlaying) return;

		Path.RemoveChild(enemie);
		enemie.QueueFree();
		Health -= 15;

		if (Health <= 0)
		{
			IsPlaying = false;
			GetTree().CallDeferred(SceneTree.MethodName.ChangeSceneToFile, "res://scenes/main_menue.tscn");
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
			var tower = ubuntuTowerScene.Instantiate<UbuntuTower>();
			_on_tower_button_pressed(tower);
		}
	}

	public void _on_fedora_tower_button_pressed()
	{
		if (Coins >= 10)
		{
			var tower = fedoraTowerScene.Instantiate<FedoraTower>();
			_on_tower_button_pressed(tower);
		}
	}
	
	//überladen
	public void _on_tower_button_pressed(UbuntuTower ubuntuTower)
	{
		AddChild(ubuntuTower);
		currentDragingTower = ubuntuTower;
		Coins -= 10;
	}

	public void _on_tower_button_pressed(FedoraTower fedoraTower)
	{
		AddChild(fedoraTower);
		currentDragingTower = fedoraTower;
		Coins -= 10;
	}
}
