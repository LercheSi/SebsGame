

using Godot;
using System;



public partial class MeinRendering : Node2D
{


	[Export] public int MapWidth =  map.GetLength(0);
	[Export] public int MapHeight = map.GetLength(1);
	[Export] public int TileWidth = 64;
	[Export] public int TileHeight = 32;
	[Export] public int ElevationStep = 0;
	public enum TileType
	{
		Dirt = 1,
		Grass = 2,
		Way = 3,
		Start = 4,
		Finish = 5
	} 


	private static TileType[,] map =
		{
		{TileType.Grass, 	TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Way, TileType.Finish},
		{TileType.Grass, 	TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Way, TileType.Way, TileType.Grass},
		{TileType.Grass, 	TileType.Dirt, TileType.Grass, TileType.Grass, TileType.Way, TileType.Way, TileType.Dirt, TileType.Grass},
		{TileType.Grass, 	TileType.Dirt, TileType.Grass, TileType.Way, TileType.Way, TileType.Grass, TileType.Dirt, TileType.Grass},
		{TileType.Grass, 	TileType.Dirt, TileType.Way, TileType.Way, TileType.Grass, TileType.Grass, TileType.Dirt, TileType.Grass},
		{TileType.Way, 		TileType.Way, TileType.Way, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Dirt, TileType.Grass},
		{TileType.Way, 		TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Dirt, TileType.Grass},
		{TileType.Start, 	TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass, TileType.Grass},
	};
	private struct TileData
	{
		public int Height;
		public TileType Type;
	}
	
	private TileData[,] tileMap = new TileData[0,0];
    // Member variables here, example:
    private int _a = 2;
    private string _b = "textvar";
	private void GenerateDemoMap()
	{
		tileMap = new TileData[MapHeight, MapWidth];
		for (int y = 0; y < MapHeight; y++)
		{
			for (int x = 0; x < MapWidth; x++)
			{
				bool border = x == 0 || y == 0 || x == MapWidth - 1 || y == MapHeight - 1;
				int hill = (int)((Mathf.Sin(x * 0.55f) + Mathf.Cos(y * 0.45f)) * 1.5f);
				tileMap[y, x] = new TileData
				{
					Height = border ? 0 : System.Math.Max(hill, 0),
					Type = map[y, x]
				};
			}
		}
	}
	private Vector2 MapToScreen(int mapX, int mapY, int height)
	{
		float isoX = (mapX - mapY) * TileWidth * 0.5f;
		float isoY = (mapX + mapY) * TileHeight * 0.5f;
		return new Vector2(isoX, isoY - height * ElevationStep);
	}



	private Color TileColor(TileType tileType)
	{
		return tileType switch
		{
			TileType.Dirt => new Color("#8d6e63"),
			TileType.Grass => new Color("#3f7f5f"),
			TileType.Way => new Color("#6b1c1c"),
			TileType.Start => new Color("#b81616"),
			TileType.Finish => new Color("#05e24b"),
			_ => new Color("#4f9b67"),
		};
	}
	public Vector2 GetStartPosition()
	{
		for (int y = 0; y < MapHeight; y++)
		{
			for (int x = 0; x < MapWidth; x++)
			{
				if (tileMap[y, x].Type == TileType.Start)
					return MapToScreen(x, y, tileMap[y, x].Height);
			}
		}
		return Vector2.Zero;
	}

    public override void _Ready()
    {
        // Called every time the node is added to the scene.
        // Initialization here.
		GenerateDemoMap();
		QueueRedraw();
		
        GD.Print("Hello from C# to Godot :)");
    }

    public override void _Process(double delta)
    {
        // Called every frame. Delta is time since the last frame.
        // Update game logic here.
    }
	public override void _Draw()
	{
		if (tileMap.GetLength(0) == 0)
			return;

		Vector2 origin = new Vector2(GetViewportRect().Size.X * 0.5f, 80f);
		float halfW = TileWidth * 0.5f;
		float halfH = TileHeight * 0.5f;
		
		for (int y = 0; y < MapHeight; y++)
		{
			for (int x = 0; x < MapWidth; x++)
			{
				var tile = tileMap[y, x];
				Vector2 center = origin + MapToScreen(x, y, tile.Height);

				var top = new[] {
					center + new Vector2(0f, -halfH),
					center + new Vector2(halfW, 0f),
					center + new Vector2(0f, halfH),
					center + new Vector2(-halfW, 0f),
				};
				
				Color baseColor = TileColor(tile.Type);
				DrawColoredPolygon(top, baseColor);
				DrawPolyline(top, Colors.Black, 1.0f);
				
				if (tile.Height > 0)
				{
					Vector2 offset = new Vector2(0f, tile.Height * ElevationStep);
					var bottom = new[] {
						top[0] + offset,
						top[1] + offset,
						top[2] + offset,
						top[3] + offset,
					};

					for (int i = 0; i < 4; i++)
					{
						int next = (i + 1) % 4;
						var side = new[] { top[i], top[next], bottom[next], bottom[i] };
						Color sideColor = baseColor.Darkened(0.2f + i * 0.05f);
						DrawColoredPolygon(side, sideColor);
						DrawPolyline(bottom, new Color(0f,0f,0f,0.35f), 1.0f);
					}
				}
			}
		}
	}
}