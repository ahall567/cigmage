using Godot;

namespace CigMage.Spells.SpellStateMachine;

[GlobalClass]
public partial class SpellStateCast : SpellStateBase
{
    public override SpellState StateKey => SpellState.Cast;

    [Export] private float _castInterval;

    private Timer _castCooldown;
    private bool _canFire;

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

    public override void HandleInput(InputEvent @event)
    {
        base.HandleInput(@event);
        
        if (@event.IsActionReleased("primary_fire"))
        {
            StateMachine.ChangeState(SpellState.Recover);
        }
    }

    // --- CLASS OVERRIDES ---
    public override void Update(double delta)
    {
        if (_canFire)
        {
            switch (SpellBaseParent.TrySpellCast())
            {
                case true:
                    _canFire = false;
                    _castCooldown.Start();
                    break;
                case false:
                    StateMachine.ChangeState(SpellState.Recover);
                    break;
            }
        }
    }

    // --- EVENT HANDLERS ---
    private void OnCastCooldownExpired()
    {
        _canFire = true;
    }
}
