using Godot;
using System;

public partial class CreativeItemList : ItemList
{
	public ItemSelectButton ItemButton;
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
		GD.Print($"{button.Name} select button pressed");
		ItemButton = button;
	}
	public void OnItemSelected(int index)
	{
		GD.Print("Item selected: " + GetItemText(index));
		ItemButton.Item = ItemManager.ItemDict[GetItemText(index)];
		Hide();
	}
}
