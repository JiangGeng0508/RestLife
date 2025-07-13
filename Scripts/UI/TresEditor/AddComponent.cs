using Godot;
using System.Linq;

public partial class AddComponent : MenuButton
{
	[Export]
	public PackedScene[] ComponentScene { get; set; }
	private Texture2D _defaultIcon = GD.Load<Texture2D>("res://Asset/Sprite/Icon/node/icon_area_meteo.png");
	public override void _Ready()
	{
		var id = 0;
		foreach (PackedScene scene in ComponentScene)
		{
			GetPopup().AddItem(scene.ResourcePath.Split('/').Last<string>(), id);
			GetPopup().SetItemIcon(id, _defaultIcon);
			GetPopup().IdPressed += (index) =>
			{
				var component = ComponentScene[index].Instantiate();
				AddSibling(component);
			};
			id++;
		}
	}
}
