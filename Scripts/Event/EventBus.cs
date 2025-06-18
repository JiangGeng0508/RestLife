using System;
using System.Collections.Generic;
using Godot;

public static class EventBus
{
	public static List<IEvent> RegisteredEvents = new List<IEvent>();
	public static Action<string, int> TimeChangeTrigger;
	public static Action<Attribute> AttributeChangeTrigger;
	//检测事件以及属性变化
	public static void Init()
	{
		Global.GameWorldTime.OnDayChange += days => { TimeChangeTrigger?.Invoke("Day", days); };
		DebugRegister();
	}
	public static void Register(IEvent e)
	{
		RegisteredEvents.Add(e);
	}
	public static void Unregister(Quest quest)
	{
		RegisteredEvents.Remove(quest);
		Global.GameWorldTime.OnDayChange -= quest.TriggerCondition.CheckHandler;
	}
	//debug
	public static void DebugRegister(int d = 5)
	{
		var q = new QuestTimeTrigger("Day",">",d);
		q.Name = d + " Days";
		Register(q);
	}
}
