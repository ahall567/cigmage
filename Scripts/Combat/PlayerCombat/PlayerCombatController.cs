using Godot;
using System.Collections.Generic;
using CigMage.Extensions;
using CigMage.Spells;

namespace CigMage.Combat.PlayerCombat;

[GlobalClass]
public partial class PlayerCombatController : Node3D
{
    private ManaComponent _manaComponent;
    private HealthComponent _healthComponent;
    private SpellBase _spell;

    public HealthComponent HealthComponent => _healthComponent;
    public ManaComponent ManaComponent => _manaComponent;
    public SpellBase Spell => _spell;
    
    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        _manaComponent = this.GetChildByType<ManaComponent>();
        _spell = this.GetChildByType<SpellBase>();
        _healthComponent = this.GetChildByType<HealthComponent>();

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
