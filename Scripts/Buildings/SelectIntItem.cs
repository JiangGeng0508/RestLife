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
		popup.PopupHide += Global.Player.Character.UpdataAnim;

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
		button.Pressed += func;
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
}
