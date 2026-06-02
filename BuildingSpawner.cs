using Godot;
using System;

public partial class BuildingSpawner : Node
{
	[Export]
    public PackedScene building;

    [Export]
    public float SpawnInterval = 1.0f;

    private float _timer;
	private TileMapLayer _tileMap;
	public override void _Ready()
	{
		_timer = 0f;
		_tileMap = GetNode<TileMapLayer>("../TileMapLayer");
		SpawnBuilding();

	}
    public override void _Process(double delta)
    {
       
    }

    private void SpawnBuilding()
    {
        Building build = building.Instantiate<Building>();
		build.Initialize(_tileMap);
        build.Position = GetNode<Marker2D>("../BuildingMarker").Position;
		 
        GetParent().GetNode<Node2D>("Buildings")
                   .AddChild(build);
    }
}
