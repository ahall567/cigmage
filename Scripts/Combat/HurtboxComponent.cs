using Godot;
using System;

namespace CigMage.Combat;

[GlobalClass]
public partial class HurtboxComponent : Area3D
{
    private bool _isActive;
    
    // --- EVENTS ---
    public event Action<CombatPayload> PayloadAccepted;
    
    // --- PUBLIC API ---
    public void SetActive(bool active) => _isActive = active;

    public void AcceptPayload(CombatPayload payload)
    {
        GD.Print("Hurtbox: Payload Accepted");
        if (_isActive)PayloadAccepted?.Invoke(payload);
    }
}
