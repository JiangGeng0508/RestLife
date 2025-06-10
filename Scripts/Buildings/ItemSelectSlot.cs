using Godot;
using Godot.Collections;
using System;

public partial class ItemSelectSlot : MenuButton
{
	public Item SettedItem;
	Texture2D DefaultIcon = GD.Load<Texture2D>("res://Asset/Sprite/Icon/control/icon_interrogation.png");
	public void OnPressed()
	{
		var popup = GetPopup();
		var ID = 1;
		Dictionary<int, Item> items = [];
		popup.Clear();
		popup.AddItem("None", 0);
		items[0] = new Item() { Name = "None" };
		foreach (var item in Global.Player.Inventory.Items)
		{
			popup.AddItem(item.Value.Name + "(" + item.Value.Number + ")", ID);
			items[ID] = item.Value;
			ID++;

		}
		popup.IdPressed += (id) =>
		{
			if (SettedItem != null)
			{
				Global.Player.Inventory.AddItem(SettedItem, SettedItem.Number);
			}
			if (id == 0)
			{
				SettedItem = null;
				Icon = DefaultIcon;
				return;
			}
			Global.Player.Inventory.RemoveItem(items[(int)id]);
			SettedItem = items[(int)id];
			Icon = items[(int)id].Icon;
		};
	}
}
