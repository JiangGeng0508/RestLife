using Godot;
using System;

public enum CharacterState
{
    Idle,
    Moving,      // 合并后的移动状态
    Running,     // 新增跑步状态
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

	// 状态值
	public float Health { get; set; } = 100f;
	public float Energy { get; set; } = 100f;
	public float Hunger { get; set; } = 100f;
	public float Thirst { get; set; } = 100f;

	// 属性值
	public float Intelligence { get; set; } = 1f;
	public float Strength { get; set; } = 1f;
	public float Charisma { get; set; } = 1f;
	public float Agility { get; set; } = 1f;
    
    // 移动参数
    float walkSpeed = 10f;       // 行走速度
    float runSpeed = 20f;        // 跑步速度
    private Vector2 _prevPosition = Vector2.Zero;
    int KeyDirection = 0, KeyDirectionLeft = 0, KeyDirectionRight = 0; // -1左, 0无, 1右
    private bool _isRunning = false;      // 是否正在跑步
	private bool _isRightMouseDown = false; // 是否右键按下

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

        // 更新鼠标追踪目标位置
        if (_isRightMouseDown)
        {
            var mousePos = GetGlobalMousePosition();
            tarPosNotifer.Position = new Vector2(mousePos.X, Position.Y);
            tarPosNotifer.ShowTarget();
            targetPosition = new Vector2(mousePos.X, Position.Y);
            state = _isRunning ? CharacterState.Running : CharacterState.Moving;
        }

        // 统一移动处理
        if (state == CharacterState.Moving || state == CharacterState.Running)
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
            else if ((targetPosition - Position).Length() > (_isRunning ? runSpeed : walkSpeed) * 1.5f)
            {
                moveDirection = (targetPosition - Position).Normalized();
                hasMovement = true;
            }
            
            // 有移动输入则执行移动
            if (hasMovement)
            {
                float currentSpeed = _isRunning ? runSpeed : walkSpeed;
                Velocity = moveDirection * currentSpeed;
                KinematicCollision2D collision = MoveAndCollide(Velocity);
                
                // 检测碰撞停止（同时适用于键盘和鼠标移动）
                if (collision != null)
                {
                    state = CharacterState.Idle;
                    KeyDirection = 0; // 重置键盘方向
                    
                    // 如果是鼠标移动，清除目标位置
                    if (!_isRightMouseDown)
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

    }

    public override void _Input(InputEvent @event)
    {
        // 处理Shift键按下/释放
        if (@event is InputEventKey shiftKeyEvent && shiftKeyEvent.Keycode == Key.Shift)
        {
            _isRunning = shiftKeyEvent.Pressed;
            // 如果正在移动，更新状态
            if (IsMoving())
            {
                state = _isRunning ? CharacterState.Running : CharacterState.Moving;
            }
        }
        // 鼠标右键点击移动
        if (@event is InputEventMouseButton mouseEvent)
        {
            if (mouseEvent.ButtonIndex == MouseButton.Right && mouseEvent.IsPressed())
            {
                if (!IsRiding() && !IsWaiting())
                {
                    _isRightMouseDown = true;
                    state = _isRunning ? CharacterState.Running : CharacterState.Moving;
                }
                else if (IsRiding())
                {
                    StopRiding();
                }
            }
            else if (mouseEvent.ButtonIndex == MouseButton.Right && !mouseEvent.IsPressed())
            {
                _isRightMouseDown = false;
                state = CharacterState.Idle; // 确保释放右键后状态变为Idle
            }
        }
    
        // 键盘输入处理
        if (@event is InputEventKey keyEvent)
        {
            if (keyEvent.Pressed)
            {
                // E键交互
                if (keyEvent.Keycode == Key.E && !IsWaiting())
                {
					// 交互物品检测
					if (GetTree().GetNodesInGroup($"ReachedItem{Id}").Count > 0)
					{
						interactItem = GetTree().GetFirstNodeInGroup($"ReachedItem{Id}") as InteractableItem;
						
						// 找出距离最近的交互物品
						foreach (InteractableItem item in GetTree().GetNodesInGroup($"ReachedItem{Id}"))
						{
							if (interactItem.Position.DistanceTo(Position) > item.Position.DistanceTo(Position))
							{
								interactItem = item;
							}
						}
                    state = CharacterState.Idle;
                    interactItem.Action();
					}
                }
                
                // 移动控制
                if (!IsRiding() && !IsWaiting())
                {
                    if (keyEvent.Keycode == Key.A)
                    {
                        KeyDirectionLeft = 1;
                        UpdateMovementState();
                    }
                    if (keyEvent.Keycode == Key.D)
                    {
                        KeyDirectionRight = 1;
                        UpdateMovementState();
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
                        prevState = (state == CharacterState.Moving) ? CharacterState.Idle : state;
                        state = CharacterState.Waiting;
                    }
                    else
                    {
                        state = prevState;
                    }
                }
            }
            else
            {
                if (keyEvent.Keycode == Key.A) KeyDirectionLeft = 0;
                else if (keyEvent.Keycode == Key.D) KeyDirectionRight = 0;
                // 按键释放处理
                UpdateMovementState();
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
    public bool IsRunning() => state == CharacterState.Running; // 新增跑步状态检查
    public bool IsIdle() => state == CharacterState.Idle;
    public bool IsWaiting() => state == CharacterState.Waiting;

    // 动作后处理
    public void AfterAction()
    {
        if (IsMoving() || IsRunning())
        {
            state = CharacterState.Idle;
        }
    }

    // 更新移动状态方法
    private void UpdateMovementState()
    {
        // 计算最终的方向
        KeyDirection = KeyDirectionRight - KeyDirectionLeft;
        if (KeyDirection != 0 && !IsRiding() && !IsWaiting())
        {
            state = _isRunning ? CharacterState.Running : CharacterState.Moving;
        }
        else
        {
            state = CharacterState.Idle;
            Velocity = Vector2.Zero; // 确保速度被重置
        }
    }
}
