using Godot;
using System;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateCharge : SpellStateBase
{
    public override SpellState StateKey => SpellState.Charge;

    [Export] private float _chargeTime;
    private Timer _chargeTimer;
    
    // --- EVENTS ---
    public event Action ChargeCompleted;

    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        base._Ready();

        _chargeTimer = new Timer();
        _chargeTimer.WaitTime = _chargeTime;
        _chargeTimer.OneShot = true;
        _chargeTimer.Timeout += OnChargeTimerExpired;
        AddChild(_chargeTimer);
    }
    
    // --- CLASS OVERRIDES ---
    public override void Enter()
    {
        base.Enter();
        _chargeTimer.Start();
    }

    public override void Exit()
    {
        base.Exit();
        _chargeTimer.Stop();
    }

    // --- EVENT HANDLERS ---
    private void OnChargeTimerExpired()
    {
        ChargeCompleted?.Invoke();
    }
}
