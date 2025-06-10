using Godot;
using Godot.Collections;
using System;

public partial class ItemSelectSlot : MenuButton
{
	public Item SettedItem;
	public void OnPressed()
	{
		var popup = GetPopup();
		var ID = 1;
		Dictionary<int, Item> items = [];
		popup.Clear();
		popup.AddItem("None", 0);
		foreach (var item in Global.Player.Inventory.Items)
		{
			popup.AddItem(item.Value.Name, ID);
			items[ID] = item.Value;
			ID++;
			
		}
		popup.IdPressed += (id) =>
		{
			if (id == 0)
			{
				Global.Player.Inventory.AddItem(SettedItem);
				SettedItem = null;
				Icon = null;
				return;
			}
			Global.Player.Inventory.RemoveItem(items[(int)id], 1);
			SettedItem = items[(int)id];
			Icon = items[(int)id].Icon;
		};
	}
}
