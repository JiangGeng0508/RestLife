using Godot;
using System;

public partial class AttributeOperator : HBoxContainer
{
	public string SelectedAttribute { get; set; }
	public MenuButton Attribute => GetNode<MenuButton>("Attribute");
	public MenuButton Operator => GetNode<MenuButton>("Operator");
	public override void _Ready()
	{
		Attribute.GetPopup().IdPressed += OnAttributePressed;
		Operator.GetPopup().IdPressed += OnOperatorPressed;
	}
	public void OnAttributePressed(long id)
	{
		Attribute.Text = Attribute.GetPopup().GetItemText((int)id);
		SelectedAttribute = Attribute.Text;
	}
	public void OnOperatorPressed(long id)
	{
		Operator.Text = Operator.GetPopup().GetItemText((int)id);
		Operator.Icon = Operator.GetPopup().GetItemIcon((int)id);
	}
}
