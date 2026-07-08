using Godot;
using CigMage.Extensions;
using CigMage.Combat.PlayerCombat;

namespace CigMage.PlayerCharacter.Cig;

public partial class CigBase : Node3D
{
    [Export] private PlayerCombatController _playerCombatController;

    public Label3D StateLabel;

    public override void _EnterTree()
    {
        StateLabel = this.GetChildByType<Label3D>();
    }
}
