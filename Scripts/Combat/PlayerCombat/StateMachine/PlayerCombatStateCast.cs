using CigMage.Spells.SpellStateMachine;
using Godot;

namespace CigMage.Combat.PlayerCombat.StateMachine;

[GlobalClass]
public partial class PlayerCombatStateCast : PlayerCombatStateBase
{
    public override PlayerCombatState StateKey => PlayerCombatState.Cast;
    
    // --- GODOT OVERRIDES ---
    public override void _ExitTree()
    {
        UnsubscribeFromEvents();
    }

    // --- STATE METHODS ---
    public override void Enter()
    {
        base.Enter();
        // TODO: Move animation control to SpellState once models are moved the SpellBase scene
        PlayerCombatControllerParent.Animator.Play("Magic_spell");
        
        // Subscribe to state events
        SpellStateMachine.ChargeCompleted += OnChargeCompleted;
        SpellStateMachine.SpellCastFailed += OnSpellCastFailed;
        SpellStateMachine.RecoveryCompleted += OnRecoveryCompleted;
        
        SpellStateMachine.ChangeState(SpellState.Charge);
    }

    public override void HandleInput(InputEvent @event)
    {
        base.HandleInput(@event);
        if (@event.IsActionReleased("primary_fire"))
        {
            StateMachine.ChangeState(PlayerCombatState.Idle);
        }
    }

    public override void Exit()
    {
        if (PlayerCombatControllerParent.Animator != null)
        {
            PlayerCombatControllerParent.Animator.Play("Relax_hands_idle");
        }
        SpellStateMachine.ChangeState(SpellState.Recover);
        UnsubscribeFromEvents();
    }
    
    // --- INTERNAL LOGIC ---
    private void UnsubscribeFromEvents()
    {
        SpellStateMachine.ChargeCompleted -= OnChargeCompleted;
        SpellStateMachine.SpellCastFailed -= OnSpellCastFailed;
        SpellStateMachine.RecoveryCompleted -= OnRecoveryCompleted;
    }
    
    // --- EVENT HANDLERS ---
    private void OnChargeCompleted()
    {
        SpellStateMachine.ChargeCompleted -= OnChargeCompleted;
        SpellStateMachine.ChangeState(SpellState.Cast);
    }

    private void OnSpellCastFailed()
    {
        SpellStateMachine.SpellCastFailed -= OnSpellCastFailed;
        SpellStateMachine.ChangeState(SpellState.Recover);
    }
    
    private void OnRecoveryCompleted()
    {
        SpellStateMachine.RecoveryCompleted -= OnRecoveryCompleted;
        StateMachine.ChangeState(PlayerCombatState.Idle);
    }
}
