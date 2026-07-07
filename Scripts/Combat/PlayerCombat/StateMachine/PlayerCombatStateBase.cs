using CigMage.Extensions;
using CigMage.StateMachine;
using CigMage.Spells.SpellStateMachine;

namespace CigMage.Combat.PlayerCombat.StateMachine;

public abstract partial class PlayerCombatStateBase : State<PlayerCombatState>
{
    protected PlayerCombatController PlayerCombatControllerParent => Parent as PlayerCombatController;
    protected SpellStateMachine SpellStateMachine;

    public override void _Ready()
    {
        base._Ready();
        SpellStateMachine = this.GetChildByType<SpellStateMachine>();
    }
}