using Godot;
using CigMage.Extensions;
using CigMage.PlayerCharacter.Cig;
using CigMage.StateMachine;
using CigMage.Spells.SpellStateMachine;

namespace CigMage.Combat.PlayerCombat.StateMachine;

public abstract partial class PlayerCombatStateBase : State<PlayerCombatState>
{
    [Export] protected Label3D StateLabel;
    
    protected PlayerCombatController PlayerCombatControllerParent => Parent as PlayerCombatController;
    
    protected SpellStateMachine SpellStateMachine;
    
    protected CigStateMachine CigStateMachine;
    protected CigBase Cig => CigStateMachine.Parent as CigBase;

    public override void _Ready()
    {
        base._Ready();
        SpellStateMachine = this.GetChildByType<SpellStateMachine>();
        CigStateMachine = this.GetChildByType<CigStateMachine>();
    }

    public override void Enter()
    {
        base.Enter();
        StateLabel.Text = $"PlayerCombatState: {StateKey.ToString()}";
    }
}