using Godot;

namespace CigMage.PlayerCharacter.Cig;

[GlobalClass]
public partial class CigStateIdle : CigStateBase
{
    public override CigState StateKey => CigState.Idle;
}
