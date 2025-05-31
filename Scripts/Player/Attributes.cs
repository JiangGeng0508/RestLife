using Godot;
using System;
using System.Linq;

public partial class Attributes : Node2D
{
	public Vector3[] AttributesBonus;
	/*
		-X  Attributes
		0 - Energy

		-Y  State
		0 - Idle
		1 - Moving
		2 - Riding
		3 - Waiting

		-Z  Bonus Value
	*/
	public float Energy = 100f;

	public Attributes()
	{
		AttributesBonus = new Vector3[4];
		//Energy
		AttributesBonus.Append(new Vector3(0f, 0f, 0f)); // Idle
		AttributesBonus.Append(new Vector3(0f, 0f, 0.1f)); // Moving
		AttributesBonus.Append(new Vector3(0f, 0f, 0f)); // Riding
		AttributesBonus.Append(new Vector3(0f, 0f, 0f)); // Waiting
		
	}
}
