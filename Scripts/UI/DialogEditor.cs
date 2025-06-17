using Godot;
using Godot.Collections;

public partial class DialogEditor : GraphEdit
{
	public override void _Ready()
	{
		OS.LowProcessorUsageMode = true;
		GetNode<Button>("New").Pressed += SummonGraphNode;
		ConnectionRequest += (fromNode, fromPort, toNode, toPort) =>
		{
			GD.Print("Connection Request from " + fromNode + " to " + toNode + " fromPort " + fromPort + " to " + toPort );
			ConnectNode(fromNode, (int)fromPort, toNode, (int)toPort, true);
			GD.Print("Connections: " + Connections.Count);
		};
		ConnectionDragStarted += (Node, Port,KeepAlive) =>
		{
		};
	}
	public void SummonGraphNode()
	{
		var node = new GraphNode();
		AddChild(node);
		var grid = new GridContainer();
		grid.AddChild(new Label
		{
			Text = "New Node",
			Size = new Vector2(100, 20)
		});
		node.AddChild(grid);
		node.Title = "New Node";
		node.CustomMinimumSize = new Vector2(100, 100);
		node.SetSlotEnabledLeft(0, true);
		node.SetSlotEnabledRight(0, true);
		node.Dragged += (from, to) =>
		{
			GD.Print("Dragged " + from + " to " + to);
		};
	}
}
