using Godot;
using System;

public partial class SlotGrid : GridContainer
{
	Control pickedNode;
	Vector2 PrevPosition;
	public override void _Ready()
	{
		GetTree().CreateTimer(0.5f).Timeout += () =>
		{
			var slot = new DragSlotButton();
			AddChild(slot);
			slot.AddChild(new DragItemButton(ItemManager.ItemDict["Apple"]));
			for (int i = 0; i < 29; i++)
				AddChild(new DragSlotButton());
		};
	}
	public void ItemClicked(DragItemButton item)//<-DragItemButton.OnPressed()
	{
		if (pickedNode == null)
		{
			pickedNode = item;
			PrevPosition = pickedNode.GlobalPosition;
			GD.Print("ItemPicked");
		}
	}
	public void SlotClicked(DragSlotButton slot)
	{
		if (pickedNode != null && slot.GetChildCount() < 1)
		{
			pickedNode.GetParent().RemoveChild(pickedNode);
			slot.AddChild(pickedNode);
			pickedNode.Position = Vector2.Zero;
			pickedNode = null;
			GD.Print("ItemPlaced");
		}
	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseMotion)
		{
			if (pickedNode != null)
			{
				pickedNode.GlobalPosition = GetViewport().GetMousePosition() + new Vector2(10, 10);
			}
		}
		if (@event is InputEventMouseButton mouseButton)
		{
			if (mouseButton.IsPressed() && mouseButton.ButtonIndex == MouseButton.Right)
			{
				if (pickedNode != null)
				{
					pickedNode.GlobalPosition = PrevPosition;
					pickedNode = null;
				}
			}
		}
	}
	public void AddItem(Item item)
	{
		foreach (DragSlotButton slot in GetChildren())
		{
			if (slot.GetChildCount() < 1)
			{
				slot.AddChild(new DragItemButton(item));
				return;
			}
		}
	}
}
