using CigMage.StateMachine;
using CigMage.Combat.PlayerCombat;

namespace CigMage.Combat.PlayerCombat.StateMachine;

public abstract partial class PlayerCombatStateBase : State<PlayerCombatState>
{
    protected PlayerCombatController PlayerCombatControllerParent => Parent as PlayerCombatController;
}
