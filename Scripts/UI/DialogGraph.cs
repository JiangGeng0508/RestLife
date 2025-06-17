using Godot;
using System;

[GlobalClass]
public partial class DialogGraph : GraphNode
{
	[Export]
	public Dialog dialog { get; set; }
	public override void _Ready()
	{

	}

}
