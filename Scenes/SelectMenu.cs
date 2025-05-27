using Godot;
using System;

public partial class SelectMenu : TabContainer
{
	public override void _Ready()
	{
		Visible = false;
	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Keycode == Key.Tab)
		{
			Visible = !Visible;
		}
	}
}
