using Godot;
using System;
using System.Collections.Generic;

namespace FpsTest.Combat;

[GlobalClass]
public partial class Hitbox : Area3D
{
    private bool _isActive;
    
    // --- EVENTS ---
    public event Action<List<Hurtbox>> HurtboxDetected;
    
    // --- PUBLIC API ---
    public void SetActive(bool active) => _isActive = active;

    public void GetHurtboxes()
    {
        List<Hurtbox> hurtboxes = new();
        Godot.Collections.Array<Area3D> overlappingAreas = GetOverlappingAreas();

        foreach (Area3D area in overlappingAreas)
        {
            if (area is Hurtbox hurtbox) hurtboxes.Add(hurtbox);
        }

        if (hurtboxes.Count > 0 && _isActive)
        {
            HurtboxDetected?.Invoke(hurtboxes);
        }
    }
}