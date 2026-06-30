using Godot;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateCharge : SpellStateBase
{
    public override SpellState StateKey => SpellState.Charge;

    private Timer _chargeTimer;

    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        base._Ready();

        _chargeTimer = new Timer();
        _chargeTimer.WaitTime = 1f;
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
        StateMachine.ChangeState(SpellState.Cast);
    }
}
