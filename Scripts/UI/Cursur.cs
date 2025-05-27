using Godot;
using System;

public partial class Cursur : Area2D
{
	Label TooltipLabel;
	string Tooltip = "Nothing yet";
	public override void _Ready()
	{
		TooltipLabel = GetNode<Label>("TooltipLabel");
		TooltipLabel.Show();
	}
	public override void _PhysicsProcess(double delta)
	{
		TooltipLabel.Text = Tooltip;
		Position = GetGlobalMousePosition();
	}
	public void SetTooltip(string tooltip)
	{
		Tooltip = tooltip;
	}
	public void OnAreaEntered(Area2D area)
	{
		SetTooltip(area.Name);
	}
	public void OnAreaExited(Area2D area)
	{
		SetTooltip(null);
	}
}
