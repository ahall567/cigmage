using Godot;
using System;
using System.Collections.Generic;

namespace CigMage.Combat;

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
            if (area is HurtboxComponent hurtbox) hurtboxes.Add(hurtbox);
        }

        if (hurtboxes.Count > 0 && _isActive)
        {
            HurtboxDetected?.Invoke(hurtboxes);
        }
    }
}