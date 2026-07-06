using Godot;
using System;
using CigMage.StateMachine;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateMachine : StateMachine<SpellState>
{
    private SpellState _initialState = SpellState.Idle;
    
    // --- EVENTS ---
    public event Action RecoveryCompleted;

    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        InitialState = _initialState;
        base._Ready();

        SubscribeToEvents();
    }

    public override void _ExitTree()
    {
        UnsubscribeFromEvents();
    }

    // --- INTERNAL LOGIC ---
    private void SubscribeToEvents()
    {
        if (States[SpellState.Recover] is SpellStateRecover recoverState) recoverState.RecoveryCompleted += OnRecoveryCompleted;
    }

    private void UnsubscribeFromEvents()
    {
        if (States[SpellState.Recover] is SpellStateRecover recoverState) recoverState.RecoveryCompleted -= OnRecoveryCompleted;
    }
    
    // --- EVENT HANDLERS ---
    private void OnRecoveryCompleted()
    {
        RecoveryCompleted?.Invoke();
    }
}
