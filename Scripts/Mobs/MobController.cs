using Godot;
using CigMage.Extensions;
using CigMage.Combat;
using CigMage.PlayerCharacter;

namespace CigMage.Mobs;

public partial class MobController : CharacterBody3D
{
    // TODO: Inject this upon instantiation
    [Export] private Player _player;
    [Export] private float _movementSpeed;
    
    private CombatHandler _combatHandler;
    private NavigationAgent3D _navAgent;
    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        _combatHandler = this.GetChildByType<CombatHandler>();
        _navAgent = this.GetChildByType<NavigationAgent3D>();
        SubscribeToEvents();
    }

    public override void _PhysicsProcess(double delta)
    {
        _navAgent.TargetPosition = _player.GlobalPosition;
        
        Vector3 velocity = Velocity;

        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }

        if (!_navAgent.IsNavigationFinished())
        {
            Vector3 next = _navAgent.GetNextPathPosition();
            
            if (!next.IsEqualApprox(GlobalPosition))
            {
                LookAt(new Vector3(next.X, GlobalPosition.Y, next.Z), Vector3.Up);
            }
            
            Vector3 diff = next - GlobalPosition;
            Vector3 dir = diff.Normalized();
            velocity.X = dir.X * _movementSpeed;
            velocity.Z = dir.Z * _movementSpeed;
        }
        else
        {
            velocity.X = 0f;
            velocity.Z = 0f;
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    public override void _ExitTree()
    {
        UnsubscribeFromEvents();
    }

    // --- INTERNAL LOGIC ---
    private void SubscribeToEvents()
    {
        _combatHandler.HealthZero += OnHealthZero;
    }

    private void UnsubscribeFromEvents()
    {
        _combatHandler.HealthZero -= OnHealthZero;
    }
    
    // --- EVENT HANDLERS ---
    private void OnHealthZero()
    {
        QueueFree();
    }
}
