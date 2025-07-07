using Godot;
using System;
public partial class DragSlotButton : TextureButton
{
	[Signal]
	public delegate void SlotClickedEventHandler(DragSlotButton slot);
	public void Init()
	{
		IgnoreTextureSize = true;
		StretchMode = StretchModeEnum.Scale;
		CustomMinimumSize = new Vector2(50, 50);
		TextureNormal = GD.Load<Texture2D>("res://Asset/Sprite/Icon/control/icon_file.png");
		Pressed += OnPressed;
		SlotClicked += Global.NeoInventory.SlotGrid.SlotClicked;
	}
	public void OnPressed()
	{
		EmitSignal(nameof(SlotClicked), this);
	}
	public DragSlotButton() { Init(); }
}
