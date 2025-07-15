using Godot;
using System;

public partial class ItemSelectButton : Button
{
	public Button GetButton;
	public Item _item;
	[Export]
	public Texture2D DefaultIcon { get; set; } = GD.Load<Texture2D>("res://Asset/Sprite/Icon/control/icon_money_bag.png");
	[Signal]
	public delegate void ItemSelectButtonPressedEventHandler(ItemSelectButton button);
	public Item Item
	{
		get { return _item; }
		set
		{
			_item = value;
			if (_item != null)
			{
				Icon = _item.Icon;
			}
			else
			{
				Icon = DefaultIcon;
			}
		}
	}

	public override void _Ready()
	{
		GetButton = GetNode<Button>("Get");
	}
	public void OnItemSelectButtonPressed()
	{
		EmitSignal(nameof(ItemSelectButtonPressed), this);
	}
	public void OnGetButtonPressed()
	{
		
	}
}
