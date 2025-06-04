using System;
using Godot;

public interface IEvent
{
	void Check();
	void Trigger();//调用事件
	// void Vanish();//失败时销毁事件
}

public partial class Quest : GodotObject,IEvent
{
	public Condition TriggerCondition { get; set; }
	// public Condition VanishCondition { get; set; }
	public void Check()
	{
		TriggerCondition.Check();
		// VanishCondition.Check();
	}
	public virtual void Trigger()
	{
		GD.Print("void trigger");
		Vanish();
	}
	public virtual void Vanish()
	{
		Global.EventBus.Unregister(this);
		TriggerCondition.Meet = null;
		Free();
	}
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
public partial class QuestTimeTrigger : Quest
{
	public QuestTimeTrigger(string Resolution,string Operator, float value)
	{
		var op = ConditionOperator.Equal;
		Global.EventBus.DayTimeTrigger += Check;
		switch (Operator)
		{
			case "==":
				op = ConditionOperator.Equal;
				break;
			case ">":
				op = ConditionOperator.GreaterThan;
				break;
			case "<":
				op = ConditionOperator.LessThan;
				break;
			case ">=":
				op = ConditionOperator.GreaterThan;
				break;
			case "<=":
				op = ConditionOperator.LessThan;
				break;
			case "!=":
				op = ConditionOperator.NotEqual;
				break;
			default:
				GD.Print($"Wrong Operator:{Operator}");
				break;
		}
		TriggerCondition = new Condition(ConditionType.Time, Resolution, op, value)
		{
			Meet = () =>
			{
				Trigger();
			}
		};
	}
}
