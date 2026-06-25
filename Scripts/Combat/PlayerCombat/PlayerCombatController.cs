using Godot;
using FpsTest.Extensions;

namespace FpsTest.Combat.PlayerCombat;

[GlobalClass]
public partial class PlayerCombatController : Node3D
{
    public AnimationPlayer Animator;
    [Export] private Marker3D _spellSpawnPoint;
    [Export] public PackedScene SpellPrefab;
    [Export] public Label3D Cig;
    [Export] public float ManaMaximum = 100;
    public float CurrentMana;
    
    public override void _Ready()
    {
        Cig = GetNode<Label3D>("Cig");
        Animator = this.GetChildByType<AnimationPlayer>();
        Animator.Play("Relax_hands_idle");
        CurrentMana = ManaMaximum;
    }

    public void SpawnSpell()
    {
        Node objectNode = SpellPrefab.Instantiate();
        if (objectNode is Node3D node3D)
        {
            node3D.GlobalTransform = _spellSpawnPoint.GlobalTransform;
        }
        
        GetTree().CurrentScene.AddChild(objectNode);
    }
    
}
