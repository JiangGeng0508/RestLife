// using System;
// using Godot;

// public enum ItemTypeEnum
// {
// 	Tool,
// 	Raw,
// 	Material,
// 	Prop,
// 	Misc
// }
// public enum RarityEnum
// {
// 	Common,
// 	Rare
// }
// public partial class ItemData : Json
// {
// 	public static string[] CustomAttributes = ["Name", "Description", "Icon", "Type", "Weight", "Value", "Rarity"];
// 	public string Name { get; set; } = "Default Name";
// 	public string Description { get; set; } = "Default Description";
// 	public string Icon { get; set; } = "res://icon.svg";
// 	private ItemTypeEnum Type { get; set; } = ItemTypeEnum.Misc;
// 	public int Weight { get; set; } = 1;
// 	public int Value { get; set; } = 1;
// 	public RarityEnum Rarity { get; set; } = RarityEnum.Common;
// 	public ItemData() { }
// 	public ItemData(string name, string description, string icon, string type, int weight, int value, string rarity)
// 	{
// 		Name = name;
// 		Description = description;
// 		Icon = icon;
// 		Type = (ItemTypeEnum)Enum.Parse(typeof(ItemTypeEnum), type);
// 		Weight = weight;
// 		Value = value;
// 		Rarity = (RarityEnum)Enum.Parse(typeof(RarityEnum), rarity);
// 	}
// }