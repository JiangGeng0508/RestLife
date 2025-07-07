using Godot;
using System;

public partial class NeoCauldron : Window
{
	public override void _Ready()
	{
		CloseRequested += Hide;
		AddChild(new DragSlotButton());
		AddChild(new DragSlotButton());
		AddChild(new DragSlotButton());
	}
}
