using CigMage.StateMachine;
using Godot;

namespace CigMage.Spells.SpellStateMachine;

public abstract partial class SpellStateBase : State<SpellState>
{
    protected SpellBase SpellBaseParent => Parent as SpellBase;
    protected SpellStateMachine SpellStateMachine => StateMachine as SpellStateMachine;
    protected SpellDataResource SpellData;
    
    // --- PUBLIC API ---
    public void InitFromData(SpellDataResource spellData)
    {
        SpellData = spellData;
    }
    
    // --- CLASS OVERRIDES ---
    public override void Enter()
    {
        SpellBaseParent.StateLabel.Text = StateKey.ToString();
    }
    
    // --- EVENT HANDLERS ---
    public virtual void OnSpellBaseReady()
    {
        InitFromData(SpellBaseParent.SpellData);
    }
}
