using System;
using Godot;

public partial class Food : Item
{

	public void Consume()
	{
		AddNumber(-1);
		GD.Print($"Yum,yum,{Number} left");
	}

	public override void Init()
	{
		AddAction(Consume);
    }

}