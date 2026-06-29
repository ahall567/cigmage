using Godot;

namespace FpsTest.Combat;

public readonly record struct CombatPayload(
    float DamageAmount,
    Vector3 SourcePosition
    // Add spell effect once these are created
);