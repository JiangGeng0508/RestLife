using Godot;
using System;

public partial class RecipeEditor : Control
{
	[Export]
	public string RecipeExportPath { get; set; } = "res://Asset/Data/Recipes/Export/";
	public void OnCraftButtonPressed()
	{
		var ingredients = new Item[9];
		for (int i = 0; i < 9; i++)
		{
			ingredients[i] = GetNode<ItemSelectButton>($"NineCraftGrid/ItemSelectButton{i + 1}").Item;
		}
		var result = GetNode<ItemSelectButton>("ItemSelectButton").Item;
		var recipe = new Recipe(ingredients, result);
		var fix = 0;
		var name = result.Name;
		while (!CheckRepeatRecipe(name))
		{
			fix++;
			name = (fix == 0)? result.Name : result.Name + fix.ToString();
		}
		var err = ResourceSaver.Save(recipe, $"{RecipeExportPath}{name}.tres", ResourceSaver.SaverFlags.Compress);
		if (err != Error.Ok)
		{
			GD.PrintErr("Failed to save recipe: " + err);
		}
		else
		{
			GD.Print("Recipe saved to: " + RecipeExportPath + name + ".tres");
		}
	}
	public bool CheckRepeatRecipe(string name)
	{
		using var dir = DirAccess.Open(RecipeExportPath);
		var files = dir.GetFiles();
		foreach (var file in files)
		{
			if (file.GetFile() == $"{name}.tres") return false;
		}
		return true;
	}
}
