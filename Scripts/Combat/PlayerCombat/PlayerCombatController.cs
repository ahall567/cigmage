using Godot;
using System.Collections.Generic;
using CigMage.Extensions;
using CigMage.Spells;

namespace CigMage.Combat.PlayerCombat;

[GlobalClass]
public partial class PlayerCombatController : Node3D
{
    private ManaComponent _manaComponent;
    private SpellBase _spell;
    [Export] public Label3D Cig;
    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        Cig = GetNode<Label3D>("Cig");
        _manaComponent = this.GetChildByType<ManaComponent>();
        _spell = this.GetChildByType<SpellBase>();

        if (_spell != null)
        {
            _spell.SpellSpawned += OnSpellSpawned;
            InitChildren();
        }
    }

    public override void _ExitTree()
    {
        _spell.SpellSpawned -= OnSpellSpawned;
    }

    // --- PUBLIC API ---
    public float GetManaValue()
    {
        return _manaComponent.CurrentValue;
    }

    public void RegenMana(float amount)
    {
        _manaComponent.Increase(amount);
    }
    
    // --- INTERNAL LOGIC ---
    private void InitChildren()
    {
        List<Node> allChildren = this.GetChildrenByType<Node>();
        foreach (Node child in allChildren)
        {
            if (child is IRequireSpellData needsData)
            {
                needsData.InitFromData(_spell.SpellData);
            }
        }
    }
    
    // --- EVENT HANDLERS ---
    private void OnSpellSpawned(Node spellNode, SpellDataResource spellData)
    {
        _manaComponent.Decrease(spellData.ManaCost);
    }
}
