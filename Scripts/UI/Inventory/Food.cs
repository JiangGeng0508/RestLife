using System;
using Godot;

public partial class Food : Item
{

	public void Consume()
	{
		Number--;
	}

	public override void Init()
	{
		base.Init();
		AddAction(Consume);
    }

}