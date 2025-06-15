using Godot;
using System;

public partial class SelectMenu : TabContainer
{
	public override void _Ready()
	{
		Global.SelectMenu = this;
		Visible = false;
	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey && eventKey.Pressed)
		{
			if (eventKey.Keycode == Key.Tab)
			{
				Visible = !Visible;
				Global.Player.Character.OnToggleMenu();
			}
			else if (Visible && eventKey.Keycode == Key.Escape)
			{
				Visible = false;
				Global.Player.Character.OnToggleMenu();
			}
		}
	}
}
