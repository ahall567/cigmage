using Godot;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateIdle : SpellStateBase
{
    public override SpellState StateKey => SpellState.Idle;
}
