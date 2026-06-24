using Godot;
using FpsTest.Extensions;

namespace FpsTest.Combat.PlayerCombat;

[GlobalClass]
public partial class PlayerCombatController : Node3D
{
    private AnimationPlayer _animator;
    [Export] public Label3D Spell;
    [Export] public Label3D Cig;
    
    public override void _Ready()
    {
        Spell = GetNode<Label3D>("Spell");
        Cig = GetNode<Label3D>("Cig");
        _animator = this.GetChildByType<AnimationPlayer>();
        _animator.Play("Relax_hands_idle");
    }
    
}
