using Godot;
using System;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateCast : SpellStateBase
{
    public override SpellState StateKey => SpellState.Cast;

    [Export] private float _castInterval;

    private Timer _castCooldown;
    private bool _canFire;
    
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
        _canFire = true;
    }

    public override void _ExitTree()
    {
        _castCooldown.Timeout -= OnCastCooldownExpired;
    }
    

    // --- CLASS OVERRIDES ---
    public override void Enter()
    {
        base.Enter();
        
        _canFire = true;
    }

    public override void Update(double delta)
    {
        if (_canFire)
        {
            // TODO: Check for TrySpellCast in ChargeState
            switch (SpellBaseParent.TrySpellCast())
            {
                case true:
                    _canFire = false;
                    _castCooldown.Start();
                    break;
                case false:
                    CastFailed?.Invoke();
                    break;
            }
        }
    }

    public override void Exit()
    {
        _castCooldown.Stop();
    }

    // --- EVENT HANDLERS ---
    private void OnCastCooldownExpired()
    {
        _canFire = true;
    }
}
