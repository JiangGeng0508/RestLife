using Godot;
using System;

public enum CharacterState
{
    Idle,
    Moving,      // 合并后的移动状态
    Riding,
    Waiting
}

public partial class Character : CharacterBody2D
{
    // 节点引用
    Area2D reachArea;
    TargetNotifer tarPosNotifer;
    InteractableItem interactItem;
    Label label; // debug
    
    // 角色属性
    public ulong Id = 0;
    bool handable = false;
    CharacterState state = CharacterState.Idle;
    Vector2 targetPosition = Vector2.Zero;
    CharacterState prevState = CharacterState.Idle;
    
    // 移动参数
    float speed = 10f;
    private Vector2 _prevPosition = Vector2.Zero;
    int KeyDirection = 0; // -1左, 0无, 1右

    public override void _Ready()
    {
        Id = GetInstanceId();
        reachArea = GetNode<Area2D>("ReachArea");
        tarPosNotifer = GetNode<TargetNotifer>("../UI/TargetNotifier");
        
        // 调试标签
        label = new Label();
        AddChild(label);
        label.Show();
        
        targetPosition = Position;
    }

    public override void _PhysicsProcess(double delta)
	{
		// 调试显示当前状态
		label.Text = $"{state}";

		// 统一移动处理
		if (state == CharacterState.Moving)
		{
			Vector2 moveDirection = Vector2.Zero;
			bool hasMovement = false;
			
			// 键盘移动
			if (KeyDirection != 0)
			{
				moveDirection = new Vector2(KeyDirection, 0);
				hasMovement = true;
			}
			// 鼠标点击移动（仅在无键盘输入时执行）
			else if ((targetPosition - Position).Length() > speed * 1.5f)
			{
				moveDirection = (targetPosition - Position).Normalized();
				hasMovement = true;
			}
			
			// 有移动输入则执行移动
			if (hasMovement)
			{
				Velocity = moveDirection * speed;
				KinematicCollision2D collision = MoveAndCollide(Velocity);
				
				// 检测碰撞停止（同时适用于键盘和鼠标移动）
				if (collision != null)
				{
					state = CharacterState.Idle;
					KeyDirection = 0; // 重置键盘方向
					
					// 如果是鼠标移动，清除目标位置
					if (KeyDirection == 0)
					{
						targetPosition = Position;
					}
				}
			}
			else
			{
				// 无移动输入则返回空闲状态
				state = CharacterState.Idle;
			}
		}


        // 交互物品检测
        if (GetTree().GetNodesInGroup($"ReachedItem{Id}").Count > 0)
        {
            handable = true;
            interactItem = GetTree().GetFirstNodeInGroup($"ReachedItem{Id}") as InteractableItem;
            
            // 找出距离最近的交互物品
            foreach (InteractableItem item in GetTree().GetNodesInGroup($"ReachedItem{Id}"))
            {
                if (interactItem.Position.DistanceTo(Position) > item.Position.DistanceTo(Position))
                {
                    interactItem = item;
                }
            }
        }
        else
        {
            handable = false;
        }
    }

    public override void _Input(InputEvent @event)
    {
        // 鼠标右键点击移动
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Right && mouseEvent.IsPressed())
            {
                if (!IsRiding() && !IsWaiting())
                {
                    var mousePos = GetGlobalMousePosition();
                    tarPosNotifer.Position = new Vector2(mousePos.X, Position.Y);
                    tarPosNotifer.ShowTarget();
                    targetPosition = new Vector2(mousePos.X, Position.Y);
                    state = CharacterState.Moving;
                }
                else if (IsRiding())
                {
                    StopRiding();
                }
            }
        }
        
        // 键盘输入处理
        if (@event is InputEventKey keyEvent)
        {
            if (keyEvent.Pressed)
            {
                // E键交互
                if (keyEvent.Keycode == Key.E && handable && !IsWaiting())
                {
                    state = CharacterState.Idle;
                    interactItem.Action();
                }
                
                // 移动控制
                if (!IsRiding() && !IsWaiting())
                {
                    if (keyEvent.Keycode == Key.A)
                    {
                        KeyDirection = -1;
                        state = CharacterState.Moving;
                    }
                    else if (keyEvent.Keycode == Key.D)
                    {
                        KeyDirection = 1;
                        state = CharacterState.Moving;
                    }
                }
                
                // R键停止骑乘
                else if (IsRiding() && keyEvent.Keycode == Key.R)
                {
                    StopRiding();
                }
                
                // Tab键切换等待状态
                if (keyEvent.Keycode == Key.Tab)
                {
                    if (!IsWaiting())
                    {
                        prevState = state;
                        state = CharacterState.Waiting;
                    }
                    else
                    {                        
                        state = prevState;
                    }
                }
            }
            // 按键释放处理
            else if ((keyEvent.Keycode == Key.A || keyEvent.Keycode == Key.D) && IsMoving())
            {
                KeyDirection = 0;
                state = CharacterState.Idle;
            }
        }
    }

    // 骑乘功能
    public void Ride(RideableItem chair)
    {
        if (!IsRiding() && !IsWaiting())
        {
            _prevPosition = Position;
            Position = chair.Position + chair.riderOffset;
            state = CharacterState.Riding;
        }
    }

    public void StopRiding()
    {
        Position = _prevPosition;
        state = CharacterState.Idle;
    }

    // 状态检查方法
    public bool IsRiding() => state == CharacterState.Riding;
    public bool IsMoving() => state == CharacterState.Moving;
    public bool IsIdle() => state == CharacterState.Idle;
    public bool IsWaiting() => state == CharacterState.Waiting;

    // 动作后处理
    public void AfterAction()
    {
        if (IsMoving())
        {
            state = CharacterState.Idle;
        }
    }
}
