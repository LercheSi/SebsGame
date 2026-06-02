using Godot;
using System;

public partial class EnemySpawner : Node
{
	[Export]
    public PackedScene goblin;

    [Export]
    public float SpawnInterval = 1.0f;

    private float _timer;
	private TileMapLayer _tileMap;
	public override void _Ready()
	{
		_timer = 0f;
		_tileMap = GetNode<TileMapLayer>("../TileMapLayer");

	}
    public override void _Process(double delta)
    {
       _timer += (float)delta;

        if (_timer >= SpawnInterval)
        {
            _timer = 0;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        Goblin enemy = goblin.Instantiate<Goblin>();
		enemy.Initialize(_tileMap);
        enemy.Position = GetNode<Marker2D>("../MarkerEnemies").Position;
		 
        GetParent().GetNode<Node2D>("Enemies")
                   .AddChild(enemy);
    }
}
