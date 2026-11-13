using Godot;

namespace CigMage.Combat.Effects;

[GlobalClass]
public partial class KnockbackEffect : CombatEffect
{
    [Export] public float Force { get; set; } = 5f;
    
    public override void Apply(CombatHandler target, in CombatContext ctx)
        => target.ApplyKnockback(Force, ctx.SourcePosition);
}