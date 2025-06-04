using Godot;
using System;

public partial class MainCamera : Camera2D
{
    Character chara;
    float defaultZoom = 1.0f;
    float maxZoom = 0.5f; // 最大缩放比例
    float minZoom = 2.0f; // 最小缩放比例
    float zoomSpeed = 0.1f; // 缩放速度

    // 新增一个变量来存储目标缩放值
    Vector2 targetZoom;

    public override void _Ready()
    {
        chara = GetNode<Character>("../Character");
        targetZoom = Zoom; // 初始化目标缩放值为默认缩放值
    }

    public override void _PhysicsProcess(double delta)
    {
        // 计算角色与相机之间的距离
        float distance = Position.DistanceTo(chara.Position);

        // 跟踪角色移动
        if (distance > 10f)
        {
            Position += (chara.Position - Position) / 2 * 5 * (float)delta;
        }

        // 使用线性插值来平滑缩放
        Zoom = Zoom.Lerp(targetZoom, (float)delta * 5);
    }

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseButton wheelEvent)
        {
            // 根据滚轮的滚动方向调整目标缩放值
            if (wheelEvent.ButtonIndex == MouseButton.WheelUp)
            {
                targetZoom = new Vector2(Math.Min(targetZoom.X + zoomSpeed, minZoom), Math.Min(targetZoom.Y + zoomSpeed, minZoom));
            }
            else if (wheelEvent.ButtonIndex == MouseButton.WheelDown)
            {
                targetZoom = new Vector2(Math.Max(targetZoom.X - zoomSpeed, maxZoom), Math.Max(targetZoom.Y - zoomSpeed, maxZoom));
            }
        }
    }
}
