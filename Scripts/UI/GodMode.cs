using Godot;
using System;

public partial class GodMode : VBoxContainer
{
	[Export]
	public bool isDebug { get; set; } = true;

	public override void _Ready()
	{
		Position = new Vector2(10, 10);
		Size = new Vector2(200, 100);
		if (!isDebug) return;
		AddDebugSelection("DebugPrint", () => { GD.Print("Debug"); });
		AddDebugSelection("NextDay", () => { Global.GameWorldTime.Days++; });
		AddDebugSelection("Pause", () => { Engine.TimeScale = 0; });
		AddDebugSelection("Unpause", () => { Engine.TimeScale = 1; });
	}
	public void AddDebugSelection(string name, Action action)
	{
		Button button = new() { Text = name };
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
