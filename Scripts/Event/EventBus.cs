using System;
using Godot;

//AutoLoad Script
public partial class EventBus : Node
{
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
		GD.Print("DayTimeTrigger" + days);
	}
}