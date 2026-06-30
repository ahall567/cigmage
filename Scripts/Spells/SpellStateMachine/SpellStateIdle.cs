using Godot;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateIdle : SpellStateBase
{
    public override SpellState StateKey => SpellState.Idle;

    public override void HandleInput(InputEvent @event)
    {
        base.HandleInput(@event);
        
        if (@event.IsActionPressed("primary_fire"))
        {
            StateMachine.ChangeState(SpellState.Cast);
        }
    }
}
