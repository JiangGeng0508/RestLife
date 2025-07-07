using Godot;
using System;

public partial class NeoInventory : ScrollContainer
{
	public SlotGrid SlotGrid;
	public override void _Ready()
	{
		Global.NeoInventory = this;
		SlotGrid = GetNode<SlotGrid>("SlotGrid");
	}
}
