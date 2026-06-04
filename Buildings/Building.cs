using Godot;
using System;

public partial class Building : Node2D
{
	public Vector2I TilePosition { get; set; }
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		AddToGroup("Buildings");
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public void Place(TileMapLayer tileMapLayer = null,Vector2I vector2I = default(Vector2I), Node enemies = null)
    {
        // Any initialization logic for the Goblin can go here
        Position = tileMapLayer.MapToLocal(vector2I);
		
        //UpdateAnimation("idle");
    }
}
