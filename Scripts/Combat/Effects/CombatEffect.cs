using Godot;

namespace CigMage.Combat.Effects;

[GlobalClass]
public abstract partial class CombatEffect : Resource
{
    public abstract void Apply(CombatHandler target, in CombatContext context);
}