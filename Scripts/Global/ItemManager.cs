using System;
using System.Collections.Generic;
using Godot;
using System.IO;

public partial class ItemManager : Node
{
	public List<Item> RegisteredItems { get; set; } = [];
	public override void _Ready()
	{
		try
		{
			if (Directory.Exists("res://Asset/Data/Items/"))
			{
				string[] files = Directory.GetFiles("res://Asset/Data/Items/", "*.tres", SearchOption.AllDirectories);
				foreach (string file in files)
				{

				}
			}
		}
		catch (Exception e)
		{
			GD.PrintErr(e.Message);
		}
	}
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