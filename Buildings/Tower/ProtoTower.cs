using Godot;
using System;

public partial class ProtoTower : Tower
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddToGroup("ProtoTowers");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{	
		_attackTimer += (float)delta;
		if (_attackTimer >= ATTACK_INTERVAL)
		{
			_attackTimer = 0f;
			GD.Print("ProtoTower Attacks!");
		}
	}



}
