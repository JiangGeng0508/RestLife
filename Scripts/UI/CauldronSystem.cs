using Godot;

public partial class CauldronSystem : Control
{
	ItemSelectSlot Slot1;
	ItemSelectSlot Slot2;
	ItemSelectSlot Slot3;
	public override void _Ready()
	{
		Slot1 = GetNode<ItemSelectSlot>("ItemSelectSlot1");
		Slot2 = GetNode<ItemSelectSlot>("ItemSelectSlot2");
		Slot3 = GetNode<ItemSelectSlot>("ItemSelectSlot3");

		// Recipe recipe1 = new Recipe();
		// recipe1.Name = "Recipe1";
		// recipe1.Ingredients = [Global.ItemManager.RegisteredItems[0] , new Item(), new Item()];
		// recipe1.Result = new Item();
		// RecipeManager.RegisterRecipe(recipe1);
	}
	public void CheckCraft()
	{
		foreach (var recipe in RecipeManager.RegisteredRecipes)
		{
			if (recipe.Ingredients.Length == 1)
			{
				if (recipe.Ingredients[0].Name == Slot1.SettedItem.Name)
				{
					Slot1.SettedItem = recipe.Result;
					return;
				}
			}
		}
	}
}
