using Godot;

namespace CigMage.Combat.Effects;

[GlobalClass]
public partial class DamageEffect : CombatEffect
{
    [Export] public float Amount { get; set; } = 10f;
    
    public override void Apply(CombatHandler target, in CombatContext ctx)
        => target.ApplyDamage(Amount);
}