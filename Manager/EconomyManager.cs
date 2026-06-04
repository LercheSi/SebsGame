using Godot;
using System;

public partial class EconomyManager : Node2D
{
	public float _Life { get;  set; } = 0;
	public float _Pressure { get;  set; } = 0;
	public float _Iron { get;  set; } = 0;
	public float _Coal { get;  set; } = 0;
	public float _Water { get;  set; } = 0;
	public float _ResearchPoints { get;  set; } = 0;
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}
}
