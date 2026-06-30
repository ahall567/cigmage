using System;
using CigMage.Combat.PlayerCombat;
using Godot;
using CigMage.Extensions;

namespace CigMage.Spells;

public partial class SpellBase : Node3D
{
    [Export] private PlayerCombatController _combatController;
    [Export] private PackedScene _spellPrefab;
    [Export] public float ManaCost;
    [Export] public float ChargeTime;

    public Label3D StateLabel;

    // TODO: Change this to use SpellData
    public event Action<float> SpellSpawned;

    public override void _EnterTree()
    {
        StateLabel = this.GetChildByType<Label3D>();
    }
    
    // --- PUBLIC API ---
    public bool TrySpellCast()
    {
        switch (_combatController.GetManaValue() >= ManaCost)
        {
            case true:
                SpawnSpell();
                return true;
            case false:
                return false;
        }
    }
    
    // --- INTERNAL LOGIC ---
    private void SpawnSpell()
    {
        Node objectNode = _spellPrefab.Instantiate();
        if (objectNode is Node3D node3D)
        {
            node3D.GlobalTransform = this.GlobalTransform;
        }
        
        GetTree().CurrentScene.AddChild(objectNode);

        SpellSpawned?.Invoke(ManaCost);
    }
}
