using Godot;
using System;
using System.Collections.Generic;

public partial class Goblin : CharacterBody2D
{
    private const float MOTION_SPEED = 2f; // Pixels/second

    private Vector2 lastDirection = new Vector2(1, 0);
    private AnimatedSprite2D sprite;
    private TileMapLayer _tileMap;
    private readonly Dictionary<string, (string name, bool flip)[]> animDirections = new()
    {
        {
            "idle",
            new (string, bool)[]
            {
                ("side_right_idle", false),
                ("45front_right_idle", false),
                ("front_idle", false),
                ("45front_left_idle", false),
                ("side_left_idle", false),
                ("45back_left_idle", false),
                ("back_idle", false),
                ("45back_right_idle", false),
            }
        },
        {
            "walk",
            new (string, bool)[]
            {
                ("side_right_walk", false),
                ("45front_right_walk", false),
                ("front_walk", false),
                ("45front_left_walk", false),
                ("side_left_walk", false),
                ("45back_left_walk", false),
                ("back_walk", false),
                ("45back_right_walk", false),
            }
        }
    };
    public void Initialize(TileMapLayer tileMapLayer = null)
    {
        // Any initialization logic for the Goblin can go here
        _tileMap = tileMapLayer;
        //UpdateAnimation("idle");
    }
    public override void _Ready()
    {
        sprite = GetNode<AnimatedSprite2D>("Sprite2D");
        UpdateAnimation("idle");
       // Vector2 new_Position = _tileMap.MapToLocal(new Vector2I(0, 0));
        //Position = new_Position;

    }
    ~Goblin()
    {
        GD.Print("Goblin instance is being destroyed.");
    }

    public override void _Process(double delta)
    {
        Random rand = new Random();
        //Vector2 targetPosition = new Vector2(rand.NextSingle(), rand.NextSingle()) * 400f - new Vector2(200f, 200f); // Random target within a 400x400 area centered on (0,0)
        Vector2I currentPosition = _tileMap.LocalToMap(Position);
        int[,] surroundingTiles =
        {
            { _tileMap.GetCellSourceId(currentPosition + new Vector2I(-1, -1)), _tileMap.GetCellSourceId(currentPosition + new Vector2I(0, -1)), _tileMap.GetCellSourceId(currentPosition + new Vector2I(1, -1)) },
            { _tileMap.GetCellSourceId(currentPosition + new Vector2I(-1, 0)), _tileMap.GetCellSourceId(currentPosition), _tileMap.GetCellSourceId(currentPosition + new Vector2I(1, 0)) },
            { _tileMap.GetCellSourceId(currentPosition + new Vector2I(-1, 1)), _tileMap.GetCellSourceId(currentPosition + new Vector2I(0, 1)), _tileMap.GetCellSourceId(currentPosition + new Vector2I(1, 1)) }
        };
        //GD.Print(_tileMap.GetCellSourceId(currentPosition));
        if (_tileMap.GetCellSourceId(currentPosition) == -1 || _tileMap.LocalToMap(Position) == new Vector2I(0, -8))
        {
           QueueFree();
       //     UpdateAnimation("idle");
      //      return; // Don't move if on a wall tile
        }
        GD.Print("Surrounding Tiles:");
        for (int y = 0; y < 3; y++)
        {
            string row = "";
            for (int x = 0; x < 3; x++)
            {
                row += surroundingTiles[y, x] + " ";
            }
            GD.Print(row);
            GD.Print("\n");
        }
        //if (surroundingTiles[1, 0] == 1) // Left
        //    lastDirection = _tileMap.MapToLocal(new Vector2I(-1, 0)) - _tileMap.MapToLocal(Vector2I.Zero);
        if (surroundingTiles[1, 2] == 1) // Right
            lastDirection = _tileMap.MapToLocal(new Vector2I(1, 0)) - _tileMap.MapToLocal(Vector2I.Zero);
        else if (surroundingTiles[0, 1] == 1) // Up
            lastDirection = _tileMap.MapToLocal(new Vector2I(0, -1))- _tileMap.MapToLocal(Vector2I.Zero);
        else if (surroundingTiles[2, 1] == 1) // Down
            lastDirection = _tileMap.MapToLocal(new Vector2I(0, 1))- _tileMap.MapToLocal(Vector2I.Zero);
        else
        {
            return;
        }
        //lastDirection = _tileMap.MapToLocal(new Vector2I(0, -1))- _tileMap.MapToLocal(Vector2I.Zero);
        UpdateAnimation("walk");
        Position += lastDirection * MOTION_SPEED * (float)delta;
        //Position = Position.MoveToward(
        //targetPosition,
        //10
    //);
    }

    private void UpdateAnimation(string animSet)
    {
        var angle = Mathf.RadToDeg(lastDirection.Angle()) + 22.5f;
        angle = (angle % 360f + 360f) % 360f; // normalize to [0,360)
        int sliceDir = (int)Mathf.Floor(angle / 45f);

        var pair = animDirections[animSet][sliceDir];
        sprite.Play(pair.name);
        sprite.FlipH = pair.flip;
    }
}
