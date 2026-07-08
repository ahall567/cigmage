using Godot;
using CigMage.StateMachine;

namespace CigMage.PlayerCharacter.Cig;

[GlobalClass]
public partial class CigStateMachine : StateMachine<CigState>
{
    private CigState _initialState = CigState.Idle;

    public override void _Ready()
    {
        InitialState = _initialState;
        base._Ready();
    }
}
