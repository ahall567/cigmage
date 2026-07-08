using CigMage.Extensions;
using CigMage.PlayerCharacter.Cig;
using CigMage.StateMachine;
using CigMage.Spells.SpellStateMachine;

namespace CigMage.Combat.PlayerCombat.StateMachine;

public abstract partial class PlayerCombatStateBase : State<PlayerCombatState>
{
    protected PlayerCombatController PlayerCombatControllerParent => Parent as PlayerCombatController;
    protected SpellStateMachine SpellStateMachine;
    protected CigStateMachine CigStateMachine;

    public override void _Ready()
    {
        base._Ready();
        SpellStateMachine = this.GetChildByType<SpellStateMachine>();
        CigStateMachine = this.GetChildByType<CigStateMachine>();
    }
}