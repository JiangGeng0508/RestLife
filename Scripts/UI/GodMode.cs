using Godot;
using System;

public partial class GodMode : ItemList
{
	[Export]
	public bool isDebug { get; set; } = false;

	public override void _Ready()
	{
		if (!isDebug) return;
		AddDebugSelection("DebugPrint", () => { GD.Print("Debug"); });
		AddDebugSelection("Pause", () => { GetTree().Paused = true; });
	}
	public void AddDebugSelection(string name, Action action)
	{
		
	}
}
