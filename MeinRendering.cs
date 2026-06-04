

using Godot;
using System;



public partial class MeinRendering : Node2D
{
    // Declare member variables here. Examples:
	// private int a = 2;
	// private string b = "text";
	private EconomyManager _economyManager;
	private Label _LifeLabel;
	private Label _PressureLabel;
	private Label _IronLabel;
	private Label _CoalLabel;
	private Label _WaterLabel;
	private Label _ResearchPointsLabel;


	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		_economyManager = GetNode<EconomyManager>("EconomyManager");
		_LifeLabel = GetNode<Label>("UI/StatusBox/LifeLabel");
		
		GD.Print(_LifeLabel.Text);
		_PressureLabel = GetNode<Label>("UI/StatusBox/PressureLabel");
		_IronLabel = GetNode<Label>("UI/StatusBox/IronLabel");
		_CoalLabel = GetNode<Label>("UI/StatusBox/CoalLabel");
		_WaterLabel = GetNode<Label>("UI/StatusBox/WaterLabel");
		_ResearchPointsLabel = GetNode<Label>("UI/StatusBox/ResearchPointsLabel");
		_LifeLabel.Text = $"Life: {_economyManager._Life}";
		_PressureLabel.Text = $"Pressure: {_economyManager._Pressure}";
		_IronLabel.Text = $"Iron: {_economyManager._Iron}";
		_CoalLabel.Text = $"Coal: {_economyManager._Coal}";
		_WaterLabel.Text = $"Water: {_economyManager._Water}";
		_ResearchPointsLabel.Text = $"Research Points: {_economyManager._ResearchPoints}";

	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		
	}

	

}