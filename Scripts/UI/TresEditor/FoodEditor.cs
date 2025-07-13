using Godot;
using System;

public partial class FoodEditor : ItemEditor
{
	public override void CheckItem()
	{
		ItemName = GetNode<LineEdit>("Name").Text;
		ItemIcon = GetNode<TextureRect>("Icon").Texture;
		if (ItemName.Length > 0 && ItemIcon != null)
		{
			var food = new Food()
			{
				Name = ItemName,
				Icon = ItemIcon,
				HungerBonus = (float)GetNode<SpinBox>("Hunger").Value,
				EnergyBonus = (float)GetNode<SpinBox>("Energy").Value,
			};
			var path = ExportPath + food.Name + ".tres";
			ResourceSaver.Save(food, path);
			GetNode<Label>("Warning").Text = "Food Created";
		}
		else
		{
			GetNode<Label>("Warning").Text = "Create Failed";
		}
	}
}
