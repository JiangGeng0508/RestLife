using Godot;
using System;

public partial class NeoCauldron : Window
{
	public override void _Ready()
	{
		CloseRequested += Hide;
		AddChild(new DragSlotButton(){ Position = new Vector2(10,0) });
		AddChild(new DragSlotButton(){ Position = new Vector2(60,0) });
		AddChild(new DragSlotButton(){ Position = new Vector2(110,0) });
	}
}
