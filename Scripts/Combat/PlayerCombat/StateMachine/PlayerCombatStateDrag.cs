using CigMage.PlayerCharacter.Cig;
using Godot;

namespace CigMage.Combat.PlayerCombat.StateMachine;

[GlobalClass]
public partial class PlayerCombatStateDrag : PlayerCombatStateBase
{
    public override PlayerCombatState StateKey => PlayerCombatState.Drag;

    [Export] private float _refillRate;

    // --- GODOT OVERRIDES ---
    public override void _ExitTree()
    {
        UnsubscribeFromEvents();
    }

    // --- STATE METHODS ---
    public override void Enter()
    {
        base.Enter();
        
        // TODO: Subscribe to state events here
        
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
        UnsubscribeFromEvents();
    }
    
    // --- EVENT HANDLERS ---
    private void OnWindupCompleted()
    {
        CigStateMachine.ChangeState(CigState.Inhale);
    }

    private void OnTooMuchSmoke()
    {
        StateMachine.ChangeState(PlayerCombatState.Cough);
    }
    
    // --- INTERNAL LOGIC ---
    private void UnsubscribeFromEvents()
    {
        
    }
}
