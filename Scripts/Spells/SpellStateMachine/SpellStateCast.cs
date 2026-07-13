using Godot;
using System;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateCast : SpellStateBase
{
    public override SpellState StateKey => SpellState.Cast;

    [Export] private float _castInterval;

    private Timer _castCooldown;
    
    // --- EVENTS ---
    public event Action CastFailed;

    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        base._Ready();
        
        _castCooldown = new Timer();
        _castCooldown.WaitTime = _castInterval;
        _castCooldown.OneShot = true;
        _castCooldown.Timeout += OnCastCooldownExpired;
        AddChild(_castCooldown);
    }

    public override void _ExitTree()
    {
        _castCooldown.Timeout -= OnCastCooldownExpired;
    }
    

    // --- CLASS OVERRIDES ---
    public override void Enter()
    {
        base.Enter();
        
        GD.Print($"SpellStateCast.Enter");
        OnCastCooldownExpired();
    }

    public override void Exit()
    {
        GD.Print($"SpellStateCast.Exit");
        _castCooldown.Stop();
    }

    // --- EVENT HANDLERS ---
    private void OnCastCooldownExpired()
    {
        GD.Print($"SpellStateCast: OnCastCooldownExpired called");
        switch (SpellBaseParent.TrySpellCast())
        {
            case true:
                _castCooldown.Start();
                break;
            case false:
                CastFailed?.Invoke();
                break;
        }
        
    }
}
