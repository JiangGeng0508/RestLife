using Godot;
using System;

public interface IEvent
{
	void Check();
	void Trigger();//调用事件
	void Vanish();//失败时销毁事件
}

public class Quest : IEvent
{
	public Condition TriggerCondition { get; set; }
	public Condition VanishCondition { get; set; }
	public void Check()
	{
		GD.Print("Checking event");
		if (TriggerCondition.Check() == true)
			Trigger();
		// if (VanishCondition.Check() == true)
		// 	Vanish();
	}
	public virtual void Trigger()
	{
		GD.Print("void trigger");
	}
	public virtual void Vanish() { }
	public Quest()
	{
		TriggerCondition = new Condition()
		{
			Meet = () =>
			{
				Trigger();
			}
		};
	}
}
public partial  class AutoActivateEvent : Node, IEvent
{
	public string EventAlias { get; set; } = "Defalut Event";
	public override void _Ready()
	{
		
	}
	public void Check()
	{

	}
	public void Trigger()
	{
		GD.Print("Triggered event: " + EventAlias);
	}
	public void Vanish()
	{
		GD.Print("Vanished event: " + EventAlias);
	}
}
