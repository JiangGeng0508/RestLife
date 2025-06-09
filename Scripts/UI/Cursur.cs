using Godot;
using System;

public partial class Cursur : Area2D
{
	Label TooltipLabel;
	Area2D area2D;
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
	
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				if (area2D is InteractableItem interactableItem)
				{
					interactableItem.Click();
				}
			}
		}
	}
	public void OnAreaEntered(Area2D area)
	{
		if (area is ReachArea) return;
		area2D = area;
		SetTooltip(area.Name);
	}
	public void OnAreaExited(Area2D area)
	{
		if (area is ReachArea) return;
		area2D = null;
		SetTooltip(null);
	}
}
