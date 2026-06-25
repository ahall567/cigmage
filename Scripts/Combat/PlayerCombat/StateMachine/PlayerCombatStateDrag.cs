using Godot;

namespace FpsTest.Combat.PlayerCombat.StateMachine;

[GlobalClass]
public partial class PlayerCombatStateDrag : PlayerCombatStateBase
{
    public override PlayerCombatState StateKey => PlayerCombatState.Drag;

    [Export] private float _refillRate = 5;

    public override void Enter()
    {
        base.Enter();
        PlayerCombatControllerParent.Cig.Visible = true;
    }

    public override void Update(double delta)
    {
        PlayerCombatControllerParent.CurrentMana += _refillRate;
        GD.Print($"CurrentMana: {PlayerCombatControllerParent.CurrentMana}");
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
