using System;
using System.Collections.Generic;
using Godot;

public class ItemManager
{
	public List<Item> RegisteredItems { get; set; } = [];
	public void Register(Item item)
	{
		RegisteredItems.Add(item);
	}
	public void Register(params Item[] items)
	{
		foreach (var item in items)
		{
			RegisteredItems.Add(item);
		}
	}
}