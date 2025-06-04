using System;
using System.Collections.Generic;
using Godot;

//AutoLoad Script
public partial class EventBus : Node
{
	public List<IEvent> RegisteredEvents = [];
	public Action DayTimeTrigger;
	//检测事件以及属性变化
	public override void _Ready()
	{
		Global.EventBus = this;
		Global.GameWorldTime.OnDayChange += (int days) => { DayTimeTrigger(); };
	}
	public override void _PhysicsProcess(double delta)
	{

	}
	public int Register(Quest quest)
	{
		RegisteredEvents.Add(quest);
		return 0;
	}
	//debug
	public void DebugRegister()
	{
		var q = new QuestTimeTrigger("Day",">",5);
		Register(q);
	}
}
