using Godot;
using System;

public partial class Setting : VFlowContainer
{
	public override void _Ready()
	{
		GetNode<HSlider>("Volume/HSlider").ValueChanged += (value) =>
		{
			Global.MainScene.BgmPlayer.VolumeDb = (float)value - 50;
		};
	}
}
