using Godot;
using CigMage.StateMachine;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateMachine : StateMachine<SpellState>
{
    private SpellState _initialState = SpellState.Idle;

    public override void _Ready()
    {
        InitialState = _initialState;
        base._Ready();
    }
}
