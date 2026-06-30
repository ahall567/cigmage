using Godot;
using System;

namespace CigMage.Spells;

public partial class SpellBase : Node3D
{
    [Export] private PackedScene _spellPrefab;
    [Export] private float _manaCost;
}
