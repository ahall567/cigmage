using Godot;
using System;

namespace CigMage.PlayerCharacter.Cig;

[GlobalClass]
public partial class CigStateWindup : CigStateBase
{
    public override CigState StateKey => CigState.Windup;
    
    // TODO: Remove timer once AnimationPlayer is implemented
    private Timer _windupTimer;
    
    // --- EVENTS ---
    public event Action WindupCompleted;
    
    // --- STATE METHODS ---
    public override void Enter()
    {
        base.Enter();
        if (_windupTimer != null) _windupTimer.Start();
    }

    public override void Exit()
    {
        base.Exit();
        if (_windupTimer != null) _windupTimer.Stop();
    }
    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        base._Ready();
        
        // TODO: Remove timer once AnimationPlayer is implemented
        _windupTimer = new Timer();
        _windupTimer.WaitTime = 0.5f;
        _windupTimer.OneShot = true;
        _windupTimer.Timeout += OnWindupTimerExpired;
        AddChild(_windupTimer);
    }
    
    // --- EVENT HANDLERS ---
    // TODO: Subscribe this to animation_finished once AnimationPlayer is implemented
    private void OnWindupTimerExpired()
    {
        WindupCompleted?.Invoke();
    }
}
