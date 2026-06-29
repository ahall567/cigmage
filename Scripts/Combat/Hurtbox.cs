using Godot;
using System;

namespace FpsTest.Combat;

[GlobalClass]
public partial class Hurtbox : Area3D
{
    private bool _isActive;
    
    // --- EVENTS ---
    public event Action<CombatPayload> PayloadAccepted;
    
    // --- PUBLIC API ---
    public void SetActive(bool active) => _isActive = active;

    public void AcceptPayload(CombatPayload payload)
    {
        if (_isActive)PayloadAccepted?.Invoke(payload);
    }
}
