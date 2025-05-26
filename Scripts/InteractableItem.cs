using Godot;
using System;

public partial class InteractableItem : Area2D
{
	public bool Interactable = false;
	public Area2D CharaReachArea;
	public Label label;
	public override void _Ready()
	{
		Connect("area_entered", new Callable(this, nameof(onAreaEntered)));
		Connect("area_exited", new Callable(this, nameof(onAreaExited)));
		label = new Label();
		AddChild(label);
		label.Show();
		Init();
	}
	public override void _PhysicsProcess(double delta)
	{
		label.Text = "Interactable: " + Interactable;
	}

	public override void _MouseEnter()
	{
		//外边框发光效果
	}
	public override void _MouseExit()
	{

	}
	public override void _Input(InputEvent @event)
	{
		if (@event is InputEventMouseButton mouseEvent)
		{
			if (Interactable && mouseEvent.ButtonIndex == MouseButton.Left && mouseEvent.Pressed)
			{
				Action();
				// GD.Print("Action");
			}
		}
	}
	public void onAreaEntered(Area2D area)
	{
		if (area is ReachArea reachArea)
		{
			Interactable = true;
			CharaReachArea = area;
			// GD.Print("Enter ReachArea");
		}
	}
	public void onAreaExited(Area2D area)
	{
		if (area is ReachArea reachArea)
		{
			Interactable = false;
			CharaReachArea = null;
		}
	}
	public virtual void Init() { }
	public virtual void Action() { }
}
