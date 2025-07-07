using Godot;
using System;
using System.Linq;

public partial class NeoCauldron : Window
{
	private DragSlotButton Slot1;
	private DragSlotButton Slot2;
	private DragSlotButton Slot3;
	public override void _Ready()
	{
		CloseRequested += Hide;
		Slot1 = new DragSlotButton() { Position = new Vector2(10, 10) };
		Slot1.SlotClicked += (slot) => { Check(); };
		Slot2 = new DragSlotButton() { Position = new Vector2(60, 10) };
		Slot2.SlotClicked += (slot) => { Check(); };
		Slot3 = new DragSlotButton() { Position = new Vector2(110, 10) };
		Slot3.SlotClicked += (slot) => { Check(); };
		AddChild(Slot1);
		AddChild(Slot2);
		AddChild(Slot3);
	}
	public void Check()
	{
		PrintTree();
		var count = 0;
		foreach (var slot in GetChildren())
		{
			if (slot.GetChildCount() > 0)
			{
				count++;
			}
		}
		var item = new Item[count];
		foreach (var slot in GetChildren())
		{
			if (slot.GetChildCount() > 0)
			{
				count--;
				item[count] = slot.GetChild<DragItemButton>(0).Item;
				GD.Print(item[0]);
			}
		}
		GD.Print(count);
		var res = RecipeManager.CheckRecipe(item);
		if (res != null)
		{
			if (Slot1.GetChildCount() > 0)
				Slot1.RemoveChild(Slot1.GetChild<DragItemButton>(0));
			if (Slot2.GetChildCount() > 0)
				Slot2.RemoveChild(Slot2.GetChild<DragItemButton>(0));
			Slot3.AddChild(new DragItemButton(res));
		}
		else
		{
			GD.PrintErr("Recipe check failed.");
		}
	}
}
