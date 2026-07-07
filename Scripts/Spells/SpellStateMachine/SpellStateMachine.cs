using Godot;
using System;
using CigMage.StateMachine;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateMachine : StateMachine<SpellState>
{
    private SpellState _initialState = SpellState.Idle;
    
    // --- EVENTS ---
    public event Action ChargeCompleted;
    public event Action RecoveryCompleted;
    public event Action SpellCastFailed;

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
        if (States[SpellState.Charge] is SpellStateCharge chargeState) chargeState.ChargeCompleted += OnChargeCompleted;
        if (States[SpellState.Cast] is SpellStateCast castState) castState.CastFailed += OnCastFailed;
        if (States[SpellState.Recover] is SpellStateRecover recoverState) recoverState.RecoveryCompleted += OnRecoveryCompleted;
    }

    private void UnsubscribeFromEvents()
    {
        if (States[SpellState.Charge] is SpellStateCharge chargeState) chargeState.ChargeCompleted -= OnChargeCompleted;
        if (States[SpellState.Cast] is SpellStateCast castState) castState.CastFailed += OnCastFailed;
        if (States[SpellState.Recover] is SpellStateRecover recoverState) recoverState.RecoveryCompleted -= OnRecoveryCompleted;
    }
    
    // --- EVENT HANDLERS ---
    private void OnChargeCompleted()
    {
        ChargeCompleted?.Invoke();
    }

    private void OnCastFailed()
    {
        SpellCastFailed?.Invoke();
    }
    
    private void OnRecoveryCompleted()
    {
        RecoveryCompleted?.Invoke();
    }
}
