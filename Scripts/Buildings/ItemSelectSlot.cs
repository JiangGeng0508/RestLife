using Godot;
using Godot.Collections;
using System;

public partial class ItemSelectSlot : MenuButton
{
	private Item _settedItem = null;
	public Item SettedItem
	{
		get { return _settedItem; }
		set
		{
			_settedItem = value;
			if (value == null)
			{
				Icon = DefaultIcon;
			}
			else
			{
				Icon = value.Icon;
			}
		}
	}
	[Export]
	Texture2D DefaultIcon { get; set; }
	public void OnPressed()
	{
		var popup = GetPopup();
		if (SettedItem != null)
		{
			Global.Player.Inventory.AddItem(SettedItem, SettedItem.Number);
			SettedItem = null;
			popup.Hide();
			return;
		}
		var ID = 1;
		Dictionary<int, Item> items = [];
		popup.Clear();
		foreach (var item in Global.Player.Inventory.Items)
		{
			popup.AddItem(item.Value.Name + "(" + item.Value.Number + ")", ID);
			items[ID] = item.Value;
			ID++;
		}
		popup.IdPressed += (id) =>
		{
			Global.Player.Inventory.RemoveItem(items[(int)id], 1);
			SettedItem = items[(int)id];
			Icon = items[(int)id].Icon;
		};
	}
}
