using System;
using Godot;

public partial class ServerPeer : Node
{
	[Export]
	public int Port { get; set; } = 25565;
	public void CreateServer()
	{
		var server = new ENetMultiplayerPeer();
		server.CreateServer(Port);
		Multiplayer.MultiplayerPeer = server;
		StartServer();
	}
	[Rpc(MultiplayerApi.RpcMode.AnyPeer)]
	public void StartServer()
	{
		GD.Print(Multiplayer.GetUniqueId());
	}
}