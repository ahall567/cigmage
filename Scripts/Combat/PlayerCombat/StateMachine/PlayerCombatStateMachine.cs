using CigMage.StateMachine;
using Godot;

namespace CigMage.Combat.PlayerCombat.StateMachine;

[GlobalClass]
public partial class PlayerCombatStateMachine : StateMachine<PlayerCombatState>
{
    [Export] private PlayerCombatState _initialState;

    public override void _Ready()
    {
        InitialState = _initialState;
        GD.Print($"PlayerCombatStateMachine - Initial State: {InitialState}");
        base._Ready();
    }
}
