using Godot;
using System;

public partial class Building : CharacterBody2D
{

	[Export]
    public PackedScene PS_Projectile;
	private int fire_range = 500;
	private float projectile_speed = 5f;	
	private const float ATTACK_INTERVAL = 1.0f;
	private float _attackTimer = 0f;
	private TileMapLayer _tileMap;
	private Node _enemies;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	_attackTimer += (float)delta;
		if (_attackTimer >= ATTACK_INTERVAL)
		{
			_attackTimer = 0f;
			AttackEnemies();
		}
	}

	public void Initialize(TileMapLayer tileMapLayer = null, Node enemies = null)
    {
        // Any initialization logic for the Goblin can go here
        _tileMap = tileMapLayer;
        _enemies = enemies;
        //UpdateAnimation("idle");
    }

	private void AttackEnemies()
	{
		
		foreach (Goblin enemy in _enemies.GetChildren())
		{
			if (Position.DistanceTo(enemy.Position) <= fire_range)
			{
				// Implement attack logic here, e.g., reduce enemy health
				GD.Print("Attacking enemy at position: " + enemy.Position);
				Projectile projectileInstance = PS_Projectile.Instantiate<Projectile>();
				projectileInstance.Initialize(enemy.Position - Position, projectile_speed);
				GetNode<Node2D>("Projectiles")
                   .AddChild(projectileInstance);
				
			}
		}
	}
}
