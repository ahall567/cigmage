using Godot;
using FpsTest.StateMachine;

namespace FpsTest.Combat.PlayerCombat.StateMachine;

[GlobalClass]
public partial class PlayerCombatStateMachine : StateMachine<PlayerCombatState>
{
    [Export] private PlayerCombatState _initialState;

    public override void _Ready()
    {
        InitialState = _initialState;
        base._Ready();
    }
}
