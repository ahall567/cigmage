using Godot;

namespace CigMage.Combat.PlayerCombat.StateMachine;

[GlobalClass]
public partial class PlayerCombatStateIdle : PlayerCombatStateBase
{
    public override PlayerCombatState StateKey => PlayerCombatState.Idle;

    public override void HandleInput(InputEvent @event)
    {
        base.HandleInput(@event);
        if (@event.IsActionPressed("primary_fire"))
        {
            StateMachine.ChangeState(PlayerCombatState.Cast);
        }
        else if (@event.IsActionPressed("take_drag"))
        {
            StateMachine.ChangeState(PlayerCombatState.Drag);
        }
    }
}
