using Godot;
using System;

public interface IEvent
{
	void Trigger()//调用事件
	{

	}
	void Vanish()//失败时销毁事件
	{

	}
}
public class Condition
{
	public virtual bool Check()
	{
		return true;
	}
}
public class Quest : IEvent
{
	public Condition TriggerCondition { get; set; }
	public Condition VanishCondition { get; set; }
	public void Check()
	{
		
	}
	public virtual void Trigger() { }
	public virtual void Vanish() { }
}
public partial  class AutoActivateEvent : Node, IEvent
{
	public string EventAlias { get; set; } = "Defalut Event";
	public override void _Ready()
	{
		
	}
	public void Trigger()
	{
		Godot.GD.Print("Triggered event: " + EventAlias);
	}
	public void Vanish()
	{
		Godot.GD.Print("Vanished event: " + EventAlias);
	}
}