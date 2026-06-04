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
	private Node _enemies;
	public override void _Ready()
	{
		_timer = 0f;
		_tileMap = GetNode<TileMapLayer>("../TileMapLayer");
		_enemies = GetNode<Node2D>("../Enemies");
		SpawnBuilding();

	}
    public override void _Process(double delta)
    {
       
    }

    private void SpawnBuilding()
    {
        Building build = building.Instantiate<ProtoTower>();
        Vector2I marker_position = _tileMap.LocalToMap(GetNode<Marker2D>("../BuildingMarker").Position); // Example position, you can set this as needed
		build.Place(_tileMap, marker_position, _enemies);
        
		 
        GetParent().GetNode<Node2D>("Buildings")
                   .AddChild(build);
    }
}
