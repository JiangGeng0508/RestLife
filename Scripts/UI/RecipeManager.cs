using System;
using Godot;
using System.Collections.Generic;

public partial class RecipeManager : Node
{
	public static List<Recipe> RegisteredRecipes = [];

	public override void _Ready()
	{
		using var dir = DirAccess.Open("res://Asset/Data/Recipes/");
		var files = dir.GetFiles();
		foreach (var file in files)
		{
			GD.Print("Loading Recipe: " + file);
			var recipe = GD.Load<Recipe>("res://Asset/Data/Recipes/" + file);
			RegisterRecipe(recipe);
		}
	}
	public static void RegisterRecipe(Recipe recipe)
	{
		RegisteredRecipes.Add(recipe);
	}
}