using Godot;
using System;
using System.Linq;

public partial class NeoInventory : ScrollContainer
{
	public Item[] Items = new Item[30];
	public SlotGrid SlotGrid;
	public override void _Ready()
	{
		Global.NeoInventory = this;
		SlotGrid = GetNode<SlotGrid>("SlotGrid");
	}
	public void AddItem(Item item)
	{
		GD.Print(Items.Length);
		Items[Items.Length] = item;
	}
}
