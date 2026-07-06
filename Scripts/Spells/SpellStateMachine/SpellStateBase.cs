using CigMage.StateMachine;
using Godot;

namespace CigMage.Spells.SpellStateMachine;

public abstract partial class SpellStateBase : State<SpellState>
{
    protected SpellBase SpellBaseParent => Parent as SpellBase;

    public override void Enter()
    {
        base.Enter();
        SpellBaseParent.StateLabel.Text = StateKey.ToString();
    }
}
