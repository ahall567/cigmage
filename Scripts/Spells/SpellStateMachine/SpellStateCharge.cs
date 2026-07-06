using Godot;
using System;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateCharge : SpellStateBase
{
    public override SpellState StateKey => SpellState.Charge;

    private Timer _chargeTimer;
    
    // --- EVENTS ---
    public event Action ChargeCompleted;
    
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

    public override void OnSpellBaseReady()
    {
        base.OnSpellBaseReady();
        
        _chargeTimer = new Timer();
        _chargeTimer.WaitTime = SpellData.ChargeTime;
        _chargeTimer.OneShot = true;
        _chargeTimer.Timeout += OnChargeTimerExpired;
        AddChild(_chargeTimer);
    }

    // --- EVENT HANDLERS ---
    private void OnChargeTimerExpired()
    {
        ChargeCompleted?.Invoke();
    }
}
