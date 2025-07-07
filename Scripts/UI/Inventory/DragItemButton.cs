using Godot;
using System;

public partial class DragItemButton : TextureButton
{
	[Signal]
	public delegate void ItemClickedEventHandler(DragItemButton node);
	[Export]
	public Item Item { get; set; } = null;

	public void Init()
	{
		IgnoreTextureSize = true;
		StretchMode = StretchModeEnum.Scale;
		ActionMode = ActionModeEnum.Press;
		CustomMinimumSize = new Vector2(50, 50);
		if (Item == null) return;
		TextureNormal = Item.Icon;
		Pressed += OnPressed;
		ItemClicked += Global.NeoInventory.SlotGrid.ItemClicked;
	}
	public void OnPressed()
	{
		EmitSignal(nameof(ItemClicked), this);
	}
	public DragItemButton() { Init(); }
	public DragItemButton(Item item)
	{
		Item = item;
		Init();
	}
}
