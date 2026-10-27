using Godot;
using System;
using System.Collections.Generic;

namespace CigMage.Combat;

// Hitbox is used by attacks/spells to detect hurtboxes and apply damage/effects to them
[GlobalClass]
public partial class HitboxComponent : Area3D
{
    private bool _isActive;
    
    // --- EVENTS ---
    public event Action<List<HurtboxComponent>> HurtboxDetected;
    
    // --- PUBLIC API ---
    public void SetActive(bool active) => _isActive = active;

    public void GetHurtboxes()
    {
        List<HurtboxComponent> hurtboxes = new();
        Godot.Collections.Array<Area3D> overlappingAreas = GetOverlappingAreas();

        foreach (Area3D area in overlappingAreas)
        {
            if (area is HurtboxComponent hurtbox)
            {
                hurtboxes.Add(hurtbox);
                GD.Print($"HitboxComponent: Hurtbox detected - {hurtbox.Name}");
            }
        }

        if (hurtboxes.Count > 0 && _isActive)
        {
            HurtboxDetected?.Invoke(hurtboxes);
        }
    }
}