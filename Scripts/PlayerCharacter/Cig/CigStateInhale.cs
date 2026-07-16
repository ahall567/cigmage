using Godot;

namespace CigMage.PlayerCharacter.Cig;

[GlobalClass]
public partial class CigStateInhale : CigStateBase
{
    public override CigState StateKey => CigState.Inhale;
    
    // --- STATE METHODS ---
    public override void Update(double delta)
    {
        base.Update(delta);
        CigBaseParent.RegenMana(delta);
    }
}
