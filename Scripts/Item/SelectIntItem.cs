using Godot;
using System;

public partial class SelectIntItem : InteractableItem
{

	PopupMenu popup;
	GridContainer grid;

	public override void Init()
	{
		popup = new PopupMenu();
		grid = new GridContainer();
		grid.Columns = 1;
		popup.Position = new Vector2I(600, 300);
		AddChild(popup);
		popup.AddChild(grid);

		//debug
		MergeVoidFunc(AddTestFood);

		OnBegin();
	}
	public override void Action()
	{
		popup.Show();
	}
	public void MergeVoidFunc(Action func)
	{
		Button button = new Button()
		{
			Text = func.Method.Name,
			ActionMode = BaseButton.ActionModeEnum.Press,
		};
		button.Connect("pressed", new Callable(this, func.Method.Name));
		grid.AddChild(button);
	}
	public void DelFunc(Action func)
	{
		foreach (Button button in grid.GetChildren())
		{
			if (button.Text == func.Method.Name)
			{
				grid.RemoveChild(button);
				break;
			}
		}
	}
	public virtual void OnBegin() { }

	//debug
	public void AddTestFood()
	{
		var item3 = new Food();
		item3.Name = "Food1";
		Inventory.AddItem(item3);
	}
}
