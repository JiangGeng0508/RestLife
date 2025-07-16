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
		var count = 0;
		foreach (var file in files)
		{
			GD.Print("Loading Recipe: " + file);
			var recipe = GD.Load<Recipe>("res://Asset/Data/Recipes/" + file);
			RegisterRecipe(recipe);
			count++;
		}
		GD.Print("Loaded " + count + " recipes");
	}
	public static void RegisterRecipe(Recipe recipe)
	{
		RegisteredRecipes.Add(recipe);
	}
	public static Item CheckRecipe(Item[] ingredients)
	{
		foreach (var recipe in RegisteredRecipes)
		{
			bool match = true;
			Item[] StIngredients = recipe.Ingredients;
			for (int i = 0; i < 9; i++)
			{
				if (StIngredients[i] != ingredients[i])
				{
					match = false;
					break;
				}
			}
			if (match)
			{
				return recipe.Result;
			}
		}
		GD.PrintErr("No recipe found for ingredients");
		return null;
	}
}
