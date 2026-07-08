using CigMage.PlayerCharacter.Cig;
using Godot;

namespace CigMage.Combat.PlayerCombat.StateMachine;

[GlobalClass]
public partial class PlayerCombatStateDrag : PlayerCombatStateBase
{
    public override PlayerCombatState StateKey => PlayerCombatState.Drag;

    [Export] private float _refillRate;

    public override void Enter()
    {
        base.Enter();
        
        CigStateMachine.ChangeState(CigState.Windup);
    }

    public override void Update(double delta)
    {
        float manaToAdd = _refillRate * (float)delta;
        PlayerCombatControllerParent.RegenMana(manaToAdd);
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
        base.Exit();
        PlayerCombatControllerParent.Cig.Visible = false;
    }
}
