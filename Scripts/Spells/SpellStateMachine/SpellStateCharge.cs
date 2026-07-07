using Godot;
using System;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateCharge : SpellStateBase, IRequireSpellData
{
    public override SpellState StateKey => SpellState.Charge;

    private Timer _chargeTimer;
    
    // --- EVENTS ---
    public event Action ChargeCompleted;
    
    // --- CLASS OVERRIDES ---
    public override void Enter()
    {
        base.Enter();
        if (_chargeTimer != null) _chargeTimer.Start();
    }

    public override void Exit()
    {
        base.Exit();
        if (_chargeTimer != null) _chargeTimer.Stop();
    }
    
    // --- IRequireSpellData IMPLEMENTATION ---
    public void InitFromData(SpellDataResource spellData)
    {
        _chargeTimer = new Timer();
        _chargeTimer.WaitTime = spellData.ChargeTime;
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
