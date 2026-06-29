using Godot;
using CigMage.Extensions;

namespace CigMage.Combat.PlayerCombat;

[GlobalClass]
public partial class PlayerCombatController : Node3D
{
    public AnimationPlayer Animator;
    private ManaComponent _manaComponent;
    [Export] private Marker3D _spellSpawnPoint;
    [Export] public PackedScene SpellPrefab;
    [Export] public Label3D Cig;
    
    // --- GODOT OVERRIDES ---
    public override void _Ready()
    {
        Cig = GetNode<Label3D>("Cig");
        Animator = this.GetChildByType<AnimationPlayer>();
        _manaComponent = this.GetChildByType<ManaComponent>();
        Animator.Play("Relax_hands_idle");
    }

    // --- PUBLIC API ---
    
    public bool SpawnSpell(float manaCost)
    {
        if (manaCost >= _manaComponent.CurrentValue)
        {
            return false;
        }
        else
        {
            Node objectNode = SpellPrefab.Instantiate();
            if (objectNode is Node3D node3D)
            {
                node3D.GlobalTransform = _spellSpawnPoint.GlobalTransform;
            }
        
            GetTree().CurrentScene.AddChild(objectNode);
            
            _manaComponent.Decrease(manaCost);
            
            return true;
        }
    }

    public void RegenMana(float amount)
    {
        _manaComponent.Increase(amount);
    }
    
}
