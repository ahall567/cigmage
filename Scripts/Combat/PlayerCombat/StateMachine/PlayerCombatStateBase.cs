using FpsTest.StateMachine;

namespace FpsTest.Combat.PlayerCombat.StateMachine;

public abstract partial class PlayerCombatStateBase : State<PlayerCombatState>
{
    protected PlayerCombatController PlayerCombatControllerParent => Parent as PlayerCombatController;
}
