using CigMage.PlayerCharacter.Cig;
using Godot;

namespace CigMage.Combat.PlayerCombat.StateMachine;

[GlobalClass]
public partial class PlayerCombatStateDrag : PlayerCombatStateBase
{
    public override PlayerCombatState StateKey => PlayerCombatState.Drag;

    [Export] private float _refillRate;

    // --- STATE METHODS ---
    public override void Enter()
    {
        base.Enter();

        CigStateMachine.WindupCompleted += OnWindupCompleted;
        CigStateMachine.ExhaleCompleted += OnExhaleCompleted;
        
        CigStateMachine.ChangeState(CigState.Windup);
    }

    public override void HandleInput(InputEvent @event)
    {
        base.HandleInput(@event);
        if (@event.IsActionReleased("take_drag"))
        {
            StateMachine.ChangeState(PlayerCombatState.Idle);
        }
    }

    public override void Exit()
    {
        CigStateMachine.ChangeState(CigState.Exhale);
    }
    
    // --- GODOT OVERRIDES ---
    public override void _ExitTree()
    {
        UnsubscribeFromEvents();
    }
    
    // --- EVENT HANDLERS ---
    private void OnWindupCompleted()
    {
        CigStateMachine.WindupCompleted -= OnWindupCompleted;
        CigStateMachine.ChangeState(CigState.Inhale);
    }

    private void OnExhaleCompleted()
    {
        CigStateMachine.ExhaleCompleted -= OnExhaleCompleted;
        CigStateMachine.ChangeState(CigState.Idle);
        
    }

    private void OnTooMuchSmoke()
    {
        StateMachine.ChangeState(PlayerCombatState.Cough);
    }
    
    // --- INTERNAL LOGIC ---
    private void UnsubscribeFromEvents()
    {
        CigStateMachine.WindupCompleted -= OnWindupCompleted;
        CigStateMachine.ExhaleCompleted -= OnExhaleCompleted;
    }
}
