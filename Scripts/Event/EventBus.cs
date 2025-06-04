using System.Collections.Generic;
using Godot;

//AutoLoad Script
public partial class EventBus : Node
{
	public List<IEvent> RegisteredEvents = [];

	//检测事件以及属性变化
	public override void _Ready()
	{
		Global.EventBus = this;
		Global.GameWorldTime.OnDayChange += DayTimeTrigger;
	}
	public override void _PhysicsProcess(double delta)
	{

	}
	public void DayTimeTrigger(int days)
	{
		//TODO: 处理时间事件
		GD.Print("DayTimeTrigger days:" + days);
		foreach (var e in RegisteredEvents)
		{
			e.Check();
		}
	}
	public int Register(Quest quest)
	{
		RegisteredEvents.Add(quest);
		GD.Print("Register Event" + quest.ToString());
		GD.Print("RegisteredEvents Count:" + RegisteredEvents.Count);
		return 0;
	}
	//debug
	public void DebugRegister()
	{
		var q = new Quest()
		{
			TriggerCondition = new Condition(ConditionType.Time, "Day", ConditionOperator.Equal, 3f)
		};
		q.TriggerCondition.Meet = () =>
				{
					q.Trigger();
				};
		Register(q);
	}
}
