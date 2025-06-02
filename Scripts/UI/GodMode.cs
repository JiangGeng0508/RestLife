using Godot;
using System;

public partial class GodMode : VBoxContainer
{
	[Export]
	public bool isDebug { get; set; } = false;

	public override void _Ready()
	{
		if (!isDebug) return;
		AddDebugSelection("DebugPrint", () => { GD.Print("Debug"); });
		// AddDebugSelection("Pause", () => { GetTree().Paused = !GetTree().Paused; });
	}
	public void AddDebugSelection(string name, Action action)
	{
		Button button = new Button() { Text = name };
		button.Pressed += action;
		button.Position = new Vector2(10, 10 + GetChildCount() * 30);
		button.Size = new Vector2(100, 20);
		AddChild(button);
	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventKey eventKey && eventKey.Pressed && eventKey.Keycode == Key.F1)
		{
			if (isDebug)
			{
				Visible = !Visible;
			}
		}
		
	}
}
