using Godot;
using System;

public partial class Hud : CanvasLayer
{
    public Control GameWorldInfo;
	public ProgressBar EnergyBar;
	public ProgressBar HungryBar;

	public override void _Ready()
	{
		GameWorldInfo = GetNode<Control>("GameWorldInfo");

		EnergyBar = GetNode<ProgressBar>("PlayerState/EnergyBar");
		EnergyBar.MaxValue = 100f;
		HungryBar = GetNode<ProgressBar>("PlayerState/HungryBar");
		HungryBar.MaxValue = 100f;
		
	}

    public override void _Process(double delta)
    {
        EnergyBar.Value = Global.Player.Character.Energy.Value;
        HungryBar.Value = Global.Player.Character.Hunger.Value;

        string formattedTime = $"Day: {Global.GameWorldTime.Days} Time: {Global.GameWorldTime.Hours:D2}:{Global.GameWorldTime.Minutes:00.}";
        GameWorldInfo.GetNode<Label>("Time").Text = formattedTime;
    }
}
