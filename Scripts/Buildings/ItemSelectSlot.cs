using Godot;
using System;

public partial class ItemSelectSlot : MenuButton
{
	public void OnPressed()
	{
		var popup = GetPopup();
		popup.Clear();
		foreach (var item in Global.Player.Inventory.Items)
		{
			popup.AddItem(item.Value.Name);
		}
	}
}
