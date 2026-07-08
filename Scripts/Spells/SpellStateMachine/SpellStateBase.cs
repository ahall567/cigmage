using CigMage.StateMachine;

namespace CigMage.Spells.SpellStateMachine;

public abstract partial class SpellStateBase : State<SpellState>
{
    protected SpellBase SpellBaseParent => Parent as SpellBase;
    protected SpellStateMachine SpellStateMachine => StateMachine as SpellStateMachine;
    
    // --- CLASS OVERRIDES ---
    public override void Enter()
    {
        SpellBaseParent.StateLabel.Text = $"SpellState: {StateKey.ToString()}";
    }
}
