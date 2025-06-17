using Godot;
using Godot.Collections;
using System;

public partial class FileBrowser : ItemList
{
	public Dictionary<long, string> DictTree = [];
	public override void _Ready()
	{
		OpenFolder("res://Asset/Sprite/");
		ItemSelected += (index) =>
		{
			OpenFolder(DictTree[index]);
		};
	}
	public void OpenFolder(string path)
	{
		Clear();
		DictTree.Clear();
		using var DirAc = DirAccess.Open(path);
		if (DirAc.GetCurrentDir() != "res://Asset/")
		{
			AddItem("..");
		}
		var dirs = DirAc.GetCurrentDir().Split("/");
		DictTree[0] = DirAc.GetCurrentDir().Replace(dirs[^1], "");
		foreach (var dict in DirAc.GetDirectories())
		{
			AddItem(dict);
			DictTree[DictTree.Count] = path + "/" + dict;
		}
	}
}
