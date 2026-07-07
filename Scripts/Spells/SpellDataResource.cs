using Godot;

namespace CigMage.Spells;

[GlobalClass]
public partial class SpellDataResource : Resource
{
    [ExportGroup("Identity")]
    [Export] private string _spellName;
    [Export] private PackedScene _prefab;
    
    [ExportGroup("Casting Stats")]
    [Export] private float _manaCost;
    [Export] private float _chargeTime;
    [Export] private float _recoveryTime;

    public string SpellName => _spellName;
    public PackedScene Prefab => _prefab;
    public float ManaCost => _manaCost;
    public float ChargeTime => _chargeTime;

    public float RecoveryTime => _recoveryTime;
}
