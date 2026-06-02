using Godot;

public partial class Camera : Camera2D
{
    [Export]
    public float Speed = 500f;

    public override void _Ready()
    {
        SetProcess(true);
        GD.Print("Camera Ready");
    }

    public override void _Process(double delta)
    {
        Vector2 direction = Vector2.Zero;

        if (Input.IsActionPressed("ui_right"))
            direction.X += 1;
        if (Input.IsActionPressed("ui_left"))
            direction.X -= 1;
        if (Input.IsActionPressed("ui_down"))
            direction.Y += 1;
        if (Input.IsActionPressed("ui_up"))
            direction.Y -= 1;

        Position += direction.Normalized() * Speed * (float)delta;
    }
}