using Godot;
using Godot.Collections;
using System;

public partial class DataLoader : Node
{
	//data path: "res://Asset/Data/"
	ItemData itemData;
	public override void _Ready()
	{
		LoadData("res://Asset/Data/TestItemData.tres");
		LoadData("res://Asset/Data/TestItemData.json");
	}
	public void LoadData(string path)
	{
		var file = FileAccess.Open(path, FileAccess.ModeFlags.Read);
		var json = new Json();
		var StringFile = file.GetAsText();
		var Error = json.Parse(StringFile);
		if (Error == Error.Ok)
		{
			var jsonData = Json.ToNative(json.Data, true);
		}
		else
		{
			GD.Print("Error: " + json.GetErrorMessage() + " in " + json.GetErrorLine());
		}
	}
	public void SaveData(string path)
	{
		var file = FileAccess.Open(path, FileAccess.ModeFlags.Write);
		var data = Json.FromNative(itemData,true);
		var str = Json.Stringify(data,"",true,true);
		file.StoreString(str);
		file.Close();
	}
		
}
