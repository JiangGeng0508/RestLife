// using Godot;
// using Godot.Collections;
// using System;

// public partial class DataLoader : Node
// {
// 	//data path: "res://Asset/Data/"
// 	ItemData itemData;
// 	public override void _Ready()
// 	{
// 		LoadData("res://Asset/Data/TestItemData.json");
// 	}
// 	public void LoadData(string path)
// 	{
// 		var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
// 		var json = new Json();
// 		var StringFile = file.GetAsText();
// 		var Error = json.Parse(StringFile,true);
// 		if (Error == Error.Ok)
// 		{
// 			var jsonData = json.Data.AsGodotDictionary();
// 			itemData = new ItemData(
// 				(string)jsonData["Name"],
// 				(string)jsonData["Description"],
// 				(string)jsonData["Icon"],
// 				(string)jsonData["Type"],
// 				(int)jsonData["Weight"],
// 				(int)jsonData["Value"],
// 				(string)jsonData["Rarity"]
// 			);
// 		}
// 		else
// 		{
// 			GD.Print("Error: " + json.GetErrorMessage() + " in " + json.GetErrorLine());
// 			return;
// 		}
// 	}
// 	public void SaveData(string path)
// 	{
// 		var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
// 		var data = Json.FromNative(itemData,true);
// 		var str = Json.Stringify(data,"",true,true);
// 		file.StoreString(str);
// 		file.Close();
// 	}
// }
