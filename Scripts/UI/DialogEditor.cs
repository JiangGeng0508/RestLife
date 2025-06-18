using Godot;

public partial class DialogEditor : GraphEdit
{
	public override void _Ready()
	{
		OS.LowProcessorUsageMode = true;
		GetNode<Button>("New").Pressed += SummonGraphNode;
		GetNode<Button>("Update").Pressed += () =>
		{
			ArrangeNodes();
		};
		ConnectionRequest += (fromNode, fromPort, toNode, toPort) =>
		{
			if (IsNodeConnected(fromNode, (int)fromPort, toNode, (int)toPort))
			{
				DisconnectNode(fromNode, (int)fromPort, toNode, (int)toPort);
			}
			else
			{
				ConnectNode(fromNode, (int)fromPort, toNode, (int)toPort, true);
				var From = GetNode<DialogGraph>((string)fromNode);
				var To = GetNode<DialogGraph>((string)toNode);
				var export = To.ExportDialog();
				GD.Print(fromPort);
				switch ((int)fromPort)
				{
					case 0:
						From.ConfirmDialog = GD.Load<Dialog>(export);
						break;
					case 1:
						From.CancelDialog = GD.Load<Dialog>(export);
						break;
					default:
						GD.PrintErr("Invalid port");
						break;
				}
				From.ExportDialog();
			}
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
