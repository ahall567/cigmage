using Godot;
using CigMage.Extensions;
using CigMage.Spells;

namespace CigMage.Combat.PlayerCombat;

[GlobalClass]
public partial class PlayerCombatController : Node3D
{
    public AnimationPlayer Animator;
    private ManaComponent _manaComponent;
    private SpellBase _spell;
    [Export] private Marker3D _spellSpawnPoint;
    [Export] public PackedScene SpellPrefab;
    [Export] public Label3D Cig;
    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        Cig = GetNode<Label3D>("Cig");
        Animator = this.GetChildByType<AnimationPlayer>();
        _manaComponent = this.GetChildByType<ManaComponent>();
        _spell = this.GetChildByType<SpellBase>();

        _spell.SpellSpawned += OnSpellSpawned;
        
        Animator.Play("Relax_hands_idle");
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
    
    // --- EVENT HANDLERS ---
    private void OnSpellSpawned(float manaCost)
    {
        _manaComponent.Decrease(manaCost);
    }
}
