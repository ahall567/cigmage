using Godot;
using CigMage.Extensions;
using CigMage.Combat.PlayerCombat;

namespace CigMage.PlayerCharacter.Cig;

public partial class CigBase : Node3D
{
    [Export] private PlayerCombatController _playerCombatController;
    [Export] private float _refillRate;

    public Label3D StateLabel;
    
    // --- PUBLIC API ---
    public void RegenMana(double delta)
    {
        float manaToAdd = _refillRate * (float)delta;
        _playerCombatController.ManaComponent.Increase(manaToAdd);
    }

    // --- GODOT OVERRIDES ---
    public override void _EnterTree()
    {
        StateLabel = this.GetChildByType<Label3D>();
    }
    
}
