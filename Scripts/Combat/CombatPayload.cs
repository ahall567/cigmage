using Godot;

namespace CigMage.Combat;

public readonly record struct CombatPayload(
    float DamageAmount,
    float KnockbackAmount,
    Vector3 SourcePosition
    // Add spell effect once these are created
);