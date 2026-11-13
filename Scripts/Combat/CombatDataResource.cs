using Godot;
using CigMage.Combat.Effects;

namespace CigMage.Combat;

[GlobalClass]
public partial class CombatDataResource : Resource
{
    [Export] public Godot.Collections.Array<CombatEffect> CombatEffects { get; set; } = new();
}