using Godot;
using System;
using System.Collections.Generic;
using CigMage.Combat.Effects;

namespace CigMage.Combat;

[GlobalClass]
public partial class CombatHandler : Node
{
    // --- TEST ---
    [Export] private string? _debugName;
    
    // --- COMPONENTS ---
    [Export] private HitboxComponent? _hitbox;
    [Export] private HurtboxComponent? _hurtbox;
    [Export] private HealthComponent? _health;
    
    // --- DATA ---
    [Export] private Godot.Collections.Array<CombatEffect> _attackEffects = new();
    
    // --- EVENTS ---
    public event Action<float>? DamageTaken;
    public event Action<float>? KnockbackTaken;
    public event Action<CombatPayload>? PayloadHandled;
    // TODO: Put this in global event bus
    public event Action? HealthZero;
    
    // --- STATUS ---
    private bool _isAttacking;
    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        SubscribeToEvents();
        if (_hurtbox != null) _hurtbox.SetActive(true);
    }

    public override void _ExitTree()
    {
        UnsubscribeFromEvents();
    }

    // --- PUBLIC API ---
    // Toggle attacking status
    public void SetAttackingStatus()
    {
        if (_hitbox != null)
        {
            switch (_isAttacking)
            {
                case true:
                    _isAttacking = false;
                    _hitbox.SetActive(false);
                    break;
                case false:
                    _hitbox.SetActive(true);
                    _isAttacking = true;
                    _hitbox.CallDeferred(nameof(_hitbox.GetHurtboxes));
                    break;
            }
        }
    }
    
    // Explicitly set attacking status
    public void SetAttackingStatus(bool isAttacking)
    {
        if (_hitbox != null)
        {
            _hitbox.SetActive(isAttacking);
            _isAttacking = isAttacking;

            if (isAttacking)
            {
                _hitbox.CallDeferred(nameof(_hitbox.GetHurtboxes));
            }
        }
    }

    public void ImpulseAttack()
    {
        if (_hitbox != null)
        {
            GD.Print($"{_debugName}: ImpulseAttack called");
            SetAttackingStatus(true);
            //SetAttackingStatus(false); // TODO: Figure out if/how this needs to be set to false at some point
        }
    }

    public void ApplyDamage(float amount)
    {
        if (amount <= 0) return;
        DamageTaken?.Invoke(amount);
        _health?.Decrease(amount);
    }

    public void ApplyKnockback(float force, Vector3 sourcePosition)
    {
        if (force <= 0) return;
        KnockbackTaken?.Invoke(force);
    }

    public void ApplyHeal(float amount)
    {
        GD.Print($"{Name}: ApplyHeal Called");
        if (amount <= 0) return;
        _health?.Increase(amount);
    }
    
    // --- INTERNAL LOGIC ---
    private void SubscribeToEvents()
    {
        if (_hitbox != null) _hitbox.HurtboxDetected += OnHurtboxDetected;
        if (_hurtbox != null) _hurtbox.PayloadAccepted += OnPayloadAccepted;
        if (_health != null) _health.HealthZero += OnHealthZero;
    }

    private void UnsubscribeFromEvents()
    {
        if (_hitbox != null) _hitbox.HurtboxDetected -= OnHurtboxDetected;
        if (_hurtbox != null) _hurtbox.PayloadAccepted -= OnPayloadAccepted;
        if (_health != null) _health.HealthZero -= OnHealthZero;
    }

    private CombatPayload BuildPayload()
    {
        return new CombatPayload
        {
            SourcePosition = GetParent<Node3D>().GlobalPosition,
            Effects = _attackEffects
        };
    }
    
    // --- EVENT HANDLERS ---
    private void OnHurtboxDetected(List<HurtboxComponent> detectedHurtboxes)
    {
        // TODO: Create way to filter out hurtboxes so that Mobs don't damage other mobs
        foreach (HurtboxComponent hurtbox in detectedHurtboxes)
        {
            if (hurtbox != _hurtbox)
            {
                GD.Print($"{_debugName} Detected a Hurtbox");
                hurtbox.AcceptPayload(BuildPayload());
            }
        }
    }

    private void OnPayloadAccepted(CombatPayload payload)
    {
        var ctx = new CombatContext(payload.SourcePosition, null);
        foreach (var effect in payload.Effects)
        {
            effect.Apply(this, ctx);
            GD.Print($"{_debugName} is accepting an effect: {effect.GetType()}");
        }
        
        PayloadHandled?.Invoke(payload);
    }

    private void OnHealthZero()
    {
        HealthZero?.Invoke();
    }
}
