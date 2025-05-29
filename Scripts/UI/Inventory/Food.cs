using System;
using Godot;

public partial class Food : Item
{

	public void Consume()
	{
		AddNumber(-1);
	}

	public override void Init()
	{
		AddAction(Consume);
    }

}