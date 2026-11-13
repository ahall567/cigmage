using Godot;

namespace CigMage.Combat.Effects;

[GlobalClass]
public partial class HealEffect : CombatEffect
{
    [Export] public float Amount { get; set; } = 25f;
    
    public override void Apply(CombatHandler target, in CombatContext ctx)
        => target.ApplyHeal(Amount);
}