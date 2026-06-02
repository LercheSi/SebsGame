using Godot;
using System;

public partial class Projectile : CharacterBody2D
{
	private const float MOTION_SPEED = 20f; // Pixels/second
	private Vector2 direction;
	private Area2D _area ;
	private float _damage;
	// Called when the node enters the scene tree for the first time.
	public void Initialize(Vector2 direction, float velocity = 200f, float damage = 25f)
	{
		Velocity = direction.Normalized() * velocity;
		_damage = damage;
	}
	public override void _Ready()
	{
		_area = GetNode<Area2D>("CollisionArea");
		_area.BodyEntered += OnBodyEntered;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Position = Position + Velocity * MOTION_SPEED * (float)delta;

	}

	private void OnBodyEntered(Node2D body)
    {
        if (body is Goblin enemy)
        {
            enemy.TakeDamage(_damage);
            QueueFree();
        }
    }
}


