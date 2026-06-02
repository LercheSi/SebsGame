using Godot;
using System;

public partial class Building : CharacterBody2D
{

	private TileMapLayer _tileMap;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Initialize(TileMapLayer tileMapLayer = null)
    {
        // Any initialization logic for the Goblin can go here
        _tileMap = tileMapLayer;
        //UpdateAnimation("idle");
    }
}
