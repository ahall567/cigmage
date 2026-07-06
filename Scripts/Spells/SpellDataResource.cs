using Godot;

namespace CigMage.Spells;

[GlobalClass]
public partial class SpellDataResource : Resource
{
    [ExportGroup("Identity")]
    [Export] public string SpellName;
    [Export] public PackedScene Prefab;
    
    [ExportGroup("Casting Stats")]
    [Export] public float ManaCost;
    [Export] public float ChargeTime;
    [Export] public float RecoveryTime;
}
