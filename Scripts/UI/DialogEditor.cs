using Godot;

public partial class DialogEditor : GraphEdit
{
	[Export]
	public Dialog[] StartDialogPaths { get; set; } = [];
	public override void _Ready()
	{
		OS.LowProcessorUsageMode = true;
		GetNode<Button>("New").Pressed += () =>
		{
			AddChild(GD.Load<PackedScene>("res://Scenes/UI/DialogGraph.tscn").Instantiate()); 
		};
		GetNode<Button>("Update").Pressed += () => { ArrangeNodes(); };
		GetNode<Button>("Refresh").Pressed += () => { GetTree().ReloadCurrentScene(); };
		GetNode<Button>("Save").Pressed += () =>
		{
			foreach (var node in GetChildren())
			{
				if (node is DialogGraph graph)
				{
					graph.ExportDialog();
				}
			}
		};
		ConnectionRequest += (fromNode, fromPort, toNode, toPort) =>
		{
			GD.Print("Connect " + fromNode + " " + fromPort + " to " + toNode + " " + toPort);
			var From = GetNode<DialogGraph>((string)fromNode);
			var To = GetNode<DialogGraph>((string)toNode);
			if (IsNodeConnected(fromNode, (int)fromPort, toNode, (int)toPort))
			{
				DisconnectNode(fromNode, (int)fromPort, toNode, (int)toPort);
				if (From.ConfirmDialog == To.dialog)
				{
					From.ConfirmDialog = null;
				}
				if (From.CancelDialog == To.dialog)
				{
					From.CancelDialog = null;
				}
			}
			else
			{
				ConnectNode(fromNode, (int)fromPort, toNode, (int)toPort, true);

				var export = To.ExportDialog();
				if (export == null) { return; }
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
		LoadDialogs(StartDialogPaths);
		ArrangeNodes();
	}
	public void LoadDialogs(Dialog[] dialogPaths)
	{
		foreach (var dialogPath in dialogPaths)
		{
			LoadDialog(dialogPath);
		}
	}
	public DialogGraph LoadDialog(Dialog dialog)
	{
		var node = GetNodeOrNull<DialogGraph>(dialog.Title);
		if (node != null)
		{
			return node;
		}
		var graph = GD.Load<PackedScene>("res://Scenes/UI/DialogGraph.tscn").Instantiate<DialogGraph>();
		graph.dialog = dialog;
		AddChild(graph);
		if (dialog.OKDialog != null)
		{
			var okGraph = LoadDialog(dialog.OKDialog);
			ConnectNode(graph.title.Text, 0, okGraph.title.Text, 0, true);
		}
		if (dialog.CancelDialog != null)
		{
			var cancelGraph = LoadDialog(dialog.CancelDialog);
			ConnectNode(graph.title.Text, 1, cancelGraph.title.Text, 0, true);
		}
		return graph;
	}
}
