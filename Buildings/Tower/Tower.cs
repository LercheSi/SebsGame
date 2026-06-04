using Godot;
using System;

public partial class Tower : Building
{

	[Export]
    public PackedScene PS_Projectile;
	protected int fire_range = 500;
	protected float projectile_speed = 5f;	
	protected const float ATTACK_INTERVAL = 1.0f;
	protected float _attackTimer = 0f;
	protected TileMapLayer _tileMap;
	private Area2D _area;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddToGroup("Towers");
		_area = GetNode<Area2D>("CollisionArea");
		_area.BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	

	}

	private void OnBodyEntered(Node2D body)
    {
        if (body is Goblin enemy)
        {

        }
    }

	private void Attack()
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
