using Godot;
using System;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateRecover : SpellStateBase, IRequireSpellData
{
    public override SpellState StateKey => SpellState.Recover;

    private Timer _recoveryTimer;
    
    // --- EVENTS ---
    public event Action RecoveryCompleted;
    
    // --- GODOT OVERRIDES ---
    public override void _ExitTree()
    {
        base._ExitTree();
        
        if (_recoveryTimer != null) _recoveryTimer.Timeout -= OnRecoveryTimerExpired;
    }

    // --- CLASS OVERRIDES ---
    public override void Enter()
    {
        base.Enter();
        if (_recoveryTimer != null) _recoveryTimer.Start();
    }

    public override void Exit()
    {
        base.Exit();
        if (_recoveryTimer != null) _recoveryTimer.Stop();
    }
    
    // --- IRequireSpellData IMPLEMENTATION ---
    public void InitFromData(SpellDataResource spellData)
    {
        _recoveryTimer = new Timer();
        _recoveryTimer.WaitTime = spellData.RecoveryTime;
        _recoveryTimer.OneShot = true;
        _recoveryTimer.Timeout += OnRecoveryTimerExpired;
        AddChild(_recoveryTimer);
    }

    // --- EVENT HANDLERS ---
    private void OnRecoveryTimerExpired()
    {
        StateMachine.ChangeState(SpellState.Idle);
        RecoveryCompleted?.Invoke();
    }
}
