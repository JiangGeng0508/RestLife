using Godot;
using System;

public partial class CreativeItemList : ItemList
{
	public ItemSelectButton GetButton;
	public override void _Ready()
	{
		foreach (Item item in ItemManager.RegisteredItems)
		{
			AddItem(item.Name, item.Icon);
		}
		Hide();
	}
	public void ItemSelectButtonPressed(ItemSelectButton button)
	{
		Show();
		GetButton = button;
	}
}
