using Godot;
using Godot.Collections;
using System;

public partial class FileBrowser : ItemList
{
	[Signal]
	public delegate void ReadFolderEventHandler(string path);
	[Export]
	public string ReadPath { get; set; } = "res://Asset/";
	
	public string CurrentPath = "";
	public Dictionary<long, string> DictTree = [];
	public override void _Ready()
	{
		OpenFolder(ReadPath);
		ItemSelected += (index) =>
		{
			EmitSignal(nameof(ReadFolder), DictTree[index]);
			// GetNode<IconPreviewGrid>("../Scroll/IconPreviewGrid").ReadFolder(DictTree[index]);
			OpenFolder(DictTree[index]);
		};
	}
	public void OpenFolder(string path)
	{
		Clear();
		DictTree.Clear();
		
		using var DirAc = DirAccess.Open(path);
		var dirs = DirAc.GetCurrentDir().Split("/");
		if (path != ReadPath)
		{
			AddItem("..");
			DictTree[0] = DirAc.GetCurrentDir().Replace(dirs[^1], "");
		}
		foreach (var dict in DirAc.GetDirectories())
		{
			AddItem(dict);
			DictTree[DictTree.Count] = path + "/" + dict;
		}
	}
}
