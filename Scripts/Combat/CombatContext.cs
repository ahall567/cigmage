using Godot;

namespace CigMage.Combat;

public readonly record struct CombatContext(Vector3 SourcePosition, Node? Source);