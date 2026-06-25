using Godot;

namespace FpsTest.Combat.PlayerCombat.StateMachine;

[GlobalClass]
public partial class PlayerCombatStateCast : PlayerCombatStateBase
{
    public override PlayerCombatState StateKey => PlayerCombatState.Cast;

    [Export] private float _cooldownTime;
    [Export] private float _manaCost;

    private Timer _castCooldown;
    private bool _canFire;
    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        base._Ready();
        _castCooldown = new Timer();
        _castCooldown.WaitTime = _cooldownTime;
        _castCooldown.OneShot = true;
        _castCooldown.Timeout += OnCastCooldownExpired;
        AddChild(_castCooldown);
        _canFire = true;
    }

    public override void _ExitTree()
    {
        _castCooldown.Timeout -= OnCastCooldownExpired;
    }

    // --- STATE METHODS ---
    public override void Enter()
    {
        base.Enter();
        PlayerCombatControllerParent.Animator.Play("Magic_spell");
    }

    public override void Update(double delta)
    {
        base.Update(delta);
        if (_canFire)
        {
            PlayerCombatControllerParent.SpawnSpell();
            _canFire = false;
            _castCooldown.Start();
            PlayerCombatControllerParent.CurrentMana -= _manaCost;
            GD.Print($"Current Mana: {PlayerCombatControllerParent.CurrentMana}");
        }

        if (PlayerCombatControllerParent.CurrentMana < _manaCost)
        {
            StateMachine.ChangeState(PlayerCombatState.Idle);
        }
    }

    public override void HandleInput(InputEvent @event)
    {
        base.HandleInput(@event);
        if (@event.IsActionReleased("primary_fire"))
        {
            StateMachine.ChangeState(PlayerCombatState.Idle);
        }
    }

    public override void Exit()
    {
        if (PlayerCombatControllerParent.Animator != null)
        {
            PlayerCombatControllerParent.Animator.Play("Relax_hands_idle");
        }
    }

    // --- EVENT HANDLERS ---
    private void OnCastCooldownExpired()
    {
        _canFire = true;
    }
}
