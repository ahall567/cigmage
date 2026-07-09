using Godot;

namespace CigMage.PlayerCharacter.Cig;

[GlobalClass]
public partial class CigStateInhale : CigStateBase
{
    public override CigState StateKey => CigState.Inhale;
}
