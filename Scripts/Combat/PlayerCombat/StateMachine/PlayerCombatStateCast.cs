using CigMage.Spells.SpellStateMachine;
using Godot;

namespace CigMage.Combat.PlayerCombat.StateMachine;

[GlobalClass]
public partial class PlayerCombatStateCast : PlayerCombatStateBase
{
    public override PlayerCombatState StateKey => PlayerCombatState.Cast;
    
    // --- STATE METHODS ---
    public override void Enter()
    {
        base.Enter();
        PlayerCombatControllerParent.Animator.Play("Magic_spell");
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
    }
}
