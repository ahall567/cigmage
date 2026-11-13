using Godot;
using System.Collections.Generic;
using CigMage.Combat.Effects;

namespace CigMage.Combat;

public sealed class CombatPayload
{
    public Vector3 SourcePosition { get; init; }
    public IReadOnlyList<CombatEffect> Effects { get; init; } = [];
}
