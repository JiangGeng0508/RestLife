using System;
using Godot;
using System.Collections.Generic;

public static class RecipeManager
{
	public static List<Recipe> RegisteredRecipes = new List<Recipe>();

	public static void Init()
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