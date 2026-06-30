using Godot;
using System;
using System.Collections.Generic;
using CigMage.Extensions;

namespace CigMage.Combat;

[GlobalClass]
public partial class CombatHandler : Node
{
    // --- COMPONENTS ---
    private HitboxComponent _hitbox;
    private HurtboxComponent _hurtbox;
    private HealthComponent _health;
    
    // --- EVENTS ---
    public event Action<float> DamageTaken;
    public event Action<float> KnockbackTaken;
    public event Action<CombatPayload> PayloadHandled;
    // TODO: Put this in global event bus
    public event Action HealthZero;
    
    // --- STATUS ---
    private bool _isAttacking;
    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        GetNodeReferences();
        SubscribeToEvents();
        if (_hurtbox != null) _hurtbox.SetActive(true);
    }

    public override void _ExitTree()
    {
        UnsubscribeFromEvents();
    }

    // --- PUBLIC API ---
    public void SetAttacking(bool isAttacking)
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
    
    // --- INTERNAL LOGIC ---
    private void GetNodeReferences()
    {
        _hitbox = this.GetChildByType<HitboxComponent>();
        _hurtbox = this.GetChildByType<HurtboxComponent>();
        _health = this.GetChildByType<HealthComponent>();
    }
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
        return new CombatPayload(5, 5, Vector3.Zero);
    }
    
    // --- EVENT HANDLERS ---
    private void OnHurtboxDetected(List<HurtboxComponent> detectedHurtboxes)
    {
        foreach (HurtboxComponent hurtbox in detectedHurtboxes)
        {
            hurtbox.AcceptPayload(BuildPayload());
        }
    }

    private void OnPayloadAccepted(CombatPayload payload)
    {
        if (payload.DamageAmount > 0)
        {
            DamageTaken?.Invoke(payload.DamageAmount);
            GD.Print($"CombatHandler - Damage Taken: {payload.DamageAmount}");
            
            _health.Decrease(payload.DamageAmount);
        }

        if (payload.KnockbackAmount > 0)
        {
            KnockbackTaken?.Invoke(payload.KnockbackAmount);
        }
        
        // Logic for sending status effects
        
        PayloadHandled?.Invoke(payload);
    }

    private void OnHealthZero()
    {
        GD.Print($"CombatHandler - OnHealthZero called");
        HealthZero?.Invoke();
    }
}
