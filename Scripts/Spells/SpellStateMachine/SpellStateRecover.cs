using Godot;
using System;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateRecover : SpellStateBase
{
    public override SpellState StateKey => SpellState.Recover;

    private Timer _recoveryTimer;
    
    // --- EVENTS ---
    public event Action RecoveryCompleted;
    
    // --- GODOT OVERRIDES ---
    public override async void _Ready()
    {
        base._Ready();
        
        _recoveryTimer = new Timer();
        _recoveryTimer.WaitTime = SpellData.RecoveryTime;
        _recoveryTimer.OneShot = true;
        _recoveryTimer.Timeout += OnRecoveryTimerExpired;
        AddChild(_recoveryTimer);
    }

    public override void _ExitTree()
    {
        base._ExitTree();
        
        _recoveryTimer.Timeout -= OnRecoveryTimerExpired;
    }

    // --- CLASS OVERRIDES ---
    public override void Enter()
    {
        base.Enter();
        _recoveryTimer.Start();
    }

    public override void Exit()
    {
        base.Exit();
        _recoveryTimer.Stop();
    }

    // --- EVENT HANDLERS ---
    private void OnRecoveryTimerExpired()
    {
        StateMachine.ChangeState(SpellState.Idle);
        RecoveryCompleted?.Invoke();
    }
}
