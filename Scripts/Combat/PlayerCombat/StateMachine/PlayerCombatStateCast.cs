using Godot;

namespace FpsTest.Combat.PlayerCombat.StateMachine;

[GlobalClass]
public partial class PlayerCombatStateCast : PlayerCombatStateBase
{
    public override PlayerCombatState StateKey => PlayerCombatState.Cast;

    public override void Enter()
    {
        base.Enter();
        PlayerCombatControllerParent.Spell.Visible = true;
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
        base.Exit();
        PlayerCombatControllerParent.Spell.Visible = false;
    }
}
