using System;
using CigMage.Combat.PlayerCombat;
using Godot;
using CigMage.Extensions;

namespace CigMage.Spells;

public partial class SpellBase : Node3D
{
    [Export] private PlayerCombatController _combatController;
    [Export] private SpellDataResource _spellData;
    
    public SpellDataResource SpellData => _spellData;

    public Label3D StateLabel;

    // --- EVENTS ---
    public event Action<SpellDataResource> NewData;
    // TODO: Refactor this to use a specific spell object type once this is made (ex. SpellObject)
    public event Action<Node, SpellDataResource> SpellSpawned;

    public override void _EnterTree()
    {
        StateLabel = this.GetChildByType<Label3D>();
    }
    
    // --- PUBLIC API ---
    public bool TrySpellCast()
    {
        switch (_combatController.GetManaValue() >= _spellData.ManaCost)
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
        Node objectNode = _spellData.Prefab.Instantiate();
        if (objectNode is Node3D node3D)
        {
            node3D.GlobalTransform = this.GlobalTransform;
        }
        
        GetTree().CurrentScene.AddChild(objectNode);

        SpellSpawned?.Invoke(objectNode, _spellData);
    }
}
