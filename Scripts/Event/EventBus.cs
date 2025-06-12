using System;
using System.Collections.Generic;
using Godot;

public partial class EventBus : Node
{
	public List<IEvent> RegisteredEvents = [];
	public Action DayTimeTrigger;
	//检测事件以及属性变化
	public override void _Ready()
	{
		Global.EventBus = this;
		Global.GameWorldTime.OnDayChange += (int days) => { DayTimeTrigger(); };
		DebugRegister();
	}
	public override void _PhysicsProcess(double delta)
	{

	}
	public void Register(Quest quest)
	{
		RegisteredEvents.Add(quest);
	}
	public void Unregister(Quest quest)
	{
		RegisteredEvents.Remove(quest);
		Global.GameWorldTime.OnDayChange -= quest.TriggerCondition.CheckHandler;
	}
	//debug
	public void DebugRegister(int d = 5)
	{
		var q = new QuestTimeTrigger("Day",">",d);
		q.Name = d + " Days";
		Register(q);
	}
}
